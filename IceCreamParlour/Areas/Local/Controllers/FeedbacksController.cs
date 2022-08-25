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
    public class FeedbacksController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Feedbacks
        //public ActionResult Index()
        //{
        //    var feedbacks = db.Feedbacks.Include(f => f.User);
        //    return View(feedbacks.ToList());
        //}

        public ActionResult Index(string Sort_Order, string Search_Data, int? Page_No)
        {
            ViewBag.CurrentSort = Sort_Order;
            ViewBag.SortName = String.IsNullOrEmpty(Sort_Order) ? "Feedback_Detail_desc" : "";
            var feedbacks = from f in db.Feedbacks select f;
            switch (Sort_Order)
            {
                case "Feedback_Detail_desc":
                    feedbacks = feedbacks.OrderByDescending(f => f.Feedback_Detail);
                    break;
                default:
                    feedbacks = feedbacks.OrderBy(f => f.Feedback_Detail);
                    break;
            }
            var feed = feedbacks.Include(f => f.User)
                .Where(f=>f.Feedback_Detail.Contains(Search_Data) || Search_Data == null).ToList().ToPagedList(Page_No ?? 1, 5);
            return View(feed);
        }

        // GET: Local/Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Local/Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name");
            return View();
        }

        // POST: Local/Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Feedback_Id,Feedback_Detail,User_Id,Name,Date,Contact,Email")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", feedback.User_Id);
            return View(feedback);
        }

        // GET: Local/Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", feedback.User_Id);
            return View(feedback);
        }

        // POST: Local/Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Feedback_Id,Feedback_Detail,User_Id,Name,Date,Contact,Email")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", feedback.User_Id);
            return View(feedback);
        }

        // GET: Local/Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Local/Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
