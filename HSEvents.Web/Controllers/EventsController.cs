using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using Common;
using Domain;
using Domain.Events;
using Domain.IEntity;
using HSEvents.Web.Api;
using Infrastructure.Repositories;

namespace HSEvents.Web.Controllers
{
    public class EventsController : BaseController
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            using (var repository = new NHGetAllRepository<Volunteer>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Volunteers"] = new SelectList(list, "Id", "Name");
            }
            using (var repository = new NHGetAllRepository<Employee>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Employees"] = new SelectList(list, "Id", "ContactInfo.FullName");
            }
            using (var repository = new NHGetAllRepository<Department>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Departments"] = new SelectList(list, "Id", "Name");
            }


            return View();
        }

        [HttpPost]
        public ActionResult Add(IEnumerable<int> volunteers, IEnumerable<int> organizers, IEnumerable<int> lecturers, IEnumerable<int> departments)
        {
            using (var repository = new NHGetAllRepository<Volunteer>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Volunteers"] = new SelectList(list, "Id", "Value");
            }
            using (var repository = new NHGetAllRepository<Employee>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Employees"] = new SelectList(list, "Id", "Value");
            }
            using (var repository = new NHGetAllRepository<Department>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Departments"] = new SelectList(list, "Id", "Value");
            }


            return View();
        }

        [HttpGet]
        public ActionResult AddExecution()
        {
            using (var repository = new NHGetAllRepository<Address>())
            {
                var list = repository.GetAll().ToList();
                ViewData["Addresses"] = new SelectList(list, "Id", "FullAddress");
            }
            
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddExecution(int id)
        {
            return View();
        }
    }
}