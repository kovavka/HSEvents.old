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
    public class EventController : BaseController<Event>
    {
        public EventController() : base("api/EventNH/GetAll")
        {
            
        }

        // GET: Event
        public ActionResult Index()
        {
            var dh = GetJs();
            

            return View();
        }


        protected IEnumerable<JsEvent> GetJs()
        {
            using (var client = CreateClient())
            {
                var response = client.GetAsync("api/EventNH/GetForMonth?month=1").Result;

                var result = response.Content.ReadAsAsync<IEnumerable<JsEvent>>().Result;

                return result;
            }
        }

        enum Month
        {
            Январь,
            Февраль,
            Март,
            Апрель,
            Май,
            Июнь,
            Июль,
            Август,
            Сентябрь,
            Октябрь,
            Ноябрь,
            Декабрь
        }
    }

    public class BaseController<T> : Controller where T:IEntity
    {
        private readonly string serviceUrl = "http://localhost:58724";
        private readonly string address;

        public BaseController(string address)
        {
            this.address = address;
        }

        protected HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(serviceUrl);
            client.Timeout = TimeSpan.FromMinutes(10);
            client.DefaultRequestHeaders.Accept.Clear();
            return client;
        }

        protected IEnumerable<T> GetAll()
        {
            using (var client = CreateClient())
            {
                var response = client.GetAsync(address).Result;

                var result = response.Content.ReadAsAsync<IEnumerable<T>>().Result;

                return result;
            }
        }
    }
}