using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using MvcWebApp.Models;
using AutoMapper;

namespace MvcWebApp
{
    
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<MyDbContext>(new MyDbInitializer());

            //Automapper --------------------------------------------

            //To ViewModel classes
            Mapper.CreateMap<Models.Faculty, ViewModels.FacultyList>();
            Mapper.CreateMap<Models.Faculty, ViewModels.FacultyFull>();            
            Mapper.CreateMap<Models.Student, ViewModels.StudentList>();
            Mapper.CreateMap<Models.Student, ViewModels.StudentFull>();
            Mapper.CreateMap<Models.Course, ViewModels.CourseList>();
            Mapper.CreateMap<Models.Course, ViewModels.CourseFull>();
            Mapper.CreateMap<Models.ClassCancellation, ViewModels.ClassCancellationList>();
            Mapper.CreateMap<Models.ClassCancellation, ViewModels.ClassCancellationFull>();

            //From ViewModel classes
            Mapper.CreateMap<ViewModels.FacultyList, Models.Faculty>();
            Mapper.CreateMap<ViewModels.FacultyFull, Models.Faculty>();
            Mapper.CreateMap<ViewModels.StudentList, Models.Student>();
            Mapper.CreateMap<ViewModels.StudentFull, Models.Student>();
            Mapper.CreateMap<ViewModels.CourseList, Models.Course>();
            Mapper.CreateMap<ViewModels.CourseFull, Models.Course>();
            Mapper.CreateMap<ViewModels.ClassCancellationList, Models.ClassCancellation>();
            Mapper.CreateMap<ViewModels.ClassCancellationFull, Models.ClassCancellation>();

            //-------------------------------------------------------


        }

    }

}
