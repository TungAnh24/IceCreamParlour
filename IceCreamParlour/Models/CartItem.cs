using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IceCreamParlour.Models
{
    [Serializable]
    public class CartItem
    {
        public int Book_Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}