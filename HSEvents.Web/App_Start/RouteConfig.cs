using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HSEvents.Web.Authentification;
using Infrastructure.Repositories;
using Unity;

namespace HSEvents.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            var container = BuildUnityContainer();
            var controllerActivator = new UnityControllerActivator(container);

            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(controllerActivator));
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAuthentication, CustomAuthentication>();


            return container;
        }
    }

    public sealed class UnityControllerActivator : IControllerActivator
    {
        private readonly IUnityContainer _container;

        public UnityControllerActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }
    }
}
