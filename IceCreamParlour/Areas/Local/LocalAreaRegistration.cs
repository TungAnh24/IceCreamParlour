using System.Web.Mvc;

namespace IceCreamParlour.Areas.Local
{
    public class LocalAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Local";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Local_default",
                "Local/{controller}/{action}/{id}",
                new { controller = "LoginAdmin",action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}