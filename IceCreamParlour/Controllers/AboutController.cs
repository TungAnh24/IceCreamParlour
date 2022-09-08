using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class AboutController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();
        // GET: About
        public ActionResult Index(int? page)
        {
            var flavors = db.Flavors.ToList();
            return View(flavors);


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}