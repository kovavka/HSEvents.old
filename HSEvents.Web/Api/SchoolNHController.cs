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
    public class SchoolNHController : NHApiController<School>
    {
        public SchoolNHController() 
            : base(new NHGetAllRepository<School>())
        {
            
        }

        [HttpGet]
        public IEnumerable<SimpleEntity> GetSimple()
        {
            var data = repository.GetAll();

            return data.Select(x => new SimpleEntity()
            {
                Id = x.Id,
                View = x.Name
            });
        }

        [HttpGet]
        public School Get(int index)
        {
            return repository.Get(index);
        }

        [HttpPost]
        public object Add(string name, int address)
        {
            var a = new NHRepository<Address>();
            var s = new NHRepository<SchoolType>();
            var school = new School()
            {
                Addresses = new List<Address>() { a.Get(address) },
                BelongToUniversityDistrict = false,
                Name = name,
                Number = 0,
                Type = s.Get(1),
                HasPriority = false,
            };
            s.Close();
            a.Close();

            return repository.Add(school);
        }

        [HttpPost]
        public object Save(int id, string name, int address)
        {
            var a = new NHRepository<Address>();
            var s = new NHRepository<SchoolType>();
            var school = repository.Get(id);
            school.Name = name;
            school.Addresses = new List<Address>() { a.Get(address) };
            s.Close();
            a.Close();

            repository.Update(school);

            return 0;
        }

        [HttpPost]
        public object Delete(int id)
        {
            repository.Delete(id);

            return 0;
        }

    }

}