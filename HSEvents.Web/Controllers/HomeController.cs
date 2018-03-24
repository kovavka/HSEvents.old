using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using HSEvents.Web.Authentification;
using Unity.Attributes;

namespace HSEvents.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(CurrentUser);
        }
    }
}
