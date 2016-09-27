using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoFish.Identity
{
    public class ProgramStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(options => { options.IssuerUri = "WierdnessHere"; })
                .AddInMemoryStores()
                .AddInMemoryScopes(IdentityConfiguration.GetScopes())
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                .AddInMemoryUsers(IdentityConfiguration.GetUsers())
                .SetTemporarySigningCredential();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Error);

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseMvc();
        }
    }
}