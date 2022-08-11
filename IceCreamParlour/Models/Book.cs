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

    [Serializable]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Order_Detail = new HashSet<Order_Detail>();
        }

        public int Book_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
    }
}
