using System.Web.Mvc;
using Domain;
using HSEvents.Web.Authentification;
using Unity.Attributes;

namespace HSEvents.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var context = filterContext.HttpContext.ApplicationInstance.Context;

            Auth.HttpContext = context;
            context.User = Auth.CurrentUser;

            ViewBag.User = CurrentUser;
        }
        
        [Dependency]
        public IAuthentication Auth { get; set; }
        public User CurrentUser
        {
            get
            {
                return ((UserIndentity)Auth.CurrentUser.Identity).User;
            }
        }
    }
}
