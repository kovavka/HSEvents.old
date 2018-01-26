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
        public EventNHController(IRepository<Event> repository) : base()
        {
            new EventRepository()
        }
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