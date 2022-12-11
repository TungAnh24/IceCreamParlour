using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IceCreamParlour.Models;

namespace IceCreamParlour.Controllers
{
    public class HomeController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();
        public ActionResult Index(int? page)
        {
            var flavors = db.Flavors.ToList();
            return View(flavors);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}