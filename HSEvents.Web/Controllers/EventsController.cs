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
                EventExecution.Clear();
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
                @event.EventExecutions = EventExecution;

                using (var repository = new NHRepository<Event>())
                {
                    repository.Update(@event);
                }
            }
            EventExecution.Clear();

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

        private static List<EventExecution> EventExecution=new List<EventExecution>();
        

        [HttpPost]
        public void PutExecutions(EventExecution[] executions)
        {
            //EventExecution = executions;
        }

        [HttpPost]
        public ActionResult GetExecution(int address, Jsdate[] data)
        {
            Address ad;
            int a;

            using (var repository = new NHRepository<Address>())
            {
                ad = repository.Get(address);
                a = ad.Id;
            }

            var result =
                new EventExecution()
                {
                    Address = ad,
                    Dates = Parse(data).ToList()
                };


            EventExecution.Add(result);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<EventDate> Parse(Jsdate[] data)
        {
            foreach (var jsdate in data)
            {
               
                var t = new DateTime(1970, 1, 1, 5, 0, 0).AddMilliseconds(jsdate.date);

                yield return new EventDate()
                {
                    Date = t,
                    StartTime = new TimeSpan(12,34,0),
                    EndTime = new TimeSpan(12, 34, 0)
                };
            }
        }

        public class Jsdate
        {
            public long date { get; set; }
            public long startTime { get; set; }
            public long endTime { get; set; }
        }
    }
}