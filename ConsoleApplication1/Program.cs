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


            //var country = new Country()
            //{
            //    Name = "Россия"
            //};
            //var region = new Region()
            //{
            //    Name = "Пермский край",
            //    Country = country
            //};

            //var cityType = new CityType()
            //{
            //    Name = "Город"
            //};
            //var city = new City()
            //{
            //    Name = "Пермь",
            //    CityType = cityType,
            //    Region = region
            //};
            //var street = new Street()
            //{
            //    Name = "Уральская",
            //    City = city
            //};

            //var ad = new Address()
            //{
            //    House = "53А",
            //    Street = street
            //};

            //var type=new SchoolType()
            //{
            //    Name = "лицей"
            //};

            var ad = session.Get<Address>(2);
            ad.House = "35";
            var type= session.Get<SchoolType>(2);

            var school = new School()
            {
                Addresses = new List<Address>() { ad },
                BelongToUniversityDistrict = false,
                Name = "Msddy",
                Number = 10,
                Type = type,
                HasPriority = false,
            };
            
            var ее = session.Save(school);

            ad = session.Get<Address>(2);



            //var a = new Course()
            //{
            //    Name="sdd",
            //    Address = ad,
            //    Type = EventType.Course,
            //    Info = "sddd",
            //    Price = 12,
            //    Duration = 34,
            //    Subject = new Subject() {Name = "dfgre"}
            //};

            //session.Save(a);
            //session.Delete(a);
            var rrr=  session.Get<Course>(1);
            var dd = session.Get<Event>(1);


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
