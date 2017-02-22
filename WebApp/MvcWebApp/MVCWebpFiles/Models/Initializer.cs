using MvcWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MvcWebApp
{

    public class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {

        protected override void Seed(MyDbContext context)
        {

            InitializeIdentityForEF(context);
            base.Seed(context);

        }

        private void InitializeIdentityForEF(MyDbContext context)
        {

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Roles -------------------------------------------------

            if (!RoleManager.RoleExists("Admin"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("Admin"));
            }
            if (!RoleManager.RoleExists("Faculty"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("Faculty"));
            }

            //Courses -----------------------------------------------

            Course int422a = new Course("INT422A", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T4042");
            Course int422b = new Course("INT422B", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T4042");
            Course int422c = new Course("INT422C", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T4042");
            Course int422d = new Course("INT422D", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T4042");

            Course jac444a = new Course("JAC444A", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T2108");
            Course jac444b = new Course("JAC444B", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T2108");
            Course jac444c = new Course("JAC444C", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T2108");
            Course jac444d = new Course("JAC444D", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T2108");

            Course dcn455a = new Course("DCN455A", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T3132");
            Course dcn455b = new Course("DCN455B", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T3132");
            Course dcn455c = new Course("DCN455C", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T3132");
            Course dcn455d = new Course("DCN455D", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "T3132");

            Course sys466a = new Course("SYS466A", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S1208");
            Course sys466b = new Course("SYS466B", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S1208");
            Course sys466c = new Course("SYS466C", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S1208");
            Course sys466d = new Course("SYS466D", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S1208");

            Course eac397a = new Course("EAC397A", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S2155");
            Course eac397b = new Course("EAC397B", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S2155");
            Course eac397c = new Course("EAC397C", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S2155");
            Course eac397d = new Course("EAC397D", new DateTime(2014, 1, 1), new DateTime(2014, 4, 1), "S2155");

            context.Courses.Add(int422a);
            context.Courses.Add(int422b);
            context.Courses.Add(int422c);
            context.Courses.Add(int422d);
            context.Courses.Add(jac444a);
            context.Courses.Add(jac444b);
            context.Courses.Add(jac444c);
            context.Courses.Add(jac444d);            
            context.Courses.Add(dcn455a);
            context.Courses.Add(dcn455b);
            context.Courses.Add(dcn455c);
            context.Courses.Add(dcn455d);
            context.Courses.Add(sys466a);
            context.Courses.Add(sys466b);
            context.Courses.Add(sys466c);
            context.Courses.Add(sys466d);
            context.Courses.Add(eac397a);
            context.Courses.Add(eac397b);
            context.Courses.Add(eac397c);
            context.Courses.Add(eac397d);

            //Faculty -----------------------------------------------

            Faculty jean = new Faculty("011111111", "Jean", "Pierre");
            int422a.Faculty = jean;
            int422b.Faculty = jean;
            int422c.Faculty = jean;
            int422d.Faculty = jean;
            context.Faculty.Add(jean);

            Faculty sandrine = new Faculty("022222222", "Sandrine", "Lopez");
            jac444a.Faculty = sandrine;
            jac444b.Faculty = sandrine;
            jac444c.Faculty = sandrine;
            jac444d.Faculty = sandrine;
            context.Faculty.Add(sandrine);

            Faculty henriquet = new Faculty("033333333", "Henriquet", "Solange");
            dcn455a.Faculty = henriquet;
            dcn455b.Faculty = henriquet;
            dcn455c.Faculty = henriquet;
            dcn455d.Faculty = henriquet;
            context.Faculty.Add(henriquet);

            Faculty coeur = new Faculty("044444444", "Coeur", "Boeuf");
            sys466a.Faculty = coeur;
            sys466b.Faculty = coeur;
            sys466d.Faculty = coeur;
            sys466c.Faculty = coeur;
            context.Faculty.Add(coeur);

            Faculty denise = new Faculty("055555555", "Louis", "Sanders");
            eac397a.Faculty = denise;
            eac397b.Faculty = denise;
            eac397c.Faculty = denise;
            eac397d.Faculty = denise;
            context.Faculty.Add(denise);

            //Users -------------------------------------------------

            string password = "123456";

            var user_admin = new ApplicationUser();
            user_admin.UserName = "admin";
            UserManager.Create(user_admin, password);
            UserManager.AddToRole(user_admin.Id, "Admin");
            user_admin.FacultyId = -1;

            var user_ian = new ApplicationUser();
            user_ian.UserName = "jean";
            UserManager.Create(user_ian, password);
            UserManager.AddToRole(user_ian.Id, "Faculty");
            user_ian.FacultyId = jean.FacultyId;

            var user_brian = new ApplicationUser();
            user_brian.UserName = "sandrine";
            UserManager.Create(user_brian, password);
            UserManager.AddToRole(user_brian.Id, "Faculty");
            user_brian.FacultyId = sandrine.FacultyId;
            
            var user_harvey = new ApplicationUser();
            user_harvey.UserName = "henriquet";
            UserManager.Create(user_harvey, password);
            UserManager.AddToRole(user_harvey.Id, "Faculty");
            user_harvey.FacultyId = henriquet.FacultyId;

            var user_cindy = new ApplicationUser();
            user_cindy.UserName = "coeur";
            UserManager.Create(user_cindy, password);
            UserManager.AddToRole(user_cindy.Id, "Faculty");
            user_cindy.FacultyId = coeur.FacultyId;

            var user_denise = new ApplicationUser();
            user_denise.UserName = "denise";
            UserManager.Create(user_denise, password);
            UserManager.AddToRole(user_denise.Id, "Faculty");
            user_denise.FacultyId = denise.FacultyId;

            //Students ----------------------------------------------

            Communication cell = new Communication("cell", "");
            Communication email = new Communication("email", "");
            Communication twitter = new Communication("twitter", "");

            Student s1 = new Student("52222", "Martin", "William");
            s1.Communication.Add(new Communication("cell", "0000000001"));
            s1.Communication.Add(new Communication("email", "martin@gmail.com"));
            s1.Communication.Add(new Communication("twitter", "@Martin"));
            int422a.Students.Add(s1);
            jac444a.Students.Add(s1);
            dcn455a.Students.Add(s1);
            sys466a.Students.Add(s1);
            eac397a.Students.Add(s1);

            Student s2 = new Student("6666661", "Tom", "Chris");
            s2.Communication.Add(new Communication("cell", "5666666666"));
            s2.Communication.Add(new Communication("email", "join@yahoo.com"));
            s2.Communication.Add(new Communication("twitter", "@Chris"));
            int422a.Students.Add(s2);
            jac444a.Students.Add(s2);
            dcn455a.Students.Add(s2);
            sys466a.Students.Add(s2);
            eac397a.Students.Add(s2);

            Student s3 = new Student("566666", "Warren", "Edithe");
            s3.Communication.Add(new Communication("cell", "88888"));
            s3.Communication.Add(new Communication("email", "edith@yahoo.com"));
            s3.Communication.Add(new Communication("twitter", "@Warren"));
            int422a.Students.Add(s3);
            jac444a.Students.Add(s3);
            dcn455a.Students.Add(s3);
            sys466a.Students.Add(s3);
            eac397a.Students.Add(s3);

            Student s4 = new Student("999999", "Denzel", "Miami");
            s4.Communication.Add(new Communication("cell", "25666633"));
            s4.Communication.Add(new Communication("email", "miami@live.com"));
            s4.Communication.Add(new Communication("twitter", "@Denzel"));
            int422a.Students.Add(s4);
            jac444a.Students.Add(s4);
            dcn455a.Students.Add(s4);
            sys466a.Students.Add(s4);
            eac397a.Students.Add(s4);

            Student s5 = new Student("85555555", "Jan", "Etoo");
            s5.Communication.Add(new Communication("cell", "5555522222"));
            s5.Communication.Add(new Communication("email", "jjkkkk@temp.fr"));
            s5.Communication.Add(new Communication("twitter", "@Etoo"));
            int422a.Students.Add(s5);
            jac444a.Students.Add(s5);
            dcn455a.Students.Add(s5);
            sys466a.Students.Add(s5);
            eac397a.Students.Add(s5);

        }

    }

}