using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Models.ViewModels;
using Leonni.Core.Controllers;

namespace Leonni.Controllers
{
    public class DisclaimerController : LeonniApplicationController
    {
        //
        // GET: /Disclaimer/

        public ActionResult Index()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Disclaimer";
            return View(new DefaultBaseViewModel());
        }

    }
}
