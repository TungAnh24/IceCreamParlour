using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IceCreamParlour.Controllers
{
    public class CartController : Controller
    {

        DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {

            var book = db.Books.FirstOrDefault();
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list); 
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }


        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Book.Book_Id == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Book.Book_Id == item.Book.Book_Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
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
                }else 
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

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult Details(int book_Id)
        {
            if(book_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(book_Id);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
    }
}