using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IceCreamParlour.Models;
using PagedList;

namespace IceCreamParlour.Areas.Local.Controllers
{
    public class BooksController : Controller
    {
        private DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: Local/Books
        //public ActionResult Index()
        //{
        //    return View(db.Books.Where(b=>b.IsDelete==0).ToList());
        //}

        public ActionResult Index(string Sort_Order, string Search_Data, int? Page_No)
        {
            ModelState.Clear();
            ViewBag.CurrentSort = Sort_Order;
            ViewBag.SortName = String.IsNullOrEmpty(Sort_Order) ? "Title_desc" : "";
            ViewBag.SortDes = Sort_Order == "Description" ? "Description_desc" : "Description";
            var books = from b in db.Books select b;
            switch (Sort_Order)
            {
                case "Title_desc":
                    books = books.OrderByDescending(b=>b.Title);
                    break;
                case "Description":
                    books = books.OrderBy(b => b.Description);
                    break;
                case "Description_desc":
                    books = books.OrderByDescending(b => b.Description);
                    break;
                default:
                    books = books.OrderBy(b=>b.Title);
                    break;
            }
            var boo = books.Where(b=>b.IsDelete==0 && (b.Title.Contains(Search_Data) || b.Author.Contains(Search_Data) || Search_Data == null)).ToList().ToPagedList(Page_No ?? 1, 5);
            return View(boo);
        }

        // GET: Local/Books/Details/5
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

        // GET: Local/Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Local/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Book_Id,Title,Description,Image,Price,Create_Date,AdminAdd_Id,Author,AdminUpdate_Id,Update_Date,IsActive,IsDelete")] Book book,HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUpload.ContentLength > 0)
                    {
                        var bn = System.IO.Path.GetFileName(fileUpload.FileName);
                        book.Image = bn;
                        var bp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/BookImages"), bn);
                        fileUpload.SaveAs(bp);
                    }
                    var check = db.Books.FirstOrDefault(b => b.Title == book.Title);
                    if (check == null)
                    {
                        book.IsDelete = 0;
                        db.Books.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Title is already exits!";
                        return View();
                    }
                }
            }catch(Exception)
            {
                ModelState.AddModelError(string.Empty, "Please upload images");
            }
            return View(book);
        }

        // GET: Local/Books/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Local/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Book_Id,Title,Description,Image,Price,Create_Date,AdminAdd_Id,Author,AdminUpdate_Id,Update_Date,IsActive,IsDelete")] Book book, HttpPostedFileBase fileEdit)
        {
            try { 
            if (ModelState.IsValid)
            {
                if (fileEdit.ContentLength > 0)
                {
                    var bn = System.IO.Path.GetFileName(fileEdit.FileName);
                    book.Image = bn;
                    var bp = System.IO.Path.Combine(Server.MapPath("~/Areas/Local/BookImages"), bn);
                    fileEdit.SaveAs(bp);
                }
                    book.IsDelete = 0;
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception)
            {
                ModelState.AddModelError(string.Empty, "Please upload images");
            }
            return View(book);
        }

        // GET: Local/Books/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
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
            book.IsDelete = 1;
            if (book != null)
            {
                /*db.Entry(book).State = EntityState.Modified;*/
                RedirectToAction("Index");
            }
            db.SaveChanges();
            return View(book);
        }

        // POST: Local/Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Book book = db.Books.Find(id);
        //    //db.Books.Remove(book);
        //    book.IsDelete = 1;
        //    if (book != null)
        //    {
        //        /*db.Entry(book).State = EntityState.Modified;*/
        //        RedirectToAction("Index");
        //    }
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
