using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace PlayEco.OrchardCore.Cloud
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache(); // Add distributed memory cache
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder.UseSession();
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
        }
        
    }
}
