using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Common;
using Domain;
using Domain.Events;
using HSEvents.Web.Controllers;
using Infrastructure.Repositories;

namespace HSEvents.Web.Views
{
    public class DirectoryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Partail(string viewName)
        {
            ViewBag.Title = "mu";
            var addresses=new NHGetAllRepository<Address>().GetAll();

            ViewData["Address"]= new SelectList(addresses, "Id", "FullAddress");

            return PartialView(viewName);
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
    }
}