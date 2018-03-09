using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSEvents.Web.Views
{
    public class DirectoryController : Controller
    {
        

        public ActionResult School()
        {
            return View();
        }

        public ActionResult Index(string type)
        {
            return View();
        }
    }
}