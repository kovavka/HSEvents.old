using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Common;
using Domain;
using Domain.Events;
using Infrastructure.Repositories;

namespace HSEvents.Web.Views
{
    public class DirectoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Partail(string viewName)
        {

            var addresses=new NHRepository<Address>().GetAll();

            ViewData["Address"]= new SelectList(addresses, "Id", "FullAddress");

            return PartialView(viewName);
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