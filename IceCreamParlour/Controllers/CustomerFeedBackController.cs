using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class CustomerFeedBackController : Controller
    {
        private DbIcecreamParlourEntities db;


        public CustomerFeedBackController()
        {
            db = new DbIcecreamParlourEntities();
        }

        // GET: CustomerFeedBack
        public ActionResult Index()
        {
            return View(db.Feedbacks.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Feedback model)
        {
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                db.Feedbacks.Add(model);
                await db.SaveChangesAsync();
                TempData["msg"] = "<script>alert('Your feedback has been sent to us !!!');</script>";
                return RedirectToAction("Create");
            }
            return View();
        }
    }
}