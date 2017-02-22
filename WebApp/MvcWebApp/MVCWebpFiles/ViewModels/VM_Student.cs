using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcWebApp.Models;

namespace MvcWebApp.ViewModels
{

    public class StudentFull : StudentList
    {

        [Display(Name = "Communication")]
        public List<Communication> Communication { get; set; }

        [Display(Name = "Courses")]
        public List<Course> Courses { get; set; }

        [Display(Name = "Messages")]
        public List<Message> Messages { get; set; }

        public StudentFull()
        {

            Communication = new List<Communication>();
            Courses = new List<Course>();
            Messages = new List<Message>();

        }

    }

    public class StudentList
    {

        [Key]
        public int StudentId { get; set; }

        [Required, Display(Name = "Seneca ID"), RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string SenecaId { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

    }

}