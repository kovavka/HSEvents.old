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
    public class SchoolTypeNHController : NHApiController<SchoolType>
    {
        public SchoolTypeNHController(IGetAllRepository<SchoolType> repository) 
            : base(repository)
        {
            
        }

        [HttpGet]
        public IEnumerable<SimpleEntity> GetSimple()
        {
            var repo = new NHGetAllRepository<SchoolType>();
            var data = repo.GetAll();

            return data.Select(x => new SimpleEntity()
            {
                Id = x.Id,
                View = x.Name
            });
        }

        [HttpGet]
        public SchoolType Get(int index)
        {
            var repo = new NHRepository<SchoolType>();
            return repo.Get(index);
        }

        [HttpPost]
        public object Add(string name)
        {
            var repo = new NHRepository<SchoolType>();
            var type = new SchoolType() {Name = name};

            return repo.Add(type);
        }

        [HttpPost]
        public object Save(int id, string name)
        {
            var repo = new NHRepository<SchoolType>();
            var type = repo.Get(id);
            type.Name = name;

            repo.Update(type);

            return 0;
        }

        [HttpPost]
        public object Delete(int id)
        {
            var repo = new NHRepository<SchoolType>();
            repo.Delete(id);

            return 0;
        }

    }

}