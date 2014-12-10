using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

//namespace Voyage.App_Start
namespace Voyage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/bootstrap.min.css",                
                "~/Content/bootstrap-theme.min.css",
                "~/Content/jquery.fancybox.css",
                "~/Content/popup.css",
                "~/Content/main.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-1.11.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/functions.js"
            ));
        }
    }
}