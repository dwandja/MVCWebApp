using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeFirstIdentity.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirstIdentity.ViewModels
{
    public class Repo_Faculty : RepositoryBase
    {
        // Get StudentName according to role of logged in user
        public List<FacultyName> getListOfFacultyName(IPrincipal user)
        {
            // get all students for Admin
            //   if (user.IsInRole("Admin"))
            {
                var faculty = dc.Faculty.OrderBy(n => n.LastName);
                return _getListOfFacultyName(faculty);
            }
        }
            // who is this ApplicationUser?
   /*        var au = um.FindById(user.Identity.GetUserId());

             //  get list of faculty students, by looking at what courses they teach
             if (user.IsInRole("INT422"))
                {
                   var faculty = dc.Faculty.Include("Courses").FirstOrDefault(fac => fac.Id == au.Person.Id);

               List<StudentName> facStudents = new List<StudentName>();
                foreach (var course in faculty.Courses)
                {
                    var cs = dc.Courses.Include("Students").FirstOrDefault(c => c.CourseCode == course.CourseCode);
                    var students = cs.Students.OrderBy(n => n.LastName);
                    facStudents.AddRange(_getListOfStudentName(students));
                }

                return facStudents;
            }
            // not admin, nor faculty, so this must be student
            // get only details for this one student
           //1 else
             { 
                FacultyName stu = new FacultyName();

                var st = dc.Faculty.FirstOrDefault(s => s.Id == au.Person.Id);

                 stu.FacultyId = st.Id;
                stu.FirstName = st.FirstName;
                stu.LastName = st.LastName;

                var rls = new List<FacultyName>();
                rls.Add(stu);
                return rls;
            }
        }

*/
        public List<FacultyFull> getListOfFacultyFull()
        {
            var st = dc.Faculty.Include("Courses").OrderBy(n => n.LastName);

            List<FacultyFull> rls = new List<FacultyFull>();

            foreach (var item in st)
            {
                FacultyFull stu = new FacultyFull();
                stu.FacultyId = item.Id;
                stu.FirstName = item.FirstName;
                stu.LastName = item.LastName;
                stu.Phone = item.Phone;
                stu.FacultyNumber = item.FacultyNumber;
                stu.Courses = Repo_Courses.getListOfCourseFull(item.Courses);
                rls.Add(stu);
            }

            return rls;

        }

        public FacultyFull getFacultyFull(int? id)
        {
            // handles situations without an id, like /Student/Details
            if (id == null) return null;

            var maxId = dc.Faculty.Max(s => s.Id);
            if (id.HasValue && id > 0 && id <= maxId)
            {
                var st = dc.Faculty.Include("Courses").FirstOrDefault(n => n.Id == id);
                if (st == null) return null;

                FacultyFull stu = new FacultyFull();
                stu.FacultyId = st.Id;
                stu.FirstName = st.FirstName;
                stu.LastName = st.LastName;
                stu.Phone = st.Phone;
                stu.FacultyNumber = st.FacultyNumber;
                stu.Courses = Repo_Courses.getListOfCourseFull(st.Courses);

                return stu;
            }
            return null;
        }


        public FacultyFull createFaculty(FacultyCreateForHttpPost newItem)
        {

            var faculty = new Faculty();

            faculty.FirstName = newItem.FirstName;
            faculty.LastName = newItem.LastName;
            faculty.Phone = newItem.Phone;
            faculty.FacultyNumber = newItem.FacultyNumber;

            foreach (var item in newItem.CourseIds)
            {
                var c = dc.Courses.Find(item);
                faculty.Courses.Add(c);
            }

            dc.Faculty.Add(faculty);

            //savechanges is the equivalent to a database commit statement
            dc.SaveChanges();

            //return a copy of the new Student as a StudentFull
            var newId = dc.Students.Max(s => s.Id);
            return getFacultyFull(newId);

        }

        // this is essentailly a ViewModel because the SelectList only 
        // carries the data needed to display a list control
        public SelectList getSelectList()
        {
            SelectList sl = new SelectList(dc.Courses, "Id", "CourseCode");
            return sl;
        }

        // Implementation details
        private List<FacultyName> _getListOfFacultyName(IEnumerable<Faculty> faculty)
        {
            List<FacultyName> rls = new List<FacultyName>();
            foreach (var item in faculty)
            {
                FacultyName stu = new FacultyName();
                stu.FacultyId = item.Id;
                stu.FirstName = item.FirstName;
                stu.LastName = item.LastName;
                rls.Add(stu);
            }
            return rls;
        }

        public Repo_Faculty()
        {
            um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        private UserManager<ApplicationUser> um;
    }
}


/*  UNUSED CODE: it still works but this was the older way of creating new items.
 * public StudentFull createStudent(StudentFull st, string ids)
        {
            //instantiate new student
            Student stu = new Student(st.FirstName, st.LastName, st.Phone, st.StudentNumber);

            //create a list of ints
            List<Int32> ls = new List<int>();

            //the format of ids is ("n,n,n,...") where n is an numeric character
            //split the string into an array of individual characters
            var nums = ids.Split(',');

            //convert each character to an int32 and store in ls
            foreach (var item in nums)
            {
                ls.Add(Convert.ToInt32(item));
            }

            //iterate through ls and for each id in the list, add a Course to the student's Courses collection
            foreach (var item in ls)
            {
                stu.Courses.Add(dc.Courses.FirstOrDefault(n => n.Id == item));
            }
            //add the student to the DataContext
            dc.Students.Add(stu);

            //savechanges is the equivalent to a database commit statement
            dc.SaveChanges();

            // last Student added was ours
            stu.Id = dc.Students.Max(s => s.Id);

            //return a copy of the new Student as a StudentFull
            return getStudentFull(stu.Id);
        }
        public StudentFull createStudent(StudentFull st)
        {
            //instantiate new student
            Student stu = new Student(st.FirstName, st.LastName, st.Phone, st.StudentNumber);

            dc.Students.Add(stu);

            //savechanges is the equivalent to a database commit statement
            dc.SaveChanges();

            // last Student added was ours
            stu.Id = dc.Students.Max(s => s.Id);

            //return a copy of the new Student as a StudentFull
            return getStudentFull(stu.Id);
        }
*/