using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IceCreamParlour.Models;

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class RecipesController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Recipes
        public ActionResult Index()
        {
            var recipes = db.Recipes.Include(r => r.Admin).Include(r => r.Flavor);
            return View(recipes.ToList());
        }

        // GET: Local/Recipes/Details/5
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

        // GET: Local/Recipes/Create
        public ActionResult Create()
        {
            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name");
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name");
            return View();
        }

        // POST: Local/Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Recipe_Id,Recipe_Name,Image,Ingredients,MakingProcess,AdminCreate_Id,Publist_Date,Flavor_Id,Update_Date,AdminUpdate_Id")] Recipe recipe, HttpPostedFileBase fileUpLoad)
        {
            if (ModelState.IsValid)
            {
                if (fileUpLoad.ContentLength >0)
                {
                    var fn = System.IO.Path.GetFileName(fileUpLoad.FileName);
                    recipe.Image = fn;
                    var fp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/RecipeImages"), recipe.Image);
                    fileUpLoad.SaveAs(fp);
                }
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name", recipe.AdminCreate_Id);
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name", recipe.Flavor_Id);

            return View(recipe);
        }

        // GET: Local/Recipes/Edit/5
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

        // POST: Local/Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Recipe_Id,Recipe_Name,Image,Ingredients,MakingProcess,AdminCreate_Id,Publist_Date,Flavor_Id,Update_Date,AdminUpdate_Id")] Recipe recipe,HttpPostedFileBase fileEdit)
        {
            if (ModelState.IsValid)
            {
                if (fileEdit.ContentLength > 0)
                {
                    var fn = System.IO.Path.GetFileName(fileEdit.FileName);
                    recipe.Image = fn;
                    var fp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/RecipeImages"), recipe.Image);
                    fileEdit.SaveAs(fp);
                }
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminCreate_Id = new SelectList(db.Admins, "Admin_Id", "Name", recipe.AdminCreate_Id);
            ViewBag.Flavor_Id = new SelectList(db.Flavors, "Flavor_Id", "Flavor_Name", recipe.Flavor_Id);
            return View(recipe);
        }

        // GET: Local/Recipes/Delete/5
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

        // POST: Local/Recipes/Delete/5
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
