using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T:IEntity
    {
        T Get(int id);
        IQueryable<T> GetAll();
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
        object Add(T entity);
    }

    public class NHRepository<T> : IRepository<T> where T : IEntity
    {
        private ISession session = NHibernateHelper.OpenSession();
        
        public T Get(int id)
        {
            return session.Get<T>(id);
        }

        public IQueryable<T> GetAll()
        {
            return session.Query<T>();
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
            session.Flush();
        }

        public void Delete(int id)
        {
            session.Delete(session.Query<T>().First(x=>x.Id==id));
            session.Flush();
        }

        public void Update(T entity)
        {
            session.Update(entity);
            session.Flush();
        }

        public object Add(T entity)
        {
            var obj= session.Save(entity);
            session.Flush();
            return obj;
        }

        public void Close()
        {
            session.Flush();
            session.Close();
        }

    }
}
