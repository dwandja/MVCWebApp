using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcWebApp.Models;

namespace MvcWebApp.ViewModels
{

    public class CourseFull : CourseList
    {

        [Display(Name = "Students")]
        public List<Student> Students { get; set; }

        [Display(Name = "Faculty")]
        public Faculty Faculty { get; set; }

        public CourseFull()
        {

            Faculty = null;
            Students = new List<Student>();

        }

    }

    public class CourseList
    {

        [Key]
        public int CourseId { get; set; }

        [Required, Display(Name = "Course Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required, Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required, Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

    }

}