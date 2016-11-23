using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GoFish.Identity
{
    public class ProgramStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentityServer(options => { options.IssuerUri = "WierdnessHere"; })
                .AddInMemoryPersistedGrants()
                .AddTemporarySigningCredential()
                .AddInMemoryClients(IdentityConfiguration.GetClients())
                .AddInMemoryScopes(IdentityConfiguration.GetScopes())
                .AddInMemoryUsers(IdentityConfiguration.GetUsers());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}