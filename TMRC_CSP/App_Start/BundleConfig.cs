using System.Web;
using System.Web.Optimization;

namespace TMRC_CSP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerys").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      //"~/Scripts/respond.js"
                      "~/bootstrap/js/modernizr-custom.js",
                      "~/bootstrap/js/classie.js",
                      "~/bootstrap/js/dummydata.js",
                      "~/bootstrap/js/main.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      //"~/Content/site.css"
                      "~/bootstrap/css/organicfoodicons.css",
                      "~/bootstrap/css/demo.css",
                      "~/bootstrap/css/component.css"
                      ));


            bundles.Add(new StyleBundle("~/Content/MyCss").Include(
                "~/Content/MyStyle.css"));
            bundles.Add(new ScriptBundle("~/Script/MyScript").Include(
                "~/Scripts/MyScript.js"));

            bundles.Add(new StyleBundle("~/Content/Invoice").Include(
                "~/Content/Invoice.css"));
            

            //For Wizard steps
            bundles.Add(new StyleBundle("~/Content/stepsCSS").Include(
              "~/Content/wizard/steps.css"));
            //bundles.Add(new ScriptBundle("~/Script/stepsJS").Include(
            //    "~/Content/wizard/jquery.steps.min.js"));

            //For Sweetalert
            bundles.Add(new StyleBundle("~/Content/sweetAlert").Include(
              "~/Content/sweetalert/sweetalert.css"));
            bundles.Add(new ScriptBundle("~/Script/sweetAlert").Include(
                "~/Content/sweetalert/sweetalert.min.js"));


        }
    }
}
