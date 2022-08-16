using IceCreamParlour.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class CartItemController : Controller
    {
        DbIcecreamParlourEntities1 db = new DbIcecreamParlourEntities1();

        public CartItemController()
        {
            db = new DbIcecreamParlourEntities1();
        }

        public List<Book> ListNewProduct(int top)
        {
            return db.Books.OrderByDescending(x => x.Create_Date).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.Books.Where(x => x.Title.Contains(keyword)).Select(x => x.Title).ToList();
        }
        public IEnumerable<Book> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Book> model = db.Books;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Title.Contains(searchString));
            }

            return model.OrderByDescending(x => x.Create_Date).ToPagedList(page, pageSize);
        }
        public List<Book> ListFeatureProduct(int top)
        {
            return db.Books.Where(x => x.Update_Date != null && x.Update_Date > DateTime.Now).OrderByDescending(x => x.Create_Date).Take(top).ToList();
        }
        //public List<Book> ListBook(ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{

        //}
        // GET: CartItem
        public Book ViewDetail(int id)
        {
            return db.Books.Find(id);
        }
    }
}