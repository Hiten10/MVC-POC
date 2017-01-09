using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc_POC.Repositories
{
    public class GenericRepository<T>:IRepository<T> where T:class
    {
        internal DbContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predeicate = null)
        {
            IQueryable<T> query = _dbSet;
            if(predeicate!=null)
            {
                return query.Where(predeicate).ToList();
            }
            return query.ToList();
        }

        public T Get(Func<T, bool> predeicate)
        {
            return _dbSet.First(predeicate);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = System.Data.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == System.Data.EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
    }
}