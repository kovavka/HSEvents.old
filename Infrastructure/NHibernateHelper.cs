using System;
using System.Linq;
using System.Reflection;
using Domain;
using Domain.Events;
using Domain.IEntity;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    @"Server=DESKTOP-51T48C5\SQLEXPRESS; Initial Catalog=HSEvents; Integrated Security=SSPI;").ShowSql()
                )

            .Mappings(m => m.FluentMappings.Conventions.AddFromAssemblyOf<EnumConvention>())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load("Infrastructure")))
                //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<BookMap>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, false))
                .BuildSessionFactory();

            //AutoMap.AssemblyOf<BookMap>(new AutomappingConfiguration());

            return sessionFactory.OpenSession();
        }

        public class AutomappingConfiguration : DefaultAutomappingConfiguration
        {
            public override bool ShouldMap(Type type)
            {
                return true;
                    //type.Namespace.StartsWith(NameSpace) &&
                    //   type.GetInterfaces().Any(y => y == typeof(IMappingProvider));

            }

        }

        public static string NameSpace
        {
            get { return typeof(NHibernateHelper).Namespace; }
        }
    }

    public class EnumConvention : IUserTypeConvention
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(
                x =>
                    x.Property.PropertyType.IsEnum ||
                    x.Property.PropertyType.IsGenericType && x.Property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) &&
                    x.Property.PropertyType.GetGenericArguments()[0].IsEnum);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType(instance.Property.PropertyType);
        }
    }
}
