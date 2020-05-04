namespace WebAppMVC_College
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using WebAppMVC_College.models;

    public class CollegeContext : DbContext
    {
        public CollegeContext() : base("name=CollegeContext"){
            Database.SetInitializer<CollegeContext>(null);
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
    }
}