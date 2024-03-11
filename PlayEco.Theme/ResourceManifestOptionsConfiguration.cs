// Copyright (c) Strange Loop Games. All rights reserved.

using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace PlayEco.Theme
{
    public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
    {
        private static readonly ResourceManifest Manifest;

        static ResourceManagementOptionsConfiguration()
        {
            Manifest = new ResourceManifest();
            
            Manifest
                .DefineStyle("PlayEco-css")
                .SetUrl("~/PlayEco.Theme/css/styles.css", "~/PlayEco.Theme/css/styles.css")
                .SetVersion("1.0.0");
        }


        public void Configure(ResourceManagementOptions options)
        {
            options.ResourceManifests.Add(Manifest);
        }
    }
}
