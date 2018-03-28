using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Common;
using Domain;
using Domain.Events;
using HSEvents.Web.Authentification;
using HSEvents.Web.Controllers;
using Infrastructure;
using Infrastructure.Repositories;
using NHibernate.Linq;

namespace HSEvents.Web.Views
{
    [Auth]
    public class DirectoryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partail(string viewName, bool edit)
        {
            //ViewBag.Title = "mu";
            var addresses=new NHGetAllRepository<Address>().GetAll().ToList();

            ViewData["Address"]= new SelectList(addresses, "Id", "FullAddress");

            ViewBag.Edit = edit;


            return View(viewName);
        }

        [HttpGet]
        public ActionResult Address(int id = 0)
        {
            ViewBag.Edit = id != 0;

            List<CityType> types;
            using (var repository = new NHGetAllRepository<CityType>())
            {
                types = repository.GetAll().ToList();
            }

            var list = (IEnumerable<SelectListItem>) new SelectList(types, "id", "Name");
            
            AddressDto address = new AddressDto();
            if (id != 0)
            {
                using (var repository= new NHRepository<Address>())
                {
                    var t = repository.Get(id);
                    address = new AddressDto()
                    {
                        Id = t.Id,
                        House = t.House,
                        Street = t.Street.Name,
                        Country = t.Country.Name,
                        Region = t.Region.Name,
                        CityType = t.CityType.Id,
                        City = t.City.Name
                    };
                }


                list = types.Select(x =>
                    new SelectListItem() {Text = x.Name, Selected = x.Id == address.CityType, Value = x.Id.ToString()});
            }

            ViewData["CityType"] = list;

            return View(address);
        }

        [HttpPost]
        public ActionResult Address(AddressDto address)
        {
            List<CityType> types;
            using (var repository = new NHGetAllRepository<CityType>())
            {
                types = repository.GetAll().ToList();
            }

            var list = (IEnumerable<SelectListItem>)new SelectList(types, "Id", "Name");

            ViewData["CityType"] = list;
            ViewBag.Edit = address.Id != 0;


           if(address.Country.IsNullOrEmpty())
               ModelState.AddModelError("Country", "Поле не может быть пустым");
            if (address.Region.IsNullOrEmpty())
                ModelState.AddModelError("Region", "Поле не может быть пустым");
            if (address.City.IsNullOrEmpty())
                ModelState.AddModelError("City", "Поле не может быть пустым");
            if (address.CityType==0)
                ModelState.AddModelError("CityType", "Поле не может быть пустым");
            if (address.Street.IsNullOrEmpty())
                ModelState.AddModelError("Street", "Поле не может быть пустым");
            if (address.House.IsNullOrEmpty())
                ModelState.AddModelError("House", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(address);


            var session = NHibernateHelper.OpenSession();


            using (var tx= session.BeginTransaction())
            {
                var country = session.Query<Country>().FirstOrDefault(x => x.Name == address.Country) ??
                             new Country() { Name = address.Country };

                var region = session.Query<Region>().FirstOrDefault(x => x.Name == address.Region) ??
                             new Region() { Name = address.Region, Country = country};

                var cityType = session.Query<CityType>().First(x => x.Id == address.CityType);

                var city = session.Query<City>().FirstOrDefault(x => x.Name == address.City) ??
                             new City() { Name = address.City, Region = region, CityType = cityType};

                var street = session.Query<Street>().FirstOrDefault(x => x.Name == address.Street) ??
                             new Street() {Name = address.Street, City = city };

                var entity = new Address()
                {
                    Id = address.Id,
                    House = address.House,
                    Street = street,
                };

                session.SaveOrUpdate(entity);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult School(int id = 0)
        {
            ViewBag.Edit = id != 0;

            List<Address> addresses;
            using (var repository = new NHGetAllRepository<Address>())
            {
                addresses = repository.GetAll().ToList();
            }

            var list = (IEnumerable<SelectListItem>)new SelectList(addresses, "Id", "FullAddress");

            SchoolDto school = new SchoolDto();
            if (id != 0)
            {
                using (var repository = new NHRepository<School>())
                {
                    var t = repository.Get(id);
                    school = new SchoolDto()
                    {
                        Id = t.Id,
                        Address = t.Addresses.ToList()[0].Id,
                        Name = t.Name
                    };
                }


                list = addresses.Select(x =>
                    new SelectListItem() { Text = x.FullAddress, Selected = x.Id == school.Address, Value = x.Id.ToString() });
            }

            ViewData["Address"] = list;

            return View(school);
        }

        [HttpPost]
        public ActionResult School(SchoolDto school)
        {
            List<Address> addresses;
            using (var repository = new NHGetAllRepository<Address>())
            {
                addresses = repository.GetAll().ToList();
            }

            var list = (IEnumerable<SelectListItem>)new SelectList(addresses, "Id", "FullAddress");

            ViewData["Address"] = list;
            ViewBag.Edit = school.Id != 0;


            if (school.Name.IsNullOrEmpty())
                ModelState.AddModelError("Name", "Поле не может быть пустым");
            if (school.Address == 0)
                ModelState.AddModelError("Address", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(school);


            var session = NHibernateHelper.OpenSession();


            using (var tx = session.BeginTransaction())
            {
                var address = session.Query<Address>().First(x => x.Id == school.Address);

                var entity = new School()
                {
                    Id = school.Id,
                    Name = school.Name,
                    Addresses = new List<Address>() { address},
                };

                session.SaveOrUpdate(entity);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Department(int id = 0)
        {
            ViewBag.Edit = id != 0;
            
            Department department = new Department();
            if (id != 0)
            {
                using (var repository = new NHRepository<Department>())
                {
                    department = repository.Get(id);
                }
            }
            
            return View(department);
        }

        [HttpPost]
        public ActionResult Department(Department department)
        {
            ViewBag.Edit = department.Id != 0;
            
            if (department.Name.IsNullOrEmpty())
                ModelState.AddModelError("Name", "Поле не может быть пустым");
            if (department.Color.IsNullOrEmpty())
                ModelState.AddModelError("Color", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(department);


            var session = NHibernateHelper.OpenSession();


            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(department);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult AcademicProgram(int id = 0)
        {
            ViewBag.Edit = id != 0;

            var academicProgram = new AcademicProgram();
            if (id != 0)
            {
                using (var repository = new NHRepository<AcademicProgram>())
                {
                    academicProgram = repository.Get(id);
                }
            }

            return View(academicProgram);
        }

        [HttpPost]
        public ActionResult AcademicProgram(AcademicProgram academicProgram)
        {
            ViewBag.Edit = academicProgram.Id != 0;

            if (academicProgram.Name.IsNullOrEmpty())
                ModelState.AddModelError("Name", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(academicProgram);


            var session = NHibernateHelper.OpenSession();


            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(academicProgram);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult SchoolType(int id = 0)
        {
            ViewBag.Edit = id != 0;

            SchoolType schoolType = new SchoolType();
            if (id != 0)
            {
                using (var repository = new NHRepository<SchoolType>())
                {
                    schoolType = repository.Get(id);
                }
            }

            return View(schoolType);
        }

        [HttpPost]
        public ActionResult SchoolType(SchoolType schoolType)
        {
            ViewBag.Edit = schoolType.Id != 0;

            if (schoolType.Name.IsNullOrEmpty())
                ModelState.AddModelError("Name", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(schoolType);


            var session = NHibernateHelper.OpenSession();


            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(schoolType);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Volunteer(int id = 0)
        {
            ViewBag.Edit = id != 0;

            Volunteer volunteer = new Volunteer();
            if (id != 0)
            {
                using (var repository = new NHRepository<Volunteer>())
                {
                    volunteer = repository.Get(id);
                }
            }

            return View(volunteer);
        }

        [HttpPost]
        public ActionResult Volunteer(Volunteer volunteer)
        {
            ViewBag.Edit = volunteer.Id != 0;

            if (volunteer.FullName.IsNullOrEmpty())
                ModelState.AddModelError("FullName", "Поле не может быть пустым");

            if (!ModelState.IsValid)
                return View(volunteer);


            var session = NHibernateHelper.OpenSession();


            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(volunteer);

                tx.Commit();
            }

            return RedirectToAction("Index");
        }
        
    }

    public enum DirectoryType
    {
        [Description("Участники")] Attendee,
        [Description("Контактные лица школ")] ContactPerson,
        [Description("Школы")] School,
        [Description("Академические программы")] AcademicProgram,
        [Description("Типы школ")] SchoolType,
        [Description("Типы населенных пунктов")] CityType,
        [Description("Волонтеры")] Volunteer,
        [Description("Группы")] Group,
        [Description("Департаменты")] Department,
        [Description("Пользователи")] User,
        [Description("Сотрудники")] Employee,
        [Description("Адреса")] Address,
    }

    public class AddressDto
    {
        public int Id { get; set; }

        public string House { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public int CityType { get; set; }

        public string City { get; set; }

    }
    
    public class SchoolDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Address { get; set; }

    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public string Error { get; set; }

        public ValidationResult(bool isValid)
        {
            IsValid = isValid;
        }
    }
}