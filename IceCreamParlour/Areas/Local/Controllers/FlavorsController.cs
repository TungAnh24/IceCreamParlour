using System;

using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.UI.WebControls;
using IceCreamParlour.Models;
using PagedList;

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class FlavorsController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Flavors
        public ActionResult Index(string Sort_Order, string Search_Data, int? Page_No)
        {
            ModelState.Clear();
            ViewBag.CurrentSort = Sort_Order;
            ViewBag.SortName = String.IsNullOrEmpty(Sort_Order) ? "Flavor_Name_desc" : "";
            ViewBag.SortIn = Sort_Order == "Ingredients" ? "Ingredients_desc" : "Ingredients";
            var flavors = from f in db.Flavors select f;
            switch (Sort_Order)
            {
                case "Flavor_Name_desc":
                    flavors = flavors.OrderByDescending(f => f.Flavor_Name);
                    break;
                case "Ingredients":
                    flavors = flavors.OrderBy(f => f.Ingredients);
                    break;
                case "Ingredients_desc":
                    flavors = flavors.OrderByDescending(f => f.Ingredients);
                    break;
                default:
                    flavors = flavors.OrderBy(f => f.Flavor_Name);
                    break;
            }
            var fla = flavors.Include(r => r.Recipes).Where(f => f.Flavor_Name.Contains(Search_Data) || Search_Data == null).ToList().ToPagedList(Page_No ?? 1, 5);
            return View(fla);
        }
        // GET: Local/Flavors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flavor flavor = db.Flavors.Find(id);
            if (flavor == null)
            {
                return HttpNotFound();
            }
            return View(flavor);
        }

        // GET: Local/Flavors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Local/Flavors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Flavor_Id,Flavor_Name,Ingredients,MakingProcess,Description,Image")] Flavor flavor, HttpPostedFileBase fileUpLoad)
        {
                                    try
                                    {
                                        if (ModelState.IsValid)
                                        {
                                            if (fileUpLoad.ContentLength > 0)
                                            {
                                                var fn = System.IO.Path.GetFileName(fileUpLoad.FileName);
                                                flavor.Image = fn;
                                                var fp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/FlavorImages"), flavor.Image);
                                                fileUpLoad.SaveAs(fp);
                                            }
                                            var check = db.Flavors.FirstOrDefault(f => f.Flavor_Name == flavor.Flavor_Name);
                                            if (check == null)
                                            {
                                                db.Flavors.Add(flavor);
                                                db.SaveChanges();
                                                return RedirectToAction("Index");
                                            }
                                            else
                                            {
                                                ViewBag.error = "Flavor's name is already exits!";
                                                return View();
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        ModelState.AddModelError(string.Empty, "Please upload images");
                                    }
            return View(flavor);
        }

        // GET: Local/Flavors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flavor flavor = db.Flavors.Find(id);
            if (flavor == null)
            {
                return HttpNotFound();
            }
            return View(flavor);
        }

        // POST: Local/Flavors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Flavor_Id,Flavor_Name,Ingredients,MakingProcess,Description,Image")] Flavor flavor,HttpPostedFileBase fileEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileEdit.ContentLength > 0)
                    {
                        var fn = System.IO.Path.GetFileName(fileEdit.FileName);
                        flavor.Image = fn;
                        var fp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/FlavorImages"), flavor.Image);
                        fileEdit.SaveAs(fp);
                    }
                    var check = db.Flavors.FirstOrDefault(f => f.Flavor_Name == flavor.Flavor_Name && f.Flavor_Id !=flavor.Flavor_Id);
                    if (check == null)
                    {
                        db.Entry(flavor).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Flavor's name is already exits!";
                        return View();
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Please upload images");
            }
            return View(flavor);
        }

        // GET: Local/Flavors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flavor flavor = db.Flavors.Find(id);
            if (flavor == null)
            {
                return HttpNotFound();
            }
            return View(flavor);
        }

        // POST: Local/Flavors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flavor flavor = db.Flavors.Find(id);
            db.Flavors.Remove(flavor);
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
