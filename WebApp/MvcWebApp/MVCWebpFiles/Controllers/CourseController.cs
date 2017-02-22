using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Controllers
{

    public class CourseController : Controller
    {

        private Repo_Course repo = new Repo_Course();
        private Course course = new Course();

        [Authorize]
        public ActionResult Index()
        {
            return View(repo.getCourseList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            return View(repo.getCourseFull(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.dropdown_faculty = course.getFacultyDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CourseFull _course, FormCollection _form)
        {
            repo.createCourse(_course, _form[4]);
            return RedirectToAction("Index");
        }

	}

}