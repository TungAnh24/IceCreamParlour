using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class CartController : Controller
    {
        DbIcecreamParlourEntities1 db = new DbIcecreamParlourEntities1();
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list); 
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
    }
}