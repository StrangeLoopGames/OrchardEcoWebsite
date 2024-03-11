using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using static System.TimeSpan;

namespace PlayEco.OrchardCore.Cloud
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = FromMinutes(60);
            });
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Accounts",
                areaName: "PlayEco.OrchardCore.Cloud",
                pattern: "Account",
                defaults: new { controller = "Account", action = "Account" }
            );
            routes.MapAreaControllerRoute(
                name: "Accounts",
                areaName: "PlayEco.OrchardCore.Cloud",
                pattern: "Login",
                defaults: new { controller = "Account", action = "Login" }
            );
            routes.MapAreaControllerRoute(
                name: "Accounts",
                areaName: "PlayEco.OrchardCore.Cloud",
                pattern: "Register",
                defaults: new { controller = "Account", action = "Register" }
            );
            builder.UseSession();
            builder.UseStaticFiles();
        }
        
    }
}
