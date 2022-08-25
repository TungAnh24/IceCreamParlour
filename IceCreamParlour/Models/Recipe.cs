//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IceCreamParlour.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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

        public int limit = 15;

        [Display(Name = "Ingredients")]
        public string NameTrimmed
        {
            get
            {
                if (this.Recipe_Name.Length > this.limit)
                    return this.Recipe_Name.Substring(0, this.limit) + "...";
                else
                    return this.Recipe_Name;
            }
        }

        [Display(Name = "Ingredients")]
        public string InTrimmed
        {
            get
            {
                if (this.Ingredients.Length > this.limit)
                    return this.Ingredients.Substring(0, this.limit) + "...";
                else
                    return this.Ingredients;
            }
        }

        [Display(Name = "MakingProcess")]
        public string MkTrimmed
        {
            get
            {
                if (this.MakingProcess.Length > this.limit) return this.MakingProcess.Substring(0, this.limit) + "...";
                else return this.MakingProcess;
            }
        }

        [Display(Name = "Image")]
        public string ImageTrimmed
        {
            get
            {
                if (this.Image.Length > this.limit) return this.Image.Substring(0, this.limit) + "...";
                else return this.Image;
            }
        }
    }
}
