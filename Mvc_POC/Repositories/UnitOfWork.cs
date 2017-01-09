using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mvc_POC.Models;

namespace Mvc_POC.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private DbContext _context;
        private bool disposed = false;

        public UnitOfWork()
        {
            this._context = new MVCEntities();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IRepository<T> Repository<T>() where T:class
        {
            return new GenericRepository<T>(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}