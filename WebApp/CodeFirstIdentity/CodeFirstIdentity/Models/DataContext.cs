using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirstIdentity.Models
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Course> Courses { get; set; }

        // implementation details 
        public DataContext() : base("DataContext") {}

        public System.Data.Entity.DbSet<CodeFirstIdentity.ViewModels.CourseFull> CourseFulls { get; set; }

    }    
}