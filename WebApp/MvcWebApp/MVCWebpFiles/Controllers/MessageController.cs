using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebApp.Models;
using MvcWebApp.ViewModels;

namespace MvcWebApp.Controllers
{
    public class MessageController : Controller
    {

        private Repo_Message repo = new Repo_Message();
        private Message message = new Message();

        [Authorize(Roles="Admin,Faculty")]
        public ActionResult Index()
        {
            return View(repo.getMessageList());
        }

        [Authorize(Roles = "Admin,Faculty")]
        public ActionResult Details(int? id)
        {
            return View(repo.getMessageFull(id));
        }

	}

}