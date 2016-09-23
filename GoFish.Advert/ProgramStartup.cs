using AutoMapper;
using GoFish.Shared.Dto;
using GoFish.Shared.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoFish.Advert
{
    public class ProgramStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AdvertisingDbContext>();
            services.AddTransient<IMessageBroker<Advert>, AdvertMessageBroker>();
            services.AddTransient<AdvertRepository, AdvertRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advert, AdvertDto>();
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMvc();

            app.ApplicationServices
                .GetService<AdvertisingDbContext>()
                .SeedData();
        }
    }
}