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
            services.AddTransient<ICommandMediator, CommandMediator>();

            services.AddTransient<ICommandHandler<SaveAdvertCommand, Advert>, SaveAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<PostAdvertCommand, Advert>, PostAdvertCommandHandler>();
            services.AddTransient<ICommandHandler<PublishAdvertCommand, Advert>, PublishAdvertCommandHandler>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Advert, AdvertDto>();
            });

            services.AddSingleton<IMapper>(sp => config.CreateMapper());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Error);

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://172.17.0.1:5002",
                ScopeName = "api1",

                RequireHttpsMetadata = false
            });

            app.UseMvc();

            app.ApplicationServices
                .GetService<AdvertisingDbContext>()
                .SeedData();
        }
    }
}