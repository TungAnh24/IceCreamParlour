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
    
    public partial class Feedback
    {
        public int Feedback_Id { get; set; }
        public string Feedback_Detail { get; set; }
        public Nullable<int> User_Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
    
        public virtual User User { get; set; }
    }
}
