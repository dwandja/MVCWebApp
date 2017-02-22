using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;

namespace MvcWebApp.ViewModels
{

    public class Repo_Faculty : Repository
    {

        public IEnumerable<FacultyFull> getFacultyFull()
        {

            var objs = context.Faculty.OrderBy(n => n.SenecaId);

            List<FacultyFull> list = new List<FacultyFull>();

            foreach (var item in objs)
            {

                FacultyFull faculty = new FacultyFull();
                faculty.FacultyId = item.FacultyId;
                faculty.SenecaId = item.SenecaId;
                faculty.FirstName = item.FirstName;
                faculty.LastName = item.LastName;
                faculty.Courses = Repo_Course.getCoursesForList(item.Courses);

                list.Add(faculty);

            }

            return list;

        }

        public FacultyFull getFacultyFull(int? id)
        {

            var obj = context.Faculty.Include("Courses").FirstOrDefault(n => n.FacultyId == id);
            return Mapper.Map<FacultyFull>(obj);

        }

        public IEnumerable<FacultyList> getFacultyList()
        {

            var all = context.Faculty.OrderBy(n => n.SenecaId);

            List<FacultyList> list = new List<FacultyList>();

            foreach (var item in all)
            {

                list.Add(Mapper.Map<FacultyList>(item));

                /*
                FacultyList faculty = new FacultyList();
                faculty.FacultyId = item.FacultyId;
                faculty.SenecaId = item.SenecaId;
                faculty.FirstName = item.FirstName;
                faculty.LastName = item.LastName;
                list.Add(faculty);
                */

            }

            return list;

        }

        public FacultyFull createFaculty(FacultyFull _faculty)
        {

            Faculty faculty = new Faculty(_faculty.SenecaId, _faculty.FirstName, _faculty.LastName);

            context.Faculty.Add(faculty);
            context.SaveChanges();

            return _faculty;

        }

        public FacultyFull createFaculty(FacultyFull _faculty, string _courses = null)
        {

            Faculty faculty = new Faculty(_faculty.SenecaId, _faculty.FirstName, _faculty.LastName);

            List<Int32> ls = new List<int>();
            var nums = _courses.Split(',');
            foreach (var item in nums)
            {
                ls.Add(Convert.ToInt32(item));
            }
            foreach (var item in ls)
            {
                faculty.Courses.Add(context.Courses.FirstOrDefault(n => n.CourseId == item));
            }

            context.Faculty.Add(faculty);
            context.SaveChanges();

            return _faculty;

        }

    }

}