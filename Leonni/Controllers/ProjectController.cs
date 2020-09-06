using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using Leonni.Core.Controllers;

namespace Leonni.Controllers
{
    public class ProjectController : LeonniApplicationController
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Projects";
            return View(new DefaultBaseViewModel());
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}
