namespace IceCreamParlour.Models
{
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class User
    {

        public User()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.Orders = new HashSet<Order>();
            this.Subscription_Payment = new HashSet<Subscription_Payment>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int User_Id { get; set; }

        [Required(ErrorMessage ="Please input your name")]
     
        public string Name { get; set; }

        [Required(ErrorMessage ="Please input your contact")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Phone number invalid")]
        [Display(Name ="Phone number")]
        public string Contact { get; set; }

        [Required(ErrorMessage ="Please input your email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Email invalid")]

        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Registration time")]
        public int UserType { get; set; }   

        [Required]
        [StringLength(14, ErrorMessage ="chua du do dai")]
        public string Card_No { get; set; }
        public System.DateTime JoinDate { get; set; }
        public int IsActive { get; set; }
        public int IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription_Payment> Subscription_Payment { get; set; }

        public int limit = 15;

        [Display(Name = "Email")]
        public string EmailTrimmed
        {
            get
            {
                if (this.Email.Length > this.limit) return this.Email.Substring(0, this.limit) + "...";
                else return this.Email;
            }
        }
    }
}
