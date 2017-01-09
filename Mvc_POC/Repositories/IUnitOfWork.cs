using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc_POC.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        IRepository<T> Repository<T>() where T:class;
    }
}
