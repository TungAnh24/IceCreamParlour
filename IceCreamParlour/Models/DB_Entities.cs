using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IceCreamParlour.Models
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("DbIcecreamParlourEntities") { }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }

}