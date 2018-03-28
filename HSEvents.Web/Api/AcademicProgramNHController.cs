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
    public class AcademicProgramNHController : NHApiController<AcademicProgram>
    {
        public AcademicProgramNHController(IGetAllRepository<AcademicProgram> repository) 
            : base(repository)
        {
            
        }

        [HttpGet]
        public IEnumerable<SimpleEntity> GetSimple()
        {
            var repo = new NHGetAllRepository<AcademicProgram>();
            var data = repo.GetAll();

            return data.Select(x => new SimpleEntity()
            {
                Id = x.Id,
                View = x.Name
            });
        }
        
        [HttpPost]
        public object Delete(int id)
        {
            var repo = new NHRepository<AcademicProgram>();
            repo.Delete(id);

            return 0;
        }

    }

}