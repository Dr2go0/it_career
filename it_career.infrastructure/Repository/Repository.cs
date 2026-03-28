using it_career.data;
using it_career.infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace it_career.infrastructure.Repository
{
    public class Repository : IRepository
    {
        private ApplicationDbContext _context;
        public Repository(ApplicationDbContext contex)
        {
            _context = contex;
        }

        public virtual void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }
        public virtual IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
        public virtual T GetById<T>(Guid id) where T : class
        {
            return _context.Set<T>().Find(id);
        }
        public virtual T GetById<T>(string id) where T : class
        {
            return _context.Set<T>().Find(id);
        }
        public virtual void RemoveById<T>(Guid id) where T : class
        {
            var entity = _context.Set<T>().Find(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }

        }
        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }

        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Any(predicate);
        }
        public IQueryable<T> GetAllWithIncludes<T>(params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
                query = query.Include(include);
            return query;
        }

        public IQueryable<T> FindWithIncludes<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            foreach (var include in includes)
                query = query.Include(include);
            return query;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}

