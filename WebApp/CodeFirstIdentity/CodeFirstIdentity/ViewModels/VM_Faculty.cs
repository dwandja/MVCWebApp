using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CodeFirstIdentity.ViewModels
{
    // StudentBase is used by Student-related ViewModels 
    public class FacultyBase
    {
        [Key]
        public int FacultyId { get; set; }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string FacultyNumber { get; set; }
    }

    // adds FirstName and LastName of StudentBase
    // this ViewModel class demonstrates a composite field too
    public class FacultyName : FacultyBase
    {
        // composite ViewModel field consisting of 
        // FirstName and LastName that is used in Index.cshtml
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

    // StudentFull has the same information as Models.Student
    // we use this object when get information to create a student
    public class FacultyFull : FacultyName
    {
        public FacultyFull()
            : base()
        {
            this.Courses = new List<CourseFull>();
        }

        [Required]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
        public string Phone { get; set; }
        public List<CourseFull> Courses { get; set; }
    }

    // Http GET method of Student/Create sends this object to the browser
    public class FacultyCreateForHttpGet
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string FacultyNumber { get; set; }

        public SelectList CourseSelectList { get; set; }

        public void Clear()
        {
            FirstName = LastName = Phone = FacultyNumber = string.Empty;
        }
    }

    // Http POST method of Student/Create recieves this object from the browser
    public class FacultyCreateForHttpPost
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[2-9]\\d{2}-\\d{3}-\\d{4}$", ErrorMessage = "nnn-nnn-nnnn")]
        public string Phone { get; set; }

        [Required]
        [RegularExpression("^[0][0-9]{8}$", ErrorMessage = "0 followed by 8 digits")]
        public string FacultyNumber { get; set; }

        [Required(ErrorMessage = "Student should be in at lest one Course")]
        public virtual ICollection<int> CourseIds { get; set; }


    }

}