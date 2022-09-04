using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                list.Add(item);
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
        [HttpPost]
        //public ActionResult Payment(string name, string address, string email, string contact)
        //{
        //    var cart = Session[CartSession];
        //    var order = new Order();
        //    var list = new List<CartItem>();
        //    if (cart != null)
        //    {
        //        list = (List<CartItem>)cart;
        //    }
        //    var userEmail = Session["Email"];
        //    var user = db.Users.FirstOrDefault(x => x.Email == userEmail);
        //    if (user != null)
        //    {
        //        order = new Order()
        //        {
        //            Name = user.Name,
        //            Address = user.Address,
        //            Email = user.Email,
        //            Contact = user.Contact,
        //            Card_No = user.Card_No
        //        };
        //    }
        //    else
        //    {
        //        order = new Order()
        //        {

        //            Name = user.Name,
        //            Address = user.Address,
        //            Email = user.Email,
        //            Contact=user.Contact,
        //            Card_No = user.Card_No

        //        };
        //        return Redirect("/payment error");
        //    }
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        foreach (var item in list)
        //        {

        //            var orderDetail = new Order_Detail()
        //            {
        //                Order_Id = order.Order_Id,
        //                Book_Id = item.Book.Book_Id,
        //                Quantity = item.Quantity,
        //                Price = item.Book.Price,

        //            };
        //            db.Order_Detail.Add(orderDetail);
        //        }
        //        db.SaveChanges();
        //        return Redirect("/complete");
        //}
        public ActionResult Payment(string name, string mobile, string address, string email)
        {
            var order = new Order();
            order.Status = false;
            order.Date = DateTime.Now;
            order.Address = address;
            order.Contact = mobile;
            order.Name = name;
            order.Email = email;

            try
            {
                var id = new OrderContoller().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailController = new OrderDetailContoller();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new Order_Detail();
                    orderDetail.Book_Id = item.Book.Book_Id;
                    orderDetail.Order_Id = (int)id;
                    orderDetail.Price = item.Book.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailController.Insert(orderDetail);

                    total = (decimal)(item.Book.Price * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Local/assets/template/neworder.html"));

                content = content.Replace("{{Name}}", name);
                content = content.Replace("{{Contact}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Card_No}}", total.ToString("N0"));
                

            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/payment error");
            }
            return Redirect("/complete");
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
        public ActionResult Success()
        {
            return View();
        }
    }
}