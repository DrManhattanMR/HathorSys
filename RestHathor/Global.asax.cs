using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RestHathor
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private void EnableCrossDmainAjaxCall()
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin",
            //              "http://localhost:5187");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin",
                          "*");
            // HttpContext.Current.Response.AddHeader("Access - Control - Allow - Headers", "Access - Control - Allow - Headers, Origin, Accept, X - Requested - With, Content - Type, Access - Control - Request - Method, Access - Control - Request - Headers", "true");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods",
                              "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                              "Content-Type, Authorization");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age",
                              "1728000");

                //response.setHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
                //response.setHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers, Origin,Accept, X-Requested-With, Content-Type, Access-Control-Request-Method, Access-Control-Request-Headers");
                HttpContext.Current.Response.End();
            }

            Response.Write(HttpContext.Current.Response.Headers.Get("X-Apikey"));
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            EnableCrossDmainAjaxCall();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
