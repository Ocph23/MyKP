﻿using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                     "~/Scripts/umd/popper.js",
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/angular.js",
                     "~/Scripts/angular-route.js" ,
                     "~/Scripts/angular-ui-router.js"  ,
                     "~/apps/app.js",
                     "~/apps/app.config.js" ,
                     "~/apps/Home/Home.Route.js",
                     "~/apps/Home/Home.Service.js",
                     "~/apps/Home/Home.Controller.js",
                     "~/apps/admin/AdminRoute.js",
                     "~/apps/admin/AdminService.js",
                     "~/apps/admin/AdminController.js",
                     "~/apps/agent/agent.Route.js",
                     "~/apps/agent/agent.Service.js",
                     "~/apps/agent/agent.Controller.js",
                     "~/node_modules/sweetalert2/dist/sweetalert2.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/node_modules/sweetalert2/dist/sweetalert2.css",
                      "~/Content/site.css"));






        }
    }
}
