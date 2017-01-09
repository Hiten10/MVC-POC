using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_POC.Models;
using Mvc_POC.Repositories;

namespace Mvc_POC.Services
{
    public class GenericService<T>:IService<T> where T:class
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._repository = unitOfWork.Repository<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predeicate = null)
        {
            if (predeicate != null)
            {
                return _repository.GetAll(predeicate);
            }
            return _repository.GetAll();
        }

        public T Get(Func<T, bool> predeicate = null)
        {
            return _repository.Get(predeicate);
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
            _unitOfWork.Save();
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWork.Save();
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _unitOfWork.Save();
        }
    }
}