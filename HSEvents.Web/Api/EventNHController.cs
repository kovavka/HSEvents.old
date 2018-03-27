using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Domain;
using Domain.Events;
using Domain.IEntity;
using HSEvents.Web.Authentification;
using Infrastructure;
using Infrastructure.Repositories;

namespace HSEvents.Web.Api
{
    [HttpAuth]
    public class EventNHController : NHApiController<Event>
    {
        public EventNHController(IGetAllRepository<Event> repository) 
            : base(repository)
        {
            
        }

        public IEnumerable<SimpleEvent> GetForMonth(int month)
        {
            var events = repository.GetAll();

            return events.Where(x => x.EventExecutions.Any(xx => xx.Dates.Any(xxx => xxx.Date.Month == month)))
                .ToList()
                .SelectMany(x=>x.EventExecutions.SelectMany(xx=>xx.Dates).Select(xx=> new SimpleEvent()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Day = xx.Date.Day,
                    Colors = x.Departments.Select(xxx => xxx.Color)
                }));
        }

        [HttpGet]
        public EventExecution GetExecution(int address, string data)
        {
            return new EventExecution()
            {
                Address = new NHRepository<Address>().Get(address),
                Dates = Parse(null).ToList()
            };
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

    }

    public class Jsdate
    {
        public DateTime date { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
    }

    public class SimpleEvent
    {
        public int Day { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}