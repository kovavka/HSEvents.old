using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Common;
using Domain;
using Domain.Events;
using Infrastructure.Repositories;

namespace HSEvents.Web.Views
{
    public class DirectoryController : Controller
    {
        public ActionResult Index(DirectoryType? type)
        {
            var view = View();
            if (type == null)
                return view;

            var methodInfo = GetType().GetMethod(type.ToString());

            if (methodInfo==null)
                return view;

            ViewBag.Title = type.GetDescription();
            ViewData["list"] = (IEnumerable<string>) methodInfo.Invoke(this, null);

            return view;
        }

        private IEnumerable<string> Attendees()
        {
            var repo = new NHRepository<Attendee>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }
        private IEnumerable<string> ContactPersons()
        {
            var repo = new NHRepository<ContactPerson>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }
        private IEnumerable<string> Schools()
        {
            var repo = new NHRepository<School>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> AcademicPrograms()
        {
            var repo = new NHRepository<AcademicProgram>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> SchoolTypes()
        {
            var repo = new NHRepository<SchoolType>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> CityTypes()
        {
            var repo = new NHRepository<CityType>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> Volunteers()
        {
            var repo = new NHRepository<Volunteer>();
            var data = repo.GetAll();

            return data.Select(x => x.FullName);
        }
        private IEnumerable<string> Groups()
        {
            var repo = new NHRepository<School>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> Departments()
        {
            var repo = new NHRepository<Department>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
        private IEnumerable<string> Users()
        {
            var repo = new NHRepository<User>();
            var data = repo.GetAll();

            return data.Select(x => x.ContactInfo.FullName);
        }
        private IEnumerable<string> Employees()
        {
            var repo = new NHRepository<School>();
            var data = repo.GetAll();

            return data.Select(x => x.Name);
        }
    }

    public enum DirectoryType
    {
        [Description("Участники")] Attendees,
        [Description("Контактные лица школ")] ContactPersons,
        [Description("Школы")] Schools,
        [Description("Академические программы")] AcademicPrograms,
        [Description("Типы школ")] SchoolTypes,
        [Description("Типы населенных пунктов")] CityTypes,
        [Description("Волонтеры")] Volunteers,
        [Description("Группы")] Groups,
        [Description("Департаменты")] Departments,
        [Description("Пользователи")] Users,
        [Description("Сотрудники")] Employees,
    }
}