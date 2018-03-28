using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Domain;
using Domain.Events;
using Domain.IEntity;
using Infrastructure;
using Infrastructure.Repositories;

namespace HSEvents.Web.Api
{
    public class AddressNHController : NHApiController<Address>
    {
        public AddressNHController(IGetAllRepository<Address> repository) 
            : base(repository)
        {
            
        }

        [HttpGet]
        public IEnumerable<SimpleEntity> GetSimple()
        {
            var repo = new NHGetAllRepository<Address>();
            var data = repo.GetAll();

            return data.ToList().Select(x => new SimpleEntity()
            {
                Id = x.Id,
                View = x.FullAddress
            });
        }
        
        [HttpPost]
        public object Delete(int id)
        {
            var repo = new NHRepository<Address>();
            repo.Delete(id);

            return 0;
        }

    }

}