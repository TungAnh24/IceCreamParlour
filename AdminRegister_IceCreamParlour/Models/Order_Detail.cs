//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminRegister_IceCreamParlour.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Detail
    {
        public int OrderDetail_Id { get; set; }
        public int Order_Id { get; set; }
        public int Book_Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}