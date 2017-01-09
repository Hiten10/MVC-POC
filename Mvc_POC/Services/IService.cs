using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc_POC.Services
{
    public interface IService<T> where T:class
    {
        IEnumerable<T> GetAll(Func<T, bool> predeicate = null);
        T Get(Func<T, bool> predeicate = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
