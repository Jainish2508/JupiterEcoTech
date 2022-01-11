using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace JupiterEcoTech
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (sender is HttpApplication app && app.Context != null)
                app.Context.Response.Headers.Remove("Server");
            if (HttpContext.Current.Request.Url.AbsoluteUri.ToLower().EndsWith("/index"))
            {

                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                int lastSlash = url.LastIndexOf('/');
                url = (lastSlash > -1) ? url.Substring(0, lastSlash) : url;
                Response.Redirect(url, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-AspNet-Version");
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Request.IsSecureConnection == true && HttpContext.Current.Request.Url.Scheme == "https")
            {
                if (Request.Cookies.Count > 0)
                {
                    foreach (string s in Request.Cookies.AllKeys)
                    {
                        Request.Cookies[s].Secure = true;
                        Request.Cookies[s].Expires = DateTime.Now.AddMinutes(30);
                    }
                }
                if (Response.Cookies.Count > 0)
                {
                    foreach (string s in Response.Cookies.AllKeys)
                    {
                        if (s == FormsAuthentication.FormsCookieName || "asp.net_sessionid".Equals(s, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Response.Cookies[s].Secure = true;
                            Response.Cookies[s].Expires = DateTime.Now.AddMinutes(30);
                        }
                    }
                }
            }
        }
    }
}
