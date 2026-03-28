using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Interface
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;

        T GetById<T>(Guid id) where T : class;
        T GetById<T>(string id) where T : class;

        void RemoveById<T>(Guid id) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;


        IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class;

        bool Any<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAllWithIncludes<T>(params Expression<Func<T, object>>[] includes) where T : class;
        IQueryable<T> FindWithIncludes<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class;

       void Save();

    }
}
