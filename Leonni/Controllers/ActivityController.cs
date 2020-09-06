using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;

namespace Leonni.Controllers
{
    public class ActivityController : LeonniApplicationController
    {
        //
        // GET: /Activities/

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Activities";
            return View(new DefaultBaseViewModel());
        }

    }
}
