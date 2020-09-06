using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using Leonni.Core.Controllers;

namespace Leonni.Controllers
{
    public class GuideController : LeonniApplicationController
    {
        //
        // GET: /Guide/

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Groups";
            return View(new DefaultBaseViewModel());
        }

    }
}
