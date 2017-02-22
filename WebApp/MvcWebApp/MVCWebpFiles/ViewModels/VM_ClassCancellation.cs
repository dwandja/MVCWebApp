using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcWebApp.Models;

namespace MvcWebApp.ViewModels
{

    public class ClassCancellationFull : ClassCancellationList
    {

        [Required]
        public Message Message { get; set; }

        public ClassCancellationFull()
        {

            Message = null;

        }

    }

    public class ClassCancellationList
    {

        [Key]
        public int ClassCancellationId { get; set; }

        [Required, Display(Name = "Cancel Date")]
        public DateTime Date { get; set; }

        [Required, Display(Name = "Course")]
        public Course Course { get; set; }

        [Required, Display(Name = "Faculty")]
        public Faculty Faculty { get; set; }

        [Display(Name = "Use Custom Message")]
        public bool UseCustomMessage { get; set; }

    }

}