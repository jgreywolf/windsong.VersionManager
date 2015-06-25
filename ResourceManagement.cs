using Orchard.UI.Resources;

namespace Windsong.VersionManager
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            // jquery anti forgery
            manifest.DefineScript("VersionInfo").SetUrl("VersionInfo.js").SetVersion("1.0").SetDependencies("jQuery");
            // jquery anti forgery
            manifest.DefineScript("jQuery_AjaxAntiForgery").SetUrl("jquery-ajax-anti-forgery.js").SetVersion("1.0").SetDependencies("jQuery");
        }
    }
}
