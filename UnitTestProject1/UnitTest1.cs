using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mvc_POC.Repositories;
using Mvc_POC.Models;
using Mvc_POC.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IRepository<Employee>> _employeeMock;
        private HomeController _controller;
        private List<Employee> _empList;

        [TestInitialize]
        public void Init()
        {
            _employeeMock = new Mock<IRepository<Employee>>();
            _empList = new List<Employee>
            {
                new Employee{EmployeeID=2,FirstName="Kumar",LastName="Hiten",Email="test@test.com",Department="IT"}
            };
        }


        [TestMethod]
        public void TestGetAllMethod()
        {
            _employeeMock.Setup(o => o.GetAll(null)).Returns(_empList).Verifiable();

            //_controller = new HomeController(_employeeMock.Object);

            //ViewResult result = _controller.Employes() as ViewResult;
            //var obj = result.Model;

        }

        [TestMethod]
        public void AddNewEmployee()
        {
            //_employeeMock.Setup(o => o.Add(It.IsAny<Employee>())).Callback<Employee>(e => _empList.Add(e));

            //Mock<IRepository<Employee>> mock = new Mock<IRepository<Employee>>();
            //mock.Setup(o => o.GetAll(null)).Returns(() => _employeeMock.Object);
        }

        [TestCleanup]
        public void Clean()
        {
            _employeeMock = null;
            _empList = null;
        }
    }
}
