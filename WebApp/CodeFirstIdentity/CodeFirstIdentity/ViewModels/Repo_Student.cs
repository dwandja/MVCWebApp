using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeFirstIdentity.Models;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodeFirstIdentity.ViewModels
{
    public class Repo_Student : RepositoryBase
    {
        // Get StudentName according to role of logged in user
        public List<StudentName> getListOfStudentName(IPrincipal user)
        {
            // get all students for Admin
            if (user.IsInRole("Admin"))
            {
              var students = dc.Students.OrderBy(n => n.LastName);
              return _getListOfStudentName(students);
            }

            // who is this ApplicationUser?
            var au = um.FindById(user.Identity.GetUserId());

            // get list of faculty students, by looking at what courses they teach
            if (user.IsInRole("INT422"))
            {
              var faculty = dc.Faculty.Include("Courses").FirstOrDefault(fac => fac.Id == au.Person.Id);

              List<StudentName> facStudents = new List<StudentName>();
              foreach (var course in faculty.Courses)
              {
                var cs = dc.Courses.Include("Students").FirstOrDefault(c => c.CourseCode == course.CourseCode);
                var students =  cs.Students.OrderBy(n => n.LastName);
                facStudents.AddRange(_getListOfStudentName(students));
              }

              return facStudents;
            }
            // not admin, nor faculty, so this must be student
            // get only details for this one student
            else
            {
              StudentName stu = new StudentName();

              var st = dc.Students.FirstOrDefault(s => s.Id == au.Person.Id);

              stu.StudentId = st.Id;
              stu.FirstName = st.FirstName;
              stu.LastName = st.LastName;

              var rls = new List<StudentName>();
              rls.Add(stu);
              return rls;
            }
        }

        
        public List<StudentFull> getListOfStudentFull()
        {
            var st = dc.Students.Include("Courses").OrderBy(n => n.LastName);

            List<StudentFull> rls = new List<StudentFull>();

            foreach (var item in st)
            {
                StudentFull stu = new StudentFull();
                stu.StudentId = item.Id;
                stu.FirstName = item.FirstName;
                stu.LastName = item.LastName;
                stu.Phone = item.Phone;
                stu.StudentNumber = item.StudentNumber;
                stu.Courses = Repo_Courses.getListOfCourseFull(item.Courses);
                rls.Add(stu);
            }

            return rls;

        }

        public StudentFull getStudentFull(int? id)
        {
            // handles situations without an id, like /Student/Details
            if (id == null) return null;

            var maxId = dc.Students.Max(s => s.Id);
            if (id.HasValue && id > 0 && id <= maxId)
            {
              var st = dc.Students.Include("Courses").FirstOrDefault(n => n.Id == id);
              if (st == null) return null;

              StudentFull stu = new StudentFull();
              stu.StudentId = st.Id;
              stu.FirstName = st.FirstName;
              stu.LastName = st.LastName;
              stu.Phone = st.Phone;
              stu.StudentNumber = st.StudentNumber;
              stu.Courses = Repo_Courses.getListOfCourseFull(st.Courses);

              return stu;
            }
            return null;
        }

      
        public StudentFull createStudent(StudentCreateForHttpPost newItem)
        {

            var student = new Student();

            student.FirstName = newItem.FirstName;
            student.LastName = newItem.LastName;
            student.Phone = newItem.Phone;
            student.StudentNumber = newItem.StudentNumber;

            foreach (var item in newItem.CourseIds)
            {
                var c = dc.Courses.Find(item);
                student.Courses.Add(c);
            }

            dc.Students.Add(student);

            //savechanges is the equivalent to a database commit statement
            dc.SaveChanges();

            //return a copy of the new Student as a StudentFull
            var newId = dc.Students.Max(s => s.Id);
            return getStudentFull(newId);

        }

        // this is essentailly a ViewModel because the SelectList only 
        // carries the data needed to display a list control
        public SelectList getSelectList()
        {
            SelectList sl = new SelectList(dc.Courses, "Id", "CourseCode");
            return sl;
        }

        // Implementation details
        private List<StudentName> _getListOfStudentName(IEnumerable<Student> students)
        {
            List<StudentName> rls = new List<StudentName>();
            foreach (var item in students)
            {
                StudentName stu = new StudentName();
                stu.StudentId = item.Id;
                stu.FirstName = item.FirstName;
                stu.LastName = item.LastName;
                rls.Add(stu);
            }
            return rls;
        }

        public Repo_Student()
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