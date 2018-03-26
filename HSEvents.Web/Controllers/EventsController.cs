using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using Common;
using Domain.Events;
using Domain.IEntity;
using HSEvents.Web.Api;

namespace HSEvents.Web.Controllers
{
    public class EventsController : BaseController
    {
        // GET: Event
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Add()
        {

            return View();
        }
    }
}