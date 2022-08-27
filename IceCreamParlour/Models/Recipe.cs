//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IceCreamParlour.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipe
    {
        public int Recipe_Id { get; set; }
        public string Recipe_Name { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public string MakingProcess { get; set; }
        public Nullable<long> AdminCreate_Id { get; set; }
        public System.DateTime Publist_Date { get; set; }
        public int Flavor_Id { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public long AdminUpdate_Id { get; set; }
    
        public virtual Admin Admin { get; set; }
        public virtual Flavor Flavor { get; set; }
    }
}
