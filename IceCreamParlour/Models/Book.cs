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

    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Order_Detail = new HashSet<Order_Detail>();
        }
    
        public int Book_Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        //[Required]
        public string Image { get; set; }
        public double Price { get; set; }
        public System.DateTime Create_Date { get; set; }
        public long AdminAdd_Id { get; set; }
        public string Author { get; set; }
        public Nullable<long> AdminUpdate_Id { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }

        public int limit = 15;

        [Display(Name = "Title")]
        public string TitleTrimmed
        {
            get
            {
                if (this.Title.Length > this.limit) return this.Title.Substring(0, this.limit) + "...";
                else return this.Title;
            }
        }

        [Display(Name = "Description")]
        public string DesTrimmed
        {
            get
            {
                if (this.Description.Length > this.limit) return this.Description.Substring(0, this.limit) + "...";
                else return this.Description;
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

        [Display(Name = "Author")]
        public string AuthorTrimmed
        {
            get
            {
                if (this.Author.Length > this.limit) return this.Author.Substring(0, this.limit) + "...";
                else return this.Author;
            }
        }
    }
}
