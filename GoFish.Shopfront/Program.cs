using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<StartUp>();

            host.Build();
        }

        public class StartUp
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc();
            }

            public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
            {
                loggerFactory.AddConsole();

                app.UseMvc();
            }
        }
    }
}