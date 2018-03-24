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
        protected readonly IGetAllRepository<T> repository;

        public NHApiController(IGetAllRepository<T> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();
        }

    }
}