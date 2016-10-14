using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC
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
        }

        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IOptions<ApplicationSettings> settings)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true
            });

            var oidcOptions = new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = settings.Value.IdentityServerUrl,
                RequireHttpsMetadata = false,
                // PostLogoutRedirectUri = "http://localhost:8003/",   // Local
                // PostLogoutRedirectUri = "http://localhost:5003/",   // Vagrant
                PostLogoutRedirectUri = "http://54.171.92.206:5003/",   // Live
                ClientId = "gofish",
                ClientSecret = "secret",
                ResponseType = "code id_token",
                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true
            };

            oidcOptions.Scope.Clear();
            oidcOptions.Scope.Add("openid");
            oidcOptions.Scope.Add("profile");
            oidcOptions.Scope.Add("api1");

            app.UseOpenIdConnectAuthentication(oidcOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}