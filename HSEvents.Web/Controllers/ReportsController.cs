using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HSEvents.Web.Authentification;

namespace HSEvents.Web.Controllers
{
    //[System.Web.Http.Authorize(Roles = "admin")]
    [Auth(Roles = "admin")]
    public class ReportsController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}