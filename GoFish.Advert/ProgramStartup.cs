using System;
using EventStore.ClientAPI;
using GoFish.Shared.Interface;
using GoFish.Shared.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace GoFish.Advert
{
    public class ProgramStartup
    {
        private readonly IConfiguration _config;

        public ProgramStartup(IHostingEnvironment hostEnv)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(hostEnv.ContentRootPath)
                .AddJsonFile("ApplicationSettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<ApplicationSettings>(_config.GetSection("ApplicationSettings"));

            services.AddDbContext<AdvertisingDbContext>();
            services.AddTransient<IMessageBroker<Advert>, AdvertMessageBroker>();
            services.AddTransient<AdvertRepository, AdvertRepository>();
            services.AddTransient<ICommandMediator, CommandMediator>();
            services.AddTransient<IAdvertFactory, AdvertFactory>();

            services.AddTransient<ICommandHandler<CreateAdvertCommand, Advert>, CreateAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateAdvertCommand, Advert>, UpdateAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<PostAdvertCommand, Advert>, PostAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<PublishAdvertCommand, Advert>, PublishAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<WithdrawAdvertCommand, Advert>, WithdrawAdvertCommandHandler>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advert, AdvertDto>();
            });

            services.AddSingleton<AutoMapper.IMapper>(sp => config.CreateMapper());

            // services.AddSingleton<IEventStoreConnection>(sp => EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113")));     // Local (With ES running in vagrant)
            services.AddSingleton<IEventStoreConnection>(sp => EventStoreConnection.Create(new Uri("tcp://admin:changeit@172.17.0.1:1113")));    // Vagrant
            // services.AddSingleton<IEventStoreConnection>(sp => EventStoreConnection.Create(new Uri("tcp://admin:changeit@54.171.92.206:1113"))); // Live

            services.AddScoped<ModelStateActionFilterAttribute>();
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IOptions<ApplicationSettings> options)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = options.Value.IdentityServerUrl,
                RequireHttpsMetadata = false,

                ScopeName = "api1",
                AutomaticAuthenticate = true
            });

            app.UseMvc();

            app.ApplicationServices
                .GetService<AdvertisingDbContext>()
                .SeedData();
        }
    }
}