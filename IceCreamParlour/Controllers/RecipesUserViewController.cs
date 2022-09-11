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

        private const string CartSession = "CartSession";

        // GET: RecipesUserView
        public ActionResult Index(string searchData, int? Page_No)
        {
            var res = db.Recipes.Where(r => r.Recipe_Name.Contains(searchData) || searchData == null).ToList().ToPagedList(Page_No??1,4);
            return View(res);
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





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AddItem(int book_Id, int quantity)
        {

            var book = db.Books.Find(book_Id);
            var cart = Session[CartSession];

            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Book.Book_Id == book_Id))
                {
                    foreach (var item in list)
                    {
                        if (item.Book.Book_Id == book_Id)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Book = book;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                //tạo mới đối tượng cart Item
                var item = new CartItem();
                item.Book = book;
                item.Quantity = quantity;
                var list = new List<CartItem>();

                //Gán vào session
                Session[CartSession] = list;

                var cart1 = Session[CartSession];
                var list1 = new List<CartItem>();
                if (cart1 != null)
                {
                    list1 = (List<CartItem>)cart;
                }
                // return View(list);

            }
            return RedirectToAction("Index");
        }
    }
}
