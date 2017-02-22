using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace MvcWebApp.Controllers
{

    public class FacultyController : Controller
    {

        private Repo_Faculty repo = new Repo_Faculty();
        private Faculty faculty = new Faculty();

        public ActionResult Index()
        {
            return View(repo.getFacultyList());
        }

        public ActionResult Details(int? id)
        {
            return View(repo.getFacultyFull(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.list_courses = faculty.getCoursesSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(FacultyFull _faculty, FormCollection _form)
        {

            if (_form.Count == 5)
            {
                repo.createFaculty(_faculty, _form[4]);
            }
            else
            {
                repo.createFaculty(_faculty);
            }

            return RedirectToAction("Index");

        }

	}

}