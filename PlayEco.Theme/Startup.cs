// Copyright (c) Strange Loop Games. All rights reserved.

using PlayEco.Theme;

namespace OrchardCore.Themes.CompanyTheme
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using OrchardCore.Modules;
    using OrchardCore.ResourceManagement;

    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        }
    }
}
