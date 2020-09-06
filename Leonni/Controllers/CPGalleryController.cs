using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;

namespace Leonni.Controllers
{
    public partial class ControlPanelController : LeonniApplicationController
    {
        //
        // GET: /Gallery/

        public ActionResult Galleries()
        {
            ViewBag.CurrentMainTab = "Front";
            ViewBag.CurrentSubTab = "Galleries";
            return View(new DefaultBaseViewModel());
        }

    }
}
