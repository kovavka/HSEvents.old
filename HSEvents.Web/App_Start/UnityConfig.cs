using System.Web.Http;
using HSEvents.Web.Authentification;
using Infrastructure.Repositories;
using Domain;
using Domain.Events;
using Unity;
using Unity.WebApi;

namespace HSEvents.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = BuildUnityContainer();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
        
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IGetAllRepository<Event>, EventRepository>();
            container.RegisterType<IRepository<School>, NHRepository<School>>();
            container.RegisterType<IRepository<SchoolType>, NHRepository<SchoolType>>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAuthentication, CustomAuthentication>();
            container.RegisterType<IGetAllRepository<Attendee>, NHGetAllRepository<Attendee>>();
            

            return container;
        }
    }
}