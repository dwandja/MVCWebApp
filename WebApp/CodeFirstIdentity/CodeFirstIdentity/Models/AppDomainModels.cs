using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstIdentity.Models
{
    public class Person
    {
        public Person()
        {
            FirstName = LastName = Phone = string.Empty;
        }

        public Person(string f, string l, string p)
        {
            FirstName = f;
            LastName = l;
            Phone = p;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
        public string Phone { get; set; }
    }

    public class Faculty : Person
    {
        public Faculty()
        {
            this.Courses = new List<Course>();
            FacultyNumber = string.Empty;
        }

        public Faculty(string f, string l, string p, string fid)
            : base(f, l, p)
        {
            this.Courses = new List<Course>();
            FacultyNumber = fid;
        }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string FacultyNumber { get; set; }

        public string School { get; set; }

        public List<Course> Courses { get; set; }
    }

    public class Student : Person
    {
        public Student()
        {
            this.Courses = new List<Course>();
            StudentNumber = string.Empty;
        }

        public Student(string f, string l, string p, string sid)
            : base(f, l, p)
        {
            this.Courses = new List<Course>();
            StudentNumber = sid;
        }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string StudentNumber { get; set; }

        public List<Course> Courses { get; set; }
    }
    public class Course
    {
        public Course()
        {
            this.Students = new List<Student>();
        }

        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public List<Student> Students { get; set; }
        public Faculty Faculty { get; set; }
    }

}