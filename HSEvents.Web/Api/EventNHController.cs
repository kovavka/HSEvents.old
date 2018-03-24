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
        public EventNHController(IGetAllRepository<Event> repository) 
            : base(repository)
        {
            
        }

        public IEnumerable<SimpleEvent> GetForMonth(int month)
        {
            var events = repository.GetAll();

            return events.Where(x => x.EventExecution.Any(xx => xx.Dates.Any(xxx => xxx.Date.Month == month)))
                .ToList()
                .SelectMany(x=>x.EventExecution.SelectMany(xx=>xx.Dates).Select(xx=> new SimpleEvent()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Day = xx.Date.Day,
                    Colors = x.Departments.Select(xxx => xxx.Color)
                }));
        }
    }

    public class SimpleEvent
    {
        public int Day { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}