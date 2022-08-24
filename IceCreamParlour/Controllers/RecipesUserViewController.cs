using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IceCreamParlour.Models;


namespace IceCreamParlour.Controllers
{
    public class RecipesUserViewController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

       


        // GET: RecipesUserView
        public ActionResult Index(int? page)
        {
            var recipes = db.Recipes.ToList();
            return View(recipes);


        }


        // GET: RecipesUserView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
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
