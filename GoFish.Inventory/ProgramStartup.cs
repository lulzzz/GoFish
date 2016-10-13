using AutoMapper;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoFish.Inventory
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<InventoryDbContext>();
            services.AddTransient<IMessageBroker<StockItem>, InventoryMessageBroker>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockItem, StockItemDto>();
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
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