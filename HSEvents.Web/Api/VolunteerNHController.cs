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
    public class VolunteerNHController : NHApiController<Volunteer>
    {
        public VolunteerNHController(IGetAllRepository<Volunteer> repository) 
            : base(repository)
        {
            
        }

        [HttpGet]
        public IEnumerable<SimpleEntity> GetSimple()
        {
            var repo = new NHGetAllRepository<Volunteer>();
            var data = repo.GetAll();

            return data.Select(x => new SimpleEntity()
            {
                Id = x.Id,
                View = x.FullName
            });
        }
        
        [HttpPost]
        public object Delete(int id)
        {
            var repo = new NHRepository<Volunteer>();
            repo.Delete(id);

            return 0;
        }

    }

}