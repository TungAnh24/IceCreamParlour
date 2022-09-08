using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceCreamParlour.Controllers
{
    public class OrderDetailContoller
    {
        DbIcecreamParlourEntities db = null;
        public OrderDetailContoller()
        {
            db = new DbIcecreamParlourEntities();
        }
        public bool Insert(Order_Detail detail)
        {
            try
            {
                db.Order_Detail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}