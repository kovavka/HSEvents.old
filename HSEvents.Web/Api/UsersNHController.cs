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
    public class UsersNHController : ApiController
    {
        protected readonly IGetAllRepository<User> repository;

        public UsersNHController(IGetAllRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpPut]
        public void SetAccess(int id)
        {
            var user = repository.Get(id);
            user.Checked = true;
            repository.Update(user);
        }

        [HttpPut]
        public bool ChangeRole(int id)
        {
            var user = repository.Get(id);
            user.IsAdmin = !user.IsAdmin;
            repository.Update(user);
            return user.IsAdmin;
        }
    }
}