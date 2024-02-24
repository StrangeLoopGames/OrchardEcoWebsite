// Copyright (c) Strange Loop Games. All rights reserved.

namespace OrchardCore.Themes.CompanyTheme
{
    using Microsoft.Extensions.Options;
    using OrchardCore.ResourceManagement;

    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest Manifest;

        static ResourceManagementOptionsConfiguration()
        {
            Manifest = new ResourceManifest();

            Manifest
                .DefineStyle("CompanyTheme-bootstrap-oc")
                .SetUrl("~/StrangeLoopGames.CompanyTheme/css/bootstrap-oc.min.css", "~/StrangeLoopGames.CompanyTheme/css/bootstrap-oc.css")
                .SetVersion("1.0.0");
        }


        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(Manifest);
        }
    }
}
