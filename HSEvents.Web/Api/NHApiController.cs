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
    public class NHApiController<T> : ApiController where T: IEntity
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