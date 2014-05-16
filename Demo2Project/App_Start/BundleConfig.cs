using System.Web.Optimization;

namespace Demo2Project
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
    {
      // jQuery
      bundles.Add(new ScriptBundle("~/Content/jquery").Include("~/Scripts/jquery-{version}.js"));

      // Bootstrap
      bundles.Add(new ScriptBundle("~/Content/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

      // Script
      bundles.Add(new ScriptBundle("~/Content/script").Include(
                "~/Scripts/script-{version}.js"));

      // Stylesheet
      bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap/bootstrap.css", "~/Content/font-awesome.css", "~/Content/site.css"));
    }
  }
}
