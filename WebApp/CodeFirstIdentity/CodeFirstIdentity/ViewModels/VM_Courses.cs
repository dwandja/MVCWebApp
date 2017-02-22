using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstIdentity.ViewModels
{
    // CourseFull has same information as Models.Course
    public class CourseFull
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
    }
}