using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using it_career.data;
using it_career.infrastructure.Interface;
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
        public virtual T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }
        public virtual void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }
      
        public virtual void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }

        public virtual IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }

        public bool Any<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Any(predicate);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
