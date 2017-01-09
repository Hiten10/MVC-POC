using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc_POC.Models;

namespace Mvc_POC.Repositories
{
    public class EmployeeRepository:IRepository<Employee>
    {
        private List<Employee> _employeeContext;
        public EmployeeRepository()
        {
            _employeeContext = new List<Employee>
            {
                new Employee{EmployeeID=1,FirstName="Hitendra",LastName="Kumar",Email="test@test.com",Department="IT"}
            };
        }
        public IEnumerable<Employee> GetAll(Func<Employee, bool> predeicate = null)
        {
            if (predeicate != null)
            {
                return _employeeContext.Where(predeicate);
            }

            return _employeeContext;
        }

        public Employee Get(Func<Employee, bool> predeicate = null)
        {
            return _employeeContext.First(predeicate);
        }

        public void Add(Employee entity)
        {
            _employeeContext.Add(entity);
        }

        public void Update(Employee entity)
        {
            
        }

        public void Delete(Employee entity)
        {
            _employeeContext.Remove(entity);
        }
    }
}