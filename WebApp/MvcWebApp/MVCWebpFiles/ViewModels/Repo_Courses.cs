using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INT422Project.Models;

namespace INT422Project.ViewModels
{

    public class Repo_Courses : Repository
    {

        public IEnumerable<CourseFull> getCourseFull()
        {

            var objs = context.Courses.Include("faculty").Include("students").OrderBy(n => n.courseId);

            List<CourseFull> list = new List<CourseFull>();

            foreach (var item in objs)
            {

                CourseFull course = new CourseFull();
                course.courseId = item.courseId;
                course.name = item.name;
                course.roomNumber = item.roomNumber;
                course.startDate = item.startDate;
                course.endDate = item.endDate;
                course.faculty = item.faculty;
                course.students = item.students;

                list.Add(course);

            }

            return list;

        }

        public CourseFull getCourseFull(int? id)
        {

            var obj = context.Courses.Include("faculty").Include("students").FirstOrDefault(n => n.courseId == id);

            CourseFull course = new CourseFull();
            course.courseId = obj.courseId;
            course.name = obj.name;
            course.roomNumber = obj.roomNumber;
            course.startDate = obj.startDate;
            course.endDate = obj.endDate;
            course.faculty = obj.faculty;
            course.students = obj.students;

            return course;

        }

        public IEnumerable<CourseList> getCourseList()
        {

            var all = context.Courses.OrderBy(n => n.name);

            List<CourseList> list = new List<CourseList>();

            foreach (var item in all)
            {

                CourseList course = new CourseList();
                course.courseId = item.courseId;
                course.name = item.name;
                course.roomNumber = item.roomNumber;
                course.startDate = item.startDate;
                course.endDate = item.endDate;

                list.Add(course);

            }

            return list;

        }

        public static List<CourseList> getCoursesForList(List<Course> courses)
        {

            List<CourseList> list = new List<CourseList>();

            foreach (var item in courses)
            {

                CourseList course = new CourseList();
                course.courseId = item.courseId;
                course.name = item.name;
                course.roomNumber = item.roomNumber;
                course.startDate = item.startDate;
                course.endDate = item.endDate;
                list.Add(course);

            }

            return list;

        }

        public CourseFull createCourse(CourseFull _course)
        {

            Course course = new Course(_course.name, _course.startDate, _course.endDate, _course.roomNumber);

            context.Courses.Add(course);
            context.SaveChanges();

            return _course;

        }

        public CourseFull createCourse(CourseFull _course, string _facultyId = null)
        {

            Course course = new Course(_course.name, _course.startDate, _course.endDate, _course.roomNumber);

            var facultyId = Convert.ToInt32(_facultyId);
            course.faculty = context.Faculty.FirstOrDefault(n2 => n2.facultyId == facultyId);

            context.Courses.Add(course);
            context.SaveChanges();

            return _course;

        }

    }

}