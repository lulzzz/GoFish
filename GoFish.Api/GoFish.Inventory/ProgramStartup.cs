using System;
using EventStore.ClientAPI;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoFish.Inventory
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
            services.AddDbContext<InventoryDbContext>();
            services.AddTransient<IMessageBroker<StockItem>, InventoryMessageBroker>();

            services.Configure<ApplicationSettings>(_config.GetSection("ApplicationSettings"));

            var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockItem, StockItemDto>();
            });

            services.AddSingleton<AutoMapper.IMapper>(sp => mapperConfig.CreateMapper());

            services.AddSingleton<IEventStoreConnection>(sp => EventStoreConnection.Create(new Uri(_config["ApplicationSettings:EventStoreUrl"])));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseMvc();

            app.ApplicationServices
                .GetService<InventoryDbContext>()
                .SeedData();
        }
    }
}