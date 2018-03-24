using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml.Schema;
using Domain;
using Domain.Events;
using Domain.IEntity;
using Infrastructure;
using Infrastructure.Repositories;

namespace HSEvents.Web.Api
{
    public class SimpleEntity
    {
        public int Id { get; set; }
        public string View { get; set; }
    }

    public class DirectoryNHController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Attendees()
        {
            var repo = new NHGetAllRepository<Attendee>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }

        [HttpGet]
        public IEnumerable<string> ContactPersons()
        {
            var repo = new NHGetAllRepository<ContactPerson>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }

        [HttpGet]
        public IEnumerable<Address> Addresses(int? index)
        {
            var repo = new NHGetAllRepository<Address>();
            var data = repo.GetAll();

            var ids = new NHGetAllRepository<School>().GetAll().Where(x => x.Id != index).SelectMany(x=>x.Addresses.Select(xx=>xx.Id));
            
            return data.Where(x => !ids.Contains(x.Id));
        }
        [HttpGet]
        public IEnumerable<string> AcademicPrograms()
        {
            var repo = new NHGetAllRepository<AcademicProgram>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }

        [HttpGet]
        public IEnumerable<string> SchoolTypes()
        {
            var repo = new NHGetAllRepository<SchoolType>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }

        [HttpGet]
        public IEnumerable<string> CityTypes()
        {
            var repo = new NHGetAllRepository<CityType>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }

        [HttpGet]
        public IEnumerable<string> Volunteers()
        {
            var repo = new NHGetAllRepository<Volunteer>();
            var data = repo.GetAll();

            return data.Select(x => x.FullName);
        }

        [HttpGet]
        public IEnumerable<string> Groups()
        {
            var repo = new NHGetAllRepository<School>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }

        [HttpGet]
        public IEnumerable<string> Departments()
        {
            var repo = new NHGetAllRepository<Department>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }

        [HttpGet]
        public IEnumerable<string> Users()
        {
            var repo = new NHGetAllRepository<User>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }

        [HttpGet]
        public IEnumerable<string> Employees()
        {
            var repo = new NHGetAllRepository<Employee>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }
        
    }
    
}