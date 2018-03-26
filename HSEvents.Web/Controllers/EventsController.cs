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
        public ActionResult Add(Event @event, IEnumerable<int> volunteers, IEnumerable<int> organizers, IEnumerable<int> lecturers, IEnumerable<int> departments)
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

            if (@event.Name.IsNullOrEmpty() || @event.Info.IsNullOrEmpty())
            {
                return View();
            }


            @event.Volunteers = Parse<Volunteer>(volunteers).ToList();
            @event.Volunteers = Parse<Volunteer>(organizers).ToList();
            @event.Volunteers = Parse<Volunteer>(lecturers).ToList();
            @event.Volunteers = Parse<Volunteer>(departments).ToList();

            using (var repository = new NHRepository<Event>())
            {
                repository.Add(@event);
            }

            if (EventExecution != null)
            {
                @event.EventExecution = EventExecution;
                using (var repository = new NHRepository<EventExecution>())
                {
                    foreach (var eventExecution in EventExecution)
                    {
                        repository.Add(eventExecution);
                    }
                }

                using (var repository = new NHRepository<Event>())
                {
                    repository.Update(@event);
                }
            }

            return RedirectToAction("Index");
        }
        

        private IEnumerable<T> Parse<T>(IEnumerable<int> data) where T:IEntity
        {
            foreach (var id in data.WithEnumerable())
            {
                using (var repository=new NHRepository<T>())
                {
                    yield return repository.Get(id);
                }
            }
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

        private static EventExecution[] EventExecution;

        [HttpPost]
        public void PutExecutions(EventExecution[] executions)
        {
            EventExecution = executions;
        }

        [HttpGet]
        public ActionResult GetExecution(int address, Jsdate[] data)
        {
            return Json(

             new EventExecution()
            {
                Address = new NHRepository<Address>().Get(address),
                Dates = Parse(data).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<EventDate> Parse(Jsdate[] data)
        {
            foreach (var jsdate in data)
            {
                yield return new EventDate()
                {
                    Date = jsdate.date,
                    StartTime = new TimeSpan(0, 0, 0, 0, jsdate.startTime),
                    EndTime = new TimeSpan(0, 0, 0, 0, jsdate.endTime)
                };
            }
        }

        public class Jsdate
        {
            public DateTime date { get; set; }
            public int startTime { get; set; }
            public int endTime { get; set; }
        }
    }
}