using System;
using System.IdentityModel.Tokens.Jwt;
using EventStore.ClientAPI;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using GoFish.Shared.Command;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoFish.Inventory
{
    public class ProgramStartup
    {
        private readonly IConfiguration _config;

        public ProgramStartup(IHostingEnvironment hostEnv)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(hostEnv.ContentRootPath)
                .AddJsonFile($"ApplicationSettings.{hostEnv.EnvironmentName}.json", optional: false)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SystemMessagingPolicy", policyAdmin =>
                {
                    policyAdmin.RequireClaim("scope", "gofish.messaging");
                });
            });

            services.AddMvc();

            services.Configure<ApplicationSettings>(_config.GetSection("ApplicationSettings"));

            services.AddDbContext<InventoryDbContext>();
            services.AddTransient<InventoryRepository, InventoryRepository>();
            services.AddTransient<ICommandMediator, CommandMediator>();
            services.AddTransient<IStockItemFactory, StockItemFactory>();
            services.AddTransient<ICommandHandler<CreateStockItemCommand, StockItem>, CreateStockItemCommandHandler>();
            services.AddTransient<IMessageBroker<StockItem>, InventoryMessageBroker>();

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockItem, StockItemDto>();
            });

            services.AddSingleton<AutoMapper.IMapper>(sp => mapperConfig.CreateMapper());
            services.AddSingleton<IEventStoreConnection>(sp => EventStoreConnection.Create(new Uri(_config["ApplicationSettings:EventStoreUrl"])));
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IOptions<ApplicationSettings> options)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = options.Value.IdentityServerUrl,
                ScopeName = "api1",
                AdditionalScopes = new[] { "gofish.messaging" },
                RequireHttpsMetadata = false,
                AutomaticAuthenticate = true
            });

            app.UseMvc();

            app.ApplicationServices
                .GetService<InventoryDbContext>()
                .SeedData();
        }
    }
}