using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Logger;
using NHibernate;

namespace DBCreator
{
    public class DataBaseHelper
    {
        public static void CreateDB(ISessionFactory sessionFactory)
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
                for (int i = 0; i < parts.Length; i++)
                {
                    if (string.IsNullOrEmpty(parts[i].Trim()))
                        continue;

                    using (ITransaction tx = session.BeginTransaction())
                    {
                        ISQLQuery query = session.CreateSQLQuery(parts[i]);
                        query.SetTimeout(600);
                        query.UniqueResult();
                        tx.Commit();
                    }
                }
            }
        }
    }
}
