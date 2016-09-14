using System.IO;
using AutoMapper;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoFish.Inventory
{
    public class Program
    {
        public static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<InventoryDbContext>();
            services.AddTransient<IMessageBroker<StockItem>, InventoryMessageBroker>();

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockItem, StockItemDto>();
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            app.ApplicationServices
                .GetService<InventoryDbContext>()
                .SeedData();
        }
    }
}