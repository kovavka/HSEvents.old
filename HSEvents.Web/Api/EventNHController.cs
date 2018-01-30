using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Domain.Events;
using Infrastructure;
using Infrastructure.Repositories;

namespace HSEvents.Web.Api
{
    public class EventNHController : NHApiController<Event>
    {
        public EventNHController(IRepository<Event> repository) 
            : base(repository)
        {
            
        }



        public IEnumerable<JsEvent> GetForMonth(int month)
        {
            var events = GetAll();
            var r = events
                .Where(x => x.Dates.Any(xx => xx.Date.Month - 1 == month))
                .SelectMany(x => x.Dates
                    .Select(xx => new JsEvent()
                    {
                        day = xx.Date.Day,
                        name = x.Name,
                        id = x.Id
                    }));

            return r;
        }
    }

    public class JsEvent
    {
        public int day;
        public string name;
        public int id;
    }

    public class NHApiController<T> : ApiController where T: IEvent
    {
        private readonly IRepository<T> repository;

        public NHApiController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

    }
}