using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CodeFirstIdentity.Models
{
    public class Initializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext dc)
        {
           var people = InitializeAppDomain(dc);

           // Administrators have no courses to teach
               people.Add(new Person() { 
               FirstName = "Admin", 
               LastName = "Administrator", 
               Phone = "416-491-5050" 
           });
           
           people.Add(new Person() {
               FirstName = "Ian",
               LastName = "Tipson",
               Phone = "416-491-5050"
           });

          InitializeIdentity(dc, people);
          base.Seed(dc);

        }
      
        private List<Person> InitializeAppDomain(DataContext dc)
        {
            var  people =  new List<Person>();

            Course int422 = new Course();
            int422.CourseCode = "INT422";
            int422.CourseName = "Windows Web Programming";

            Course jac444 = new Course();
            jac444.CourseName = "Java";
            jac444.CourseCode = "JAC444";

            // according to our AppDomainModels all properties are required
            Student student = new Student();
            student.Id = 1;
            student.FirstName = "Bob";
            student.LastName = "Smith";
            student.Phone = "555-555-5555";
            student.StudentNumber = "011111111";
            student.Courses.Add(int422);

            dc.Students.Add(student);
            int422.Students.Add(student);

            people.Add(student);
            student = null;

            student = new Student();
            student.Id = 2;
            student.FirstName = "Mary";
            student.LastName = "Brown";
            student.Phone = "555-555-5555";
            student.StudentNumber = "011111112";
            student.Courses.Add(int422);

            dc.Students.Add(student);
            int422.Students.Add(student);

            people.Add(student);
            student = null;

            student = new Student();
            student.Id = 3;
            student.FirstName = "Wei";
            student.LastName = "Chen";
            student.Phone = "555-555-5555";
            student.StudentNumber = "011111113";
            student.Courses.Add(jac444);

            dc.Students.Add(student);
            jac444.Students.Add(student);

            people.Add(student);
            student = null;

            student = new Student("John", "Woo", "555-555-1234", "011111114");
            student.Id = 4;
            student.Courses.Add(jac444);
            student.Courses.Add(int422);

            dc.Students.Add(student);
            int422.Students.Add(student);
            jac444.Students.Add(student);
            dc.Courses.Add(int422);
            dc.Courses.Add(jac444);

            people.Add(student);
            student = null;

            Faculty fac = new Faculty("Peter", "McIntyre", "555-567-6789", "034234678");
            fac.Courses.Add(jac444);
            dc.Faculty.Add(fac);

            people.Add(fac);
            fac = null;

            fac = new Faculty("Mark", "Fernandes", "416-491-5050", "098765432");
            fac.Courses.Add(int422);
            dc.Faculty.Add(fac);

            people.Add(fac);
            student = null;

            dc.SaveChanges();

            return people;
        }

        private void InitializeIdentity(DataContext context, List<Person> people)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            // Admin is a user and a Role.
            string password = "123456";
            var person = people.FirstOrDefault(p => p.FirstName == "Admin");
            _createUserInRole(person, password, "Admin", UserManager, RoleManager);
            people.Remove(person);
            person = null;

            // Create user ian  with password=123456 and put into Admin
            person = people.FirstOrDefault(p => p.LastName == "Tipson");
            _createUserInRole(person, password, "Admin",UserManager, RoleManager);
            people.Remove(person);
            person = null;

            // Create User mark with password=123456 and add to INT422 role
            person = people.FirstOrDefault(p => p.LastName == "Fernandes");
            _createUserInRole(person, password, "INT422", UserManager, RoleManager);
            people.Remove(person);
            person = null;

            // Create User peter with password=123456 and add to INT422 role
            person = people.FirstOrDefault(p => p.LastName == "McIntyre");
            _createUserInRole(person, password, "INT422", UserManager, RoleManager);
            people.Remove(person);
            person = null;

            // rest are students (HINT: you could create a separate role for all of them)
            foreach (var student in people) 
            {
                var user = new ApplicationUser();
                user.UserName = student.FirstName.ToLower();
                user.Person = student;
                try
                {
                    var userresult = UserManager.Create(user, password);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    _logErrorsToFile(e);
                    throw;
                }
            }

        }

        // helper functions

        private void _createUserInRole(Person person, string password, string role,
            UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm) 
        {
            // Create Role if it does not exist
            if (!rm.RoleExists(role))
            {
                var roleresult = rm.Create(new IdentityRole(role));
            }

            // Create User if it does not exist
            var user = new ApplicationUser();

            if (person.FirstName == "Admin")
            {
              user.UserName = "Admin";
            }
            else
            {
              user.UserName = person.FirstName.ToLower();
            }
            user.Person = person;
            try
            {
                var userCreate = um.Create(user, password);
                if (userCreate.Succeeded)
                {
                  var result = um.AddToRole(user.Id, role);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                _logErrorsToFile(e);
                throw;
            }

        }
        private void _logErrorsToFile(System.Data.Entity.Validation.DbEntityValidationException e)
        {
            var logFile = HttpContext.Current.Server.MapPath("~/App_Data/error_log.txt");
            System.Diagnostics.TextWriterTraceListener logger;
            using (logger = new System.Diagnostics.TextWriterTraceListener(logFile, "My Log Name"))
            {
                logger.WriteLine(DateTime.Now + " : " + e.Message);
                foreach (var eve in e.EntityValidationErrors)
                {
                    string str = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    logger.WriteLine(str);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        str = String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        logger.WriteLine(str);
                    }
                }
            }
        }
    }
}