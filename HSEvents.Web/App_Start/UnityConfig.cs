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
            container.RegisterType<IGetAllRepository<School>, NHGetAllRepository<School>>();
            container.RegisterType<IGetAllRepository<SchoolType>, NHGetAllRepository<SchoolType>>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IAuthentication, CustomAuthentication>();
            container.RegisterType<IGetAllRepository<Attendee>, NHGetAllRepository<Attendee>>();
            container.RegisterType<IGetAllRepository<User>, NHGetAllRepository<User>>();


            container.RegisterType<IGetAllRepository<AcademicProgram>, NHGetAllRepository<AcademicProgram>>();
            container.RegisterType<IGetAllRepository<CityType>, NHGetAllRepository<CityType>>();
            container.RegisterType<IGetAllRepository<Volunteer>, NHGetAllRepository<Volunteer>>();
            container.RegisterType<IGetAllRepository<Address>, NHGetAllRepository<Address>>();
            container.RegisterType<IGetAllRepository<Department>, NHGetAllRepository<Department>>();

            
            return container;
        }
    }
}