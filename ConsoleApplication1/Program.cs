using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBCreator;
using Domain;
using Domain.Events;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure;
using Infrastructure.Mappings.Events;
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
            //DataBaseHelper.ReBuildDB();


            var session = NHibernateHelper.OpenSession();



            var school = session.Get<School>(2);
            var prog2 = session.Get<AcademicProgram>(2);
            var prog1 = session.Get<AcademicProgram>(1);

            var pupil = new Pupil()
            {
                ContactInfo = new ContactInfo() {FullName = "fd"},
                Sex = Sex.Female,
                School = school,
                IntrestingPrograms = new List<AcademicProgram>(){ prog1,prog2 },
                RegistrarionPrograms = new List<AcademicProgram>() { prog2 }
            };

            session.Save(pupil);
            session.Flush();


            var pupil2 = new Pupil()
            {
                ContactInfo = new ContactInfo() { FullName = "fdds" },
                Sex = Sex.Female,
                School = school,
                IntrestingPrograms = new List<AcademicProgram>() { prog1, prog2 },
                RegistrarionPrograms = new List<AcademicProgram>() { prog2 }
            };

            session.Save(pupil2);
            session.Flush();
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
