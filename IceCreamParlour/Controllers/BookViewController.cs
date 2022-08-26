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
    public class BookViewController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

       


        // GET: RecipesUserView
        public ActionResult Index(int? page)
        {
            var books = db.Books.ToList();
            return View(books);


        }


        // GET: RecipesUserView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
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
