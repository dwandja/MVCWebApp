using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Controllers
{

    public class StudentController : Controller
    {

        private Repo_Student repo = new Repo_Student();

        public ActionResult Index()
        {
            return View(repo.getStudentList());
        }

        public ActionResult Details(int? id)
        {
            return View(repo.getStudentFull(id));
        }

	}

}