namespace StudentRegistrationSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StudentContext : DbContext
    {
        public StudentContext()
            : base("name=StudentContext1")
        {
        }

        public virtual DbSet<T_Student> T_Student { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<CourceRegistration> CourceRegistrations { get; set; }

        public virtual DbSet<Cource> Cources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<StudentRegistrationSystem.Models.Role> Roles { get; set; }
    }
}

