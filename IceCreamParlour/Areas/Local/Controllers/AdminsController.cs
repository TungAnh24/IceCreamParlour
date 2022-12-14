using System;

using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

using System.Web.Mvc;
using IceCreamParlour.Models;
using PagedList;

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class AdminsController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Admins
        //public ActionResult Index()
        //{
        //    return View(db.Admins.Where(a=>a.IsDelete==0).ToList());
        //}

        public ActionResult Index(string Sort_Order, string Search_Data, int? Page_No)
        {
            ModelState.Clear();
            ViewBag.CurrentSort = Sort_Order;
            ViewBag.SortName = String.IsNullOrEmpty(Sort_Order) ? "Name_desc" : "";
            var admins = from a in db.Admins select a;
            switch (Sort_Order)
            {
                case "Name_desc":
                    admins = admins.OrderByDescending(f => f.Name);
                    break;
                default:
                    admins = admins.OrderBy(f => f.Name);
                    break;
            }
            var ad = admins.Where(a=>a.IsDelete ==0 && (a.Name.Contains(Search_Data) || Search_Data == null)).ToList().ToPagedList(Page_No ?? 1, 5);
            return View(ad);
        }

        // GET: Local/Admins/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Local/Admins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Local/Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Admin_Id,Name,Email,Roles,Password,IsActive,IsDelete")] Admin admin)
        {
           
            if (ModelState.IsValid)
            {
                var check = db.Admins.FirstOrDefault(s => s.Email == admin.Email);
                if (check == null)
                {
                    admin.Password = GetMD5(admin.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(admin);
        }

        // GET: Local/Admins/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Local/Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Admin_Id,Name,Email,Roles,Password,IsActive,IsDelete")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                var check = db.Admins.FirstOrDefault(s => s.Email == admin.Email);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Local/Admins/Delete/5
        //[Authorize(Roles = "1")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            admin.ConfirmPassword = admin.Password;
            admin.IsDelete = 1;
            if (admin != null)
            {
                /*db.Entry(book).State = EntityState.Modified;*/
                RedirectToAction("Index");
            }
            db.SaveChanges();
            return View(admin);
        }

        public ActionResult Lock(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            admin.ConfirmPassword = admin.Password;
            if (admin.IsActive == 0) admin.IsActive = 1;
            else admin.IsActive = 0;
            if (admin != null)
            {
                /*db.Entry(book).State = EntityState.Modified;*/
                RedirectToAction("Index");
            }
            db.SaveChanges();
            return View(admin);
        }

        //[HttpPost]
        //public ActionResult Delete(int? id)
        //{
        //    Admin admin = new Admin();
        //    admin.De
        //}

        // POST: Local/Admins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Admin admin = db.Admins.Find(id);
        //    //db.Admins.Remove(admin);
        //    admin.IsDelete = 1;
        //    //db.SaveChanges();
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
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
