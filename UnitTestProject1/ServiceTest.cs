using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using Mvc_POC.Models;
using Mvc_POC.Repositories;
using Mvc_POC.Services;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    internal class ServiceTest
    {
        private Mock<IRepository<Employe>> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitWork;
        private IService<Employe> _service;
        private List<Employe> _empList;

        [TestFixtureSetUp]
        public void Init_Once()
        {
            _mockUnitWork = new Mock<IUnitOfWork>();
            _mockRepository = new Mock<IRepository<Employe>>();
               
        }

        [SetUp]
        public void Init()
        {
            _empList = new List<Employe>{
            new Employe{EmployeeID=1,Email="test@test.com",FirstName="Hitendra",LastName="Kumar",Designation="Lead",Gender="M"},
            new Employe{EmployeeID=2,Email="test@test.com",FirstName="Jitendra",LastName="Kumar",Designation="Manager",Gender="M"}
            };

        }

        [TestCase]
        public void Get_All_Service()
        {
            _mockRepository.Setup(x => x.GetAll(null)).Returns(_empList);
            _mockUnitWork.Setup(m => m.Repository<Employe>()).Returns(_mockRepository.Object);
            _service = new GenericService<Employe>(_mockUnitWork.Object);
            var result = _service.GetAll(null).ToList<Employe>();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestCase]
        public void Add_Employe()
        {
            Employe e = new Employe {EmployeeID = 1001, FirstName = "Test", LastName = "Test"};
            //_mockRepository.Setup(m => m.Add(e)).Callback((Employe emp) =>
            //{
            //    emp.EmployeeID = e.EmployeeID;
            //    emp.FirstName = e.FirstName;
            //    emp.LastName = e.LastName;
            //    _empList.Add(emp);
            //});
            //_mockRepository.Setup(m => m.Add(e)).Callback((Employe o) => _empList.Add(o));
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_empList);
            _mockRepository.Setup(m => m.Add(It.IsAny<Employe>())).Callback<Employe>(o=>_empList.Add(o));
            _mockUnitWork.Setup(u => u.Repository<Employe>()).Returns(_mockRepository.Object);
            _service = new GenericService<Employe>(_mockUnitWork.Object);
            _service.Add(e);

            var result = _service.GetAll();
            var emp = result.First(t => t.EmployeeID==1001);

            Assert.AreEqual(_empList.Count(), result.Count());
            Assert.AreEqual("Test", emp.FirstName);
            Assert.AreEqual("Test", emp.LastName);

        }

        [TearDown]
        public void CleanUp()
        {
            _service = null;
            _empList = null;
        }

        [TestFixtureTearDown]
        public void CleanUp_Once()
        {
            _mockRepository = null;
            _mockUnitWork = null;
            
        }
    }
}
