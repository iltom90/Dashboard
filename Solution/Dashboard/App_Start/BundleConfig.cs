using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Dashboard.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection Bundles)
        {
            BundleTable.EnableOptimizations = false;          
            #region JS
            // Bootstrap
            Bundle Bootstrap_JS = new ScriptBundle("~/JS/Bootstrap").Include(
                "~/Scripts/System/jquery-{version}.js",
                "~/Scripts/System/bootstrap.js");

            Bundles.Add(Bootstrap_JS);
            //Bootstrap min
            Bundle Bootstrap_min_JS = new ScriptBundle("~/JS/Bootstrap_min").Include(
                "~/Scripts/System/bootstrap.min.js");
            Bundles.Add(Bootstrap_min_JS);
            //JQuery
            Bundles.Add(new ScriptBundle("~/JS/JQuery").Include("~/Scripts/System/jquery-2.1.1.min.js"));

            //JQuery 1.10
            Bundles.Add(new ScriptBundle("~/JS/JQuery1.10").Include("~/Scripts/jquery-ui-1.10.4.custom.min.js"));

            //JQuery UI
            Bundles.Add(new ScriptBundle("~/JS/JQueryUI").Include("~/Scripts/System/jquery-ui-1.8.24.js"));

            // Bootstrap Plugin
            Bundle Bootbox_JS = new ScriptBundle("~/JS/Bootstrap/Bootbox").Include("~/Scripts/System/bootbox.min.js");
            Bootbox_JS.Transforms.Clear();  
            Bundles.Add(Bootbox_JS);
               

            Bundles.Add(new ScriptBundle("~/JS/Bootstrap/Highcharts").Include(
                "~/Scripts/System/Highcharts/highcharts.js",
                "~/Scripts/System/Highcharts/modules/data.js",
                "~/Scripts/System/Highcharts/modules/exporting.js"));
          
            // Backgrid
            Bundle Backgrid_JS = new ScriptBundle("~/JS/Backgrid").Include(
                "~/Scripts/System/underscore-min.js",
                "~/Scripts/System/backbone-min.js",
                "~/Scripts/System/backbone-pageable.min.js",
                "~/Scripts/System/backgrid.min.js",
                "~/Scripts/System/backgrid-ext.js",
                "~/Scripts/System/backgrid-select-all.min.js",
                "~/Scripts/System/backgrid-paginator.min.js",
                "~/Scripts/System/backgrid-filter.min.js",
                "~/Scripts/System/moment.min.js",
                "~/Scripts/System/moment-with-locales.min.js",
                "~/Scripts/System/backgrid-moment-cell.min.js");
            Backgrid_JS.Transforms.Clear();   
            Bundles.Add(Backgrid_JS);

            // Less.js
            Bundle Less_JS = new ScriptBundle("~/JS/Less").Include("~/Scripts/System/less.min.js");
            Less_JS.Transforms.Clear();   
            Bundles.Add(Less_JS);

            Bundle Home_JS = new ScriptBundle("~/JS/Home/Index")
                .Include("~/Scripts/Home/Index.js");
            Home_JS.Transforms.Clear();   
            Bundles.Add(Home_JS);

            Bundle Works_JS = new ScriptBundle("~/JS/Works/Index")
                .Include("~/Scripts/Works/Index.js");
            Home_JS.Transforms.Clear();   
            Bundles.Add(Works_JS);

            #endregion JS
           
            #region CSS
                        
            // Bootstrap
            Bundles.Add(new StyleBundle("~/CSS/Bootstrap")
                .Include("~/Styles/System/normalize.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/bootstrap.css", new CssRewriteUrlTransform()));

            // Backgrid
            Bundle bndl_backgrid = new StyleBundle("~/CSS/Backgrid")
                .Include("~/Styles/System/backgrid.min.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/backgrid-select-all.min.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/backgrid-paginator.min.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/backgrid-filter.min.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/backgrid-moment-cell.min.css", new CssRewriteUrlTransform())
                .Include("~/Styles/System/backgrid-subgrid-cell.css", new CssRewriteUrlTransform());
            bndl_backgrid.Transforms.Clear();   
            Bundles.Add(bndl_backgrid);
            #endregion CSS
        }
    }
}