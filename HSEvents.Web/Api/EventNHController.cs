using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Domain.Events;
using Domain.IEntity;
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

        public IQueryable<SimpleEvent> GetForMonth(int month)
        {
            var events = repository.GetAll();



            return events.Where(x => x.EventExecution.Any(xx => xx.Dates.Any(xxx => xxx.Date.Month == month)))
                .SelectMany(x=>x.EventExecution.SelectMany(xx=>xx.Dates).Select(xx=> new SimpleEvent()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Day = xx.Date.Day
                }));
        }
    }

    public class SimpleEvent
    {
        public int Day { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Color { get; set; }
    }
}