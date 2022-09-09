using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IceCreamParlour
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public static void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Local_default",
                "Local/{controller}/{action}/{id}",
                defaults: new { controller = "LoginAdmin",action = "Login", id = UrlParameter.Optional },
                //new { controller = "LoginAdmin" },
                new[] { "MyApp.Areas.Local.Controllers" }
            );
        }       
    }
}