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
    
    public partial class Flavor
    {
        public Flavor()
        {
            this.Recipes = new HashSet<Recipe>();
        }
    
        public int Flavor_Id { get; set; }
        public string Flavor_Name { get; set; }
        public string Ingredients { get; set; }
        public string MakingProcess { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
