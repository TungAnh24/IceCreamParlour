using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IceCreamParlour.Models;
using PagedList;

namespace IceCreamParlour.Controllers
{
    public class RecipesUserViewController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

       


        // GET: RecipesUserView
        public ActionResult Index()
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

        // GET: RecipesUserView/Create
        public ActionResult Create()
        {
            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name");
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name");
            return View();
        }

        // POST: RecipesUserView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Recipe_Id,Recipe_Name,Image,Ingredients,MakingProcess,AdminCreate_Id,Publist_Date,Flavor_Id,Update_Date,AdminUpdate_Id")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name", recipe.AdminCreate_Id);
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name", recipe.Flavor_Id);
            return View(recipe);
        }

        // GET: RecipesUserView/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name", recipe.AdminCreate_Id);
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name", recipe.Flavor_Id);
            return View(recipe);
        }

        // POST: RecipesUserView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Recipe_Id,Recipe_Name,Image,Ingredients,MakingProcess,AdminCreate_Id,Publist_Date,Flavor_Id,Update_Date,AdminUpdate_Id")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name", recipe.AdminCreate_Id);
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name", recipe.Flavor_Id);
            return View(recipe);
        }

        // GET: RecipesUserView/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: RecipesUserView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
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
