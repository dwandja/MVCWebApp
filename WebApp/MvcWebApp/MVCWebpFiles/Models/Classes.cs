using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Models
{

    public class Faculty
    {

        public MyDbContext Context = new MyDbContext();

        [Key]
        public int FacultyId { get; set; }

        [Required]
        public string SenecaId { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        public List<Course> Courses { get; set; }
        public List<Message> Messages { get; set; }

        public Faculty()
        {

            SenecaId = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Courses = new List<Course>();
            Messages = new List<Message>();

        }

        public Faculty(string _senecaId, string _firstName, string _lastName)
        {

            SenecaId = _senecaId;
            FirstName = _firstName;
            LastName = _lastName;
            Courses = new List<Course>();
            Messages = new List<Message>();

        }

        public SelectList getCoursesSelectList()
        {

            SelectList list = new SelectList(Context.Courses, "CourseId", "Name");
            return list;

        }

    }

    public class Student
    {

        [Key]
        public int StudentId { get; set; }

        [Required]
        public string SenecaId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<Communication> Communication { get; set; }
        public List<Course> Courses { get; set; }
        public List<Message> Messages { get; set; }

        public Student()
        {

            SenecaId = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Communication = new List<Communication>();
            Courses = new List<Course>();
            Messages = new List<Message>();

        }

        public Student(string _senecaId, string _firstName, string _lastName)
        {

            SenecaId = _senecaId;
            FirstName = _firstName;
            LastName = _lastName;
            Communication = new List<Communication>();
            Courses = new List<Course>();
            Messages = new List<Message>();

        }

    }

    public class Course
    {

        public MyDbContext Context = new MyDbContext();

        [Key]
        public int CourseId { get; set; }

        [Required, Display(Name = "Course Name")]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        public ApplicationUser User { get; set; }
        public Faculty Faculty { get; set; }
        public List<Student> Students { get; set; }

        public Course()
        {

            Name = string.Empty;
            StartDate = new DateTime(2014, 1, 1);
            EndDate = new DateTime(2014, 1, 1);
            RoomNumber = string.Empty;
            User = null;
            Faculty = null;
            Students = new List<Student>();

        }

        public Course(string _name, DateTime _startDate, DateTime _endDate, string _roomNumber)
        {

            Name = _name;
            StartDate = _startDate;
            EndDate = _endDate;
            RoomNumber = _roomNumber;
            User = null;
            Faculty = null;
            Students = new List<Student>();

        }

        public SelectList getFacultyDropDownList()
        {

            SelectList list = new SelectList(Context.Faculty, "FacultyId", "LastName");
            return list;

        }

    }

    public class ClassCancellation
    {

        public MyDbContext Context = new MyDbContext();

        [Key]
        public int ClassCancellationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Course Course { get; set; }

        [Required]
        public Faculty Faculty { get; set; }       

        [Required]
        public Message Message { get; set; }

        public bool UseCustomMessage { get; set; }

        public ClassCancellation()
        {

            Date = DateTime.Now;
            Course = null;
            Faculty = null;                        
            Message = null;
            UseCustomMessage = true;

        }

        public SelectList getCourseDropDownList()
        {

            Repo_Course repo = new Repo_Course();

            SelectList list = new SelectList(repo.getCourseList(), "CourseId", "Name");
            return list;

        }

        public SelectList getFacultyDropDownList()
        {

            SelectList list = new SelectList(Context.Faculty, "FacultyId", "LastName");
            return list;

        }

        public SelectList getDateSelectList()
        {

            List<DateTime> dates = new List<DateTime>();

            DateTime cur = DateTime.Now;
            DateTime end = new DateTime(2014, 5, 1);

            //Add new date from today until course end date is reached
            while (cur.CompareTo(end) < 0)
            {
                dates.Add(new DateTime(cur.Year, cur.Month, cur.Day));
                cur = cur.AddDays(1);
            }

            SelectList list = new SelectList(dates, "Date", "Date");
            
            return list;

        }

    }

    public class Message
    {

        [Key]
        public int MessageId { get; set; }

        [Required, Display(Name = "Standard Message")]
        public string StandardMessage { get; set; }

        [Required, Display(Name = "Custom Message")]
        public string CustomMessage { get; set; }

        public Faculty Faculty { get; set; }

        public Message()
        {

            StandardMessage = string.Empty;
            CustomMessage = string.Empty;
            Faculty = null;

        }

        public Message(string _standardMessage, string _customMessage)
        {

            StandardMessage = _standardMessage;
            CustomMessage = _customMessage;
            Faculty = null;

        }

    }

    public class Communication
    {

        [Key]
        public int CommunicationId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Data { get; set; }

        public Communication()
        {

            Type = string.Empty;
            Data = string.Empty;

        }

        public Communication(string _type, string _data)
        {

            Type = _type;
            Data = _data;

        }

    }

}