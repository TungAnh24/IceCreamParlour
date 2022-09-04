using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceCreamParlour.Controllers
{
    public class OrderContoller
    {
        DbIcecreamParlourEntities db = null;
        public OrderContoller()
        {
            db = new DbIcecreamParlourEntities();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.Order_Id;
        }
    }
}