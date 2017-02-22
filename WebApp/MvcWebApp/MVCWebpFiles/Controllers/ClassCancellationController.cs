using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Controllers
{

    public class ClassCancellationController : Controller
    {

        public MyDbContext context = new MyDbContext();
        private ClassCancellation cancellation = new ClassCancellation();
        private Repo_ClassCancellation repo_cancellation = new Repo_ClassCancellation();

        [Authorize(Roles="Admin,Faculty")]
        public ActionResult Index()
        {
            ViewBag.dropdown_faculty = cancellation.getFacultyDropDownList();
            ViewBag.filter_date = "all";
            ViewBag.filter_faculty = "all";
            return View(repo_cancellation.getClassCancellationList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection _form)
        {
            ViewBag.dropdown_faculty = cancellation.getFacultyDropDownList();
            ViewBag.filter_date = _form[0];
            ViewBag.filter_faculty = _form[1];
            return View(repo_cancellation.getClassCancellationList(_form[0], _form[1]));
        }

        [Authorize(Roles = "Faculty")]
        public ActionResult Create()
        {
            ViewBag.dropdown_courses = cancellation.getCourseDropDownList();
            ViewBag.list_dates = cancellation.getDateSelectList();
            return View();
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            return View(repo_cancellation.getClassCancellationFull(id));
        }

        [HttpPost]
        public ActionResult Create(ClassCancellationFull _cancellation, FormCollection _form)
        {
            repo_cancellation.createClassCancellation(_cancellation, _form[1], _form[2], _form[3], _form[4]);
            return RedirectToAction("Index");
        }

	}

}