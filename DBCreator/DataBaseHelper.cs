﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Logger;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DBCreator
{
    public class DataBaseHelper
    {
        private static ISessionFactory sessionFactory;

        private static readonly string server = @"Server= .; ";

        private static FluentConfiguration defaultConfiguration = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    server + @"Initial Catalog=HSEvents; Integrated Security=SSPI;").ShowSql()
            )
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));

        private static FluentConfiguration reBiuldConfiguration = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    server + @"Integrated Security=SSPI;").ShowSql()
            )
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true));


        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                    sessionFactory = defaultConfiguration.BuildSessionFactory();

                return sessionFactory;
            }
        }

        public static void ReBuildDB()
        {
            var factory = reBiuldConfiguration.BuildSessionFactory();

            Run("Drop database HSEvents;", factory);
            Run("Create database HSEvents;", factory);
            factory.Close();

            UpdateDB(SessionFactory);
        }

        private static void Run(string queryString, ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            {
                ISQLQuery query = session.CreateSQLQuery(queryString);
                query.SetTimeout(600);
                query.UniqueResult();
            }
        }

        private static void UpdateDB(ISessionFactory sessionFactory)
        {
            var scripts = GetScripts();
            foreach (var script in scripts)
            {
                script.Value.RunScript(sessionFactory);
            }
        }
        
        private static SortedDictionary<int, ScriptItem> GetScripts()
        {
            //временно, пока запускаю из консоли
            var dir = @"..\..\..\DBCreator\SQL";
            var result = new SortedDictionary<int, ScriptItem>();
            

            var files = Directory.GetFiles(dir, "*.sql");

            foreach (var name in files)
            {
                int number;
                int.TryParse(Path.GetFileNameWithoutExtension(name), out number);

                if (result.ContainsKey(number))
                    Log.Error(new Exception(string.Format("скрипт с номером {0} уже существует", number)));

                var script = File.ReadAllText(name, Encoding.UTF8);

                result.Add(number, new ScriptItem(script));
            }

            return result;
        }

    }
    
    public class ScriptItem
    {
        public readonly string Script;

        public ScriptItem(string script)
        {
            Script = script;
        }
        
        public void RunScript(ISessionFactory sessionFactory)
        {
            string[] parts = Script.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            using (ISession session = sessionFactory.OpenSession())
            {
                foreach(var queryString in parts)
                {
                    if (string.IsNullOrEmpty(queryString.Trim()))
                        continue;
                    
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        ISQLQuery query = session.CreateSQLQuery(queryString);
                        query.SetTimeout(600);
                        query.UniqueResult();

                        tx.Commit();
                    }
                }
            }
        }
        
    }
}
