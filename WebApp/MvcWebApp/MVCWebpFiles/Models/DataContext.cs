using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebApp.Models
{

    public class ApplicationUser : IdentityUser
    {

        public int FacultyId { get; set; }

    }

    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {

        public MyDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

        }

        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassCancellation> ClassCancellations { get; set; }
        public DbSet<Message> Messages { get; set; }

        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.StudentFull> StudentFulls { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.StudentList> StudentLists { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.FacultyList> FacultyLists { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.FacultyFull> FacultyFulls { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.CourseList> CourseLists { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.CourseFull> CourseFulls { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.MessageList> MessageLists { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.MessageFull> MessageFulls { get; set; }
        public System.Data.Entity.DbSet<MvcWebApp.ViewModels.ClassCancellationFull> ClassCancellationFulls { get; set; }

    }

}