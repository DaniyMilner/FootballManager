using System.Web.Optimization;

namespace manager.Configuration
{
    public static class BundleConfiguration
    {
        public static void Configure()
        {
            var bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/scripts/vendor")
                    .Include("~/Scripts/jquery-{version}.js")
                    .Include("~/Scripts/knockout-{version}.js")
                    .Include("~/Scripts/bootstrap.js")
                    .Include("~/Scripts/q.js")
                    .Include("~/Scripts/underscore.js")
                    .Include("~/Scripts/jquery-ui-{version}.custom.js")
                    .IncludeDirectory("~/Scripts/knockoutBindings", "*Binding.js")
                );

            bundles.Add(new StyleBundle("~/Content/css")
                    .Include("~/Content/all.css")
                );
        }
    }
}