using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBCreator;
using Domain;
using Domain.Events;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseHelper.ReBuildDB();


            var session = NHibernateHelper.OpenSession();

            var book = session.Get<Book>(1);
            var ev = session.Get<Event>(1);

            var b=new Book()
            {
                Name = "ddd"
                
            };
            session.Save(b);

            book = session.Get<Book>(2);

        }

        private ISessionFactory Create()
        {

            var cfg = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.ConnectionString =
                        @"Server=DESKTOP-51T48C5\SQLEXPRESS; initial catalog=HSEvents; Integrated Security=SSPI;";
                    db.Dialect<MsSql2008Dialect>();
                });

            new SchemaUpdate(cfg).Execute(false, false);

            return cfg.BuildSessionFactory();
        }
    }

    
}
