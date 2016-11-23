using System.IdentityModel.Tokens.Jwt;
using GoFish.UI.MVC.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoFish.UI.MVC.Advert
{
    public class ProgramStartup
    {
        private readonly IConfiguration _config;

        public ProgramStartup(IHostingEnvironment hostEnv)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(hostEnv.ContentRootPath)
                .AddJsonFile($"ApplicationSettings.{hostEnv.EnvironmentName}.json", optional: false)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<ApplicationSettings>(_config.GetSection("ApplicationSettings"));

            services.AddSingleton<IUserDetails, UserDetails>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(
            IHostingEnvironment env,
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IOptions<ApplicationSettings> settings)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Warning);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                loggerFactory.AddConsole(LogLevel.Warning);
            }

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
                PostLogoutRedirectUri = settings.Value.PostLogoutRedirectUri,
                RequireHttpsMetadata = false,
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