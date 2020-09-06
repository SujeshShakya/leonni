using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;

namespace Leonni.Controllers
{
    public class SystemMessageController : LeonniApplicationController
    {
        //
        // GET: /Error/

        public ActionResult Index(string error)
        {
            ViewBag.Error = error;
            return View(new DefaultBaseViewModel());
        }

    }
}
