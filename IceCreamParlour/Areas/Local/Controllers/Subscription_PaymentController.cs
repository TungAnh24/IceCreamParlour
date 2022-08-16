using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IceCreamParlour.Models;

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class Subscription_PaymentController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Subscription_Payment
        public ActionResult Index()
        {
            var subscription_Payment = db.Subscription_Payment.Include(s => s.Subscription).Include(s => s.User);
            return View(subscription_Payment.ToList());
        }

        // GET: Local/Subscription_Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription_Payment subscription_Payment = db.Subscription_Payment.Find(id);
            if (subscription_Payment == null)
            {
                return HttpNotFound();
            }
            return View(subscription_Payment);
        }

        // GET: Local/Subscription_Payment/Create
        public ActionResult Create()
        {
            ViewBag.Subscription_Id = new SelectList(db.Subscriptions, "Subscription_Id", "Title");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name");
            return View();
        }

        // POST: Local/Subscription_Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubscriptionPayment_Id,User_Id,Subscription_Id,Date")] Subscription_Payment subscription_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Subscription_Payment.Add(subscription_Payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Subscription_Id = new SelectList(db.Subscriptions, "Subscription_Id", "Title", subscription_Payment.Subscription_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", subscription_Payment.User_Id);
            return View(subscription_Payment);
        }

        // GET: Local/Subscription_Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription_Payment subscription_Payment = db.Subscription_Payment.Find(id);
            if (subscription_Payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subscription_Id = new SelectList(db.Subscriptions, "Subscription_Id", "Title", subscription_Payment.Subscription_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", subscription_Payment.User_Id);
            return View(subscription_Payment);
        }

        // POST: Local/Subscription_Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubscriptionPayment_Id,User_Id,Subscription_Id,Date")] Subscription_Payment subscription_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription_Payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subscription_Id = new SelectList(db.Subscriptions, "Subscription_Id", "Title", subscription_Payment.Subscription_Id);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", subscription_Payment.User_Id);
            return View(subscription_Payment);
        }

        // GET: Local/Subscription_Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription_Payment subscription_Payment = db.Subscription_Payment.Find(id);
            if (subscription_Payment == null)
            {
                return HttpNotFound();
            }
            return View(subscription_Payment);
        }

        // POST: Local/Subscription_Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription_Payment subscription_Payment = db.Subscription_Payment.Find(id);
            db.Subscription_Payment.Remove(subscription_Payment);
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
