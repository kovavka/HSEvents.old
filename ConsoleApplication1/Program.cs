using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Events;
using Infrastructure;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }

    
}
