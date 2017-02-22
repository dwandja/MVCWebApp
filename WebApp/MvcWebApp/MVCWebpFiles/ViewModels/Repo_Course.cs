using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MvcWebApp.Models;
using System.Web.Security;
using AutoMapper;

namespace MvcWebApp.ViewModels
{

    public class Repo_Course : Repository
    {

        public IEnumerable<CourseFull> getCourseFull()
        {

            var objs = context.Courses.Include("Faculty").Include("Students").OrderBy(n => n.CourseId);

            List<CourseFull> list = new List<CourseFull>();

            foreach (var item in objs)
            {

                CourseFull course = new CourseFull();
                course.CourseId = item.CourseId;
                course.Name = item.Name;
                course.RoomNumber = item.RoomNumber;
                course.StartDate = item.StartDate;
                course.EndDate = item.EndDate;
                course.Faculty = item.Faculty;
                course.Students = item.Students;

                list.Add(course);

            }

            return list;

        }

        public CourseFull getCourseFull(int? id)
        {

            var obj = context.Courses.Include("Faculty").Include("Students").FirstOrDefault(n => n.CourseId == id);

            return Mapper.Map<CourseFull>(obj);

        }

        public IEnumerable<CourseList> getCourseList()
        {

            var all = context.Courses.OrderBy(n => n.Name);
            List<CourseList> list = new List<CourseList>();

            //Check current user ------------------------------------
            
            string currentId = HttpContext.Current.User.Identity.GetUserId();
            bool admin = HttpContext.Current.User.IsInRole("Admin");
            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == currentId);
            Faculty faculty = context.Faculty.FirstOrDefault(x => x.FacultyId == user.FacultyId);

            //-------------------------------------------------------

            foreach (var item in all)
            {

                if (!admin)
                {


                    if (item.Faculty != null && faculty.FacultyId == item.Faculty.FacultyId)
                    {

                        CourseList course = new CourseList();
                        course.CourseId = item.CourseId;
                        course.Name = item.Name;
                        course.RoomNumber = item.RoomNumber;
                        course.StartDate = item.StartDate;
                        course.EndDate = item.EndDate;

                        list.Add(course);

                    }

                }
                else
                {

                    CourseList course = new CourseList();
                    course.CourseId = item.CourseId;
                    course.Name = item.Name;
                    course.RoomNumber = item.RoomNumber;
                    course.StartDate = item.StartDate;
                    course.EndDate = item.EndDate;

                    list.Add(course);

                }

            }

            return list;

        }

        public static List<CourseList> getCoursesForList(List<Course> courses)
        {

            List<CourseList> list = new List<CourseList>();

            foreach (var item in courses)
            {

                CourseList course = new CourseList();
                course.CourseId = item.CourseId;
                course.Name = item.Name;
                course.RoomNumber = item.RoomNumber;
                course.StartDate = item.StartDate;
                course.EndDate = item.EndDate;

                list.Add(course);

            }

            return list;

        }

        public CourseFull createCourse(CourseFull _course)
        {

            Course course = new Course(_course.Name, _course.StartDate, _course.EndDate, _course.RoomNumber);

            context.Courses.Add(course);
            context.SaveChanges();

            return _course;

        }

        public CourseFull createCourse(CourseFull _course, string _facultyId = null)
        {

            Course course = new Course(_course.Name, _course.StartDate, _course.EndDate, _course.RoomNumber);

            var facultyId = Convert.ToInt32(_facultyId);
            course.Faculty = context.Faculty.FirstOrDefault(n2 => n2.FacultyId == facultyId);

            context.Courses.Add(course);
            context.SaveChanges();

            return _course;

        }

    }

}