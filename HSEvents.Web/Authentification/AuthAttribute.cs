using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Attributes;

namespace HSEvents.Web.Authentification
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AuthAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var container = (UnityContainer)RouteConfig.BuildUnityContainer();
            var auth = container.Resolve<IAuthentication>();

            auth.HttpContext = filterContext.HttpContext.ApplicationInstance.Context;

            var user = ((UserIndentity)auth.CurrentUser.Identity).User;
            if (user == null)
            {
                HttpContext.Current.Response.StatusCode = 401;
                HttpContext.Current.Response.End();
                return;
            }
            
            foreach (var role in Roles.Split(',', ' '))
            {
                if (!auth.CurrentUser.IsInRole(role))
                {
                    HttpContext.Current.Response.StatusCode = 401;
                    HttpContext.Current.Response.End();
                    return;
                }
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsLocal || base.AuthorizeCore(httpContext);
        }
    }
}