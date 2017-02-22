using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcWebApp.Models;
using System.Web.Security;
using AutoMapper;

namespace MvcWebApp.ViewModels
{

    public class Repo_ClassCancellation : Repository
    {

        public IEnumerable<ClassCancellationFull> getClassCancellationFull()
        {

            var objs = context.ClassCancellations.Include("Course").Include("Faculty").Include("Message").OrderBy(n => n.ClassCancellationId);

            List<ClassCancellationFull> list = new List<ClassCancellationFull>();

            foreach (var item in objs)
            {
                list.Add(Mapper.Map<ClassCancellationFull>(item));
            }

            return list;

        }

        public ClassCancellationFull getClassCancellationFull(int? id)
        {

            var obj = context.ClassCancellations.Include("Course").Include("Faculty").Include("Message").FirstOrDefault(n => n.ClassCancellationId == id);
            return Mapper.Map<ClassCancellationFull>(obj);

        }

        public IEnumerable<ClassCancellationList> getClassCancellationList()
        {

            var all = context.ClassCancellations.Include("Course").Include("Faculty").Include("Message").OrderBy(n => n.ClassCancellationId);

            List<ClassCancellationList> list = new List<ClassCancellationList>();

            foreach (var item in all)
            {                
                list.Add(Mapper.Map<ClassCancellationList>(item));
            }

            return list;

        }

        public IEnumerable<ClassCancellationList> getClassCancellationList(string _date = null, string _faculty = null)
        {

            var all = context.ClassCancellations.Include("Course").Include("Faculty").Include("Message").OrderBy(n => n.ClassCancellationId);
            List<ClassCancellationList> list = new List<ClassCancellationList>();

            //Check current user ------------------------------------

            string currentId = HttpContext.Current.User.Identity.GetUserId();
            bool admin = HttpContext.Current.User.IsInRole("Admin");
            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == currentId);
            Faculty faculty = context.Faculty.FirstOrDefault(x => x.FacultyId == user.FacultyId);

            //-------------------------------------------------------

            foreach (var item in all)
            {

                //Get dates
                int currentDate = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0'));
                int itemDate = Convert.ToInt32(item.Date.Year.ToString() + item.Date.Month.ToString().PadLeft(2, '0') + item.Date.Day.ToString().PadLeft(2, '0'));

                if ((_date == "past" && itemDate < currentDate) || (_date == "today" && itemDate == currentDate) || (_date == "future" && itemDate > currentDate) || _date == "all")
                {

                    if (Convert.ToInt32(_faculty) == faculty.FacultyId)
                    {
                        list.Add(Mapper.Map<ClassCancellationList>(item));
                    }

                }


            }

            return list;

        }

        public ClassCancellationFull createClassCancellation(ClassCancellationFull _cancellation, string _courseId = null, string _date = null, string _message = null, string _useMessage = "false")
        {

            //Check current user ------------------------------------

            string currentId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == currentId);
            Faculty faculty = context.Faculty.FirstOrDefault(x => x.FacultyId == user.FacultyId);

            //-------------------------------------------------------

            //Get a list of dates from _date (form)
            List<DateTime> list = new List<DateTime>();
            var nums = _date.Split(',');

            foreach (var item in nums)
            {
                list.Add(Convert.ToDateTime(item));
            }

            //Create new ClassCancellation for each date
            foreach (var item in list)
            {

                ClassCancellation cancellation = new ClassCancellation();

                var courseId = Convert.ToInt32(_courseId);

                cancellation.Date = Convert.ToDateTime(item);
                cancellation.Course = context.Courses.FirstOrDefault(x => x.CourseId == courseId);
                cancellation.Faculty = faculty;                
                if (_useMessage == "false") cancellation.UseCustomMessage = false;
                else cancellation.UseCustomMessage = true;

                Message message;
                string std = faculty.FirstName + " " + faculty.LastName + ", " + cancellation.Course.Name + ", " + cancellation.Date;
                message = new Message(std, _message);
                message.Faculty = faculty;

                cancellation.Message = message;

                context.Messages.Add(message);
                context.ClassCancellations.Add(cancellation);
                context.SaveChanges();

            }

            /*
            ClassCancellation cancellation = new ClassCancellation();
            var courseId = Convert.ToInt32(_courseId);
            cancellation.Date = Convert.ToDateTime(_date);
            cancellation.Course = context.Courses.FirstOrDefault(x => x.CourseId == courseId);
            cancellation.Faculty = faculty;
            string std = faculty.FirstName + " " + faculty.LastName + " > " + cancellation.Course.Name + " > " + cancellation.Date;
            Message message = new Message(faculty, std, _message);
            message.Faculty = faculty;
            cancellation.Message = message;
            context.Messages.Add(message);
            context.ClassCancellations.Add(cancellation);
            context.SaveChanges();            
            */

            return _cancellation;

        }

    }

}