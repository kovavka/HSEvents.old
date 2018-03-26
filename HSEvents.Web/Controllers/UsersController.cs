using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Infrastructure.Repositories;

namespace HSEvents.Web.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Users
        public ActionResult Index()
        {
            ViewData["Users"]=new NHGetAllRepository<User>().GetAll().ToList();
            return View();
        }

        
    }
}