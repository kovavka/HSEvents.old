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
        IEnumerable<T> GetAll();
        void Delete(T entity);
        void Update(T entity);
    }

    public class NHRepository<T> : IRepository<T> where T : IEntity
    {
        private ISession session = NHibernateHelper.OpenSession();
        
        public T Get(int id)
        {
            return session.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return session.Query<T>();
        }

        public void Delete(T entity)
        {
            session.Delete(entity);
        }

        public void Update(T entity)
        {
            session.Update(entity);
        }

    }
}
