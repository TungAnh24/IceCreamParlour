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
                name: "Add Cart",
                url: "AddToCart",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "IceCreamParlour.Controllers" }
                );
            routes.MapRoute(
                name: "Payment Success",
                url: "complete",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "IceCreamParlour.Controllers" }
                );
            routes.MapRoute(
                name: "payment error",
                url: "payment error",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "Icecreamparlour.controllers" }
                 );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            // name: "Product Detail",
            // url: "chi-tiet/{title}-{id}",
            // defaults: new { controller = "Book", action = "Index", id = UrlParameter.Optional },
            // namespaces: new[] { "IceCreamParlour.Controllers" }
            //);
            

        }

    }
}