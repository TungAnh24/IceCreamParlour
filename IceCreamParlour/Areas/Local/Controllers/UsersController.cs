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

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class UsersController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Users
        //public ActionResult Index()
        //{
        //    return View(db.Users.ToList());
        //}

        public ActionResult Index(string Sort_Order, string Search_Data, int? Page_No)
        {
            ViewBag.CurrentSort = Sort_Order;
            ViewBag.SortName = String.IsNullOrEmpty(Sort_Order) ? "Name_desc" : "";
            var users = from u in db.Users select u;
            switch (Sort_Order)
            {
                case "Name_desc":
                    users = users.OrderByDescending(u => u.Name);
                    break;
                default:
                    users = users.OrderBy(u => u.Name);
                    break;
            }
            var use = users.Where(u=>u.IsDelete==0 && (u.Name.Contains(Search_Data) || Search_Data == null)).ToList().ToPagedList(Page_No ?? 1, 5);
            return View(use);
        }

        // GET: Local/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Local/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Local/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Name,Contact,Email,Address,Password,UserType,Card_No,JoinDate,IsActive,IsDelete")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Local/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Local/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,Name,Contact,Email,Address,Password,UserType,Card_No,JoinDate,IsActive,IsDelete")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Local/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsDelete = 1;
            if(user != null)
            {
                RedirectToAction("Index");
            }
            db.SaveChanges();
            return View(user);
        }

        // POST: Local/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
