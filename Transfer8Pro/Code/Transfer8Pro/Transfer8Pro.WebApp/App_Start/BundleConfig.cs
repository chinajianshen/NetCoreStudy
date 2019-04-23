using System.Web;
using System.Web.Optimization;

namespace Transfer8Pro.WebApp
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/content/common_js/jquery").Include(
                   "~/Content/Scripts/jquery.min.js"
                   ));

            bundles.Add(new ScriptBundle("~/content/common_js/jqueryver").Include(
                  "~/Content/Scripts/jquery-1.9.1.min.js"
                  ));

            bundles.Add(new ScriptBundle("~/content/common_js/jqueryval").Include(
                  "~/Content/Scripts/jquery.validate.js"
                  ));

            bundles.Add(new StyleBundle("~/content/common_css/font_awesome_ie7").Include(
                      "~/Content/Styles/font-awesome-ie7.min.css"
                      ));

            bundles.Add(new StyleBundle("~/content/common_css/ace_ie9").Include(
                     "~/Content/Styles/ace-ie.min.css"
                     ));

            bundles.Add(new ScriptBundle("~/content/scripts/Plugins/layer/js").Include(
                "~/Content/Scripts/Plugins/layer/layui-all.js"
               ));

            bundles.Add(new StyleBundle("~/content/scripts/Plugins/jstree/dist/themes/default/css").Include(
               "~/Content/Scripts/Plugins/jstree/dist/themes/default/style.css"));

            bundles.Add(new ScriptBundle("~/content/scripts/Plugins/jstree/dist/js").Include(
                 "~/Content/Scripts/Plugins/jstree/dist/jstree.js"));

            #region 登录页JS/CSS绑定
            bundles.Add(new StyleBundle("~/content/login_css/bootstrap").Include(
                        "~/Content/Styles/bootstrap.min.css",
                        "~/Content/Styles/bootstrap-responsive.min.css",
                        "~/Content/Styles/font-awesome.min.css"
                        ));

            bundles.Add(new StyleBundle("~/content/login_css/ace").Include(
                       "~/Content/Styles/ace.min.css",
                       "~/Content/Styles/ace-responsive.min.css"
                       ));
            #endregion

            #region 母版页JS/CSS绑定
            bundles.Add(new StyleBundle("~/content/layout_css/bootstrap").Include(
                       "~/Content/Styles/bootstrap.min.css",
                       "~/Content/Styles/bootstrap-responsive.min.css",
                       "~/Content/Styles/font-awesome.min.css"
                       ));

            bundles.Add(new StyleBundle("~/content/layout_css/ace").Include(
                      "~/Content/Styles/ace.min.css",
                      "~/Content/Styles/ace-responsive.min.css",
                      "~/Content/Styles/ace-skins.min.css"
                      ));

            bundles.Add(new StyleBundle("~/content/layout_css/common").Include(
                   "~/Content/Styles/common.css",
                   "~/Content/Scripts/Plugins/select2/select2.css",
                   "~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-min.css",
                   // "~/Content/Scripts/Plugins/layer/skin/layer.css",
                   //"~/Content/Scripts/Plugins/layer/skin/layer.css",
                   //"~/Content/Scripts/Plugins/layer/skin/layer-ext.css",
                   "~/Content/Scripts/Plugins/iCheck/custom.css"
                   ));          

            bundles.Add(new ScriptBundle("~/content/layout_js/bootstrap").Include(
                   "~/Content/Scripts/bootstrap.min.js"
                   ));
            bundles.Add(new ScriptBundle("~/content/layout_js/common").Include(
                     "~/Content/Scripts/jquery-ui-1.10.2.custom.min.js",
                     "~/Content/Scripts/jquery.ui.touch-punch.min.js",
                      "~/Content/Scripts/jquery.slimscroll.min.js",
                      "~/Content/Scripts/jquery.easy-pie-chart.min.js",
                       "~/Content/Scripts/jquery.sparkline.min.js",
                        "~/Content/Scripts/jquery.flot.min.js",
                         "~/Content/Scripts/jquery.flot.pie.min.js",
                          "~/Content/Scripts/jquery.flot.resize.min.js",
                          "~/Content/Scripts/ace-elements.min.js",
                          "~/Content/Scripts/ace.min.js",
                          //"~/Content/Scripts/Plugins/layer/layui-all.js",
                           "~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-min.js",
                "~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-mobile-min.js",
                "~/Content/Scripts/Plugins/bootstrap-table/locale/bootstrap-table-zh-CN-min.js",
                "~/Content/Scripts/Plugins/bootstrap-table/table-constructor.js",
                 "~/Content/Scripts/Plugins/iCheck/icheck-min.js",
                 "~/Content/Scripts/Plugins/select2/select2.js",
                "~/Content/Scripts/common.js"
                     ));



            #endregion

            #region 弹出层JS/CSS绑定
            bundles.Add(new StyleBundle("~/content/layoutpopub_css/bootstrap").Include(
                      "~/Content/Styles/bootstrap.min.css",
                      "~/Content/Styles/bootstrap-responsive.min.css",
                      "~/Content/Styles/font-awesome.min.css"
                      ));

            bundles.Add(new StyleBundle("~/content/layoutpopub_css/ace").Include(
                    "~/Content/Styles/ace.min.css",
                    "~/Content/Styles/ace-responsive.min.css",
                    "~/Content/Styles/ace-skins.min.css"
                    ));


            bundles.Add(new StyleBundle("~/content/layoutpopub_css/common").Include(
                   "~/Content/Styles/common.css",
                   "~/Content/Scripts/Plugins/select2/select2.css",
                    //"~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-min.css",
                   // "~/Content/Scripts/Plugins/layer/skin/layer.css",
                   //"~/Content/Scripts/Plugins/layer/skin/layer.css",
                   //"~/Content/Scripts/Plugins/layer/skin/layer-ext.css",
                   "~/Content/Scripts/Plugins/iCheck/custom.css"
                    ));


            bundles.Add(new ScriptBundle("~/content/layoutpopub_js/bootstrap").Include(
                 "~/Content/Scripts/bootstrap.min.js"
                 ));

            bundles.Add(new ScriptBundle("~/content/layoutpopub_js/common").Include(
               "~/Content/Scripts/ace-elements.min.js",
               "~/Content/Scripts/ace.min.js",
                //"~/Content/Scripts/Plugins/layer/layui-all.js",
                 //"~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-min.js",
                 //"~/Content/Scripts/Plugins/bootstrap-table/bootstrap-table-mobile-min.js",
                 //"~/Content/Scripts/Plugins/bootstrap-table/locale/bootstrap-table-zh-CN-min.js",
                 //"~/Content/Scripts/Plugins/bootstrap-table/table-constructor.js",
                 "~/Content/Scripts/Plugins/iCheck/icheck-min.js",
                 "~/Content/Scripts/Plugins/select2/select2.js",
                "~/Content/Scripts/common.js"
              ));
            #endregion
        }
    }
}
