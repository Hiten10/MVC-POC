using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mvc_POC.Controllers;
using Mvc_POC.Models;
using Mvc_POC.Services;

namespace UnitTestProject1
{
    [TestClass]
    class ControllerTesting
    {
        private Mock<IService<Employe>> _mockService;
        HomeController _controller;
        List<Employe> _empList;
        List<Employee> _employeeList;

        [TestInitialize]
        public void Init()
        {
            _mockService=new Mock<IService<Employe>>();
            _controller=new HomeController(_mockService.Object);
            _empList=new List<Employe>{
            new Employe{EmployeeID=1,Email="test@test.com",FirstName="Hitendra",LastName="Kumar",Designation="Lead",Gender="M"},
            new Employe{EmployeeID=2,Email="test@test.com",FirstName="Jitendra",LastName="Kumar",Designation="Manager",Gender="M"}
            };

            var query = from e in _empList
                            select new Employee
                            {
                                EmployeeID=e.EmployeeID,
                                FirstName=e.FirstName,
                                LastName=e.LastName,
                                Department=e.Designation,
                                Email=e.Email                                
                            };
            _employeeList = query.ToList();
        }

        [TestMethod]
        [DeploymentItem("Org.Mentalis.Security.dll")]
        public void Employee_Get_All()
        {
            _mockService.Setup(x=>x.GetAll(null)).Returns(_empList);
            var result = ((_controller.EmployeList() as ViewResult)).Model as List<Employee>;

            Assert.IsNotNull(result);
            Assert.AreEqual<List<Employee>>(result,_employeeList);
            Assert.AreEqual(2,result.Count);
        }

        [TestCleanup]
        public void Clean()
        {
            _mockService = null;
            _controller = null;
            _empList = null;
            _employeeList = null;
        }



    }
}
