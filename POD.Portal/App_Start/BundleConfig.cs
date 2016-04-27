using System.Web;
using System.Web.Optimization;

namespace POD.Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Assets/lib/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Assets/lib/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Assets/lib/modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Assets/lib/bootstrap/dist/js/bootstrap.min.js",
                "~/Assets/lib/respondJS/dest/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Assets/styles/site.css",
                "~/Assets/styles/SortableGrid.css",
                "~/Assets/lib/bootstrap/dist/css/bootstrap.css",
                "~/Assets/lib/angular-block-ui/dist/angular-block-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
               "~/Assets/lib/angular/angular.min.js",
               "~/Assets/lib/angular-block-ui/dist/angular-ui.min.js",
               "~/Assets/lib/angular-block-ui/dist/angular-block-ui.js",
               "~/Assets/lib/angular-bootstrap/ui-bootstrap.min.js",
               "~/Assets/lib/angular-bootstrap/ui-bootstrap-tpls.min.js",
               "~/Assets/lib/angular-cookies/angular-cookies.min.js",
               "~/Assets/lib/angular-route/angular-route.min.js",
               "~/Assets/lib/angular-sanitize/angular-sanitize.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/shared").Include(
                "~/Views/Shared/PODBootstrap.js",
                "~/Views/Shared/AuthService.js",
                "~/Views/Shared/ApiInterceptorService.js",               
                "~/Views/Shared/AjaxService.js",
                "~/Views/Shared/AlertService.js",
                "~/Views/Shared/DataGridService.js",
                "~/Views/Shared/MasterController.js"));

            bundles.Add(new ScriptBundle("~/bundles/routing-debug").Include(
                "~/Views/Shared/PODRouting.js"));

            bundles.Add(new ScriptBundle("~/bundles/routing-production").Include(
                "~/Views/Shared/PODRouting-production.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                "~/Views/Home/IndexController.js",
                "~/Views/Home/AboutController.js"));


            bundles.Add(new ScriptBundle("~/bundles/customers").Include(
                "~/Views/Customers/CustomerMaintananceController.js"));
        }
    }
}
