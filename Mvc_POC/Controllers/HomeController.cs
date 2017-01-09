using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_POC.Models;
using Mvc_POC.Repositories;
using Mvc_POC.Services;

namespace Mvc_POC.Controllers
{
    public class HomeController : Controller
    {
        //private IRepository<Employee> _employeeRepository;
        IService<Employe> _employeeService;

        //public HomeController(IRepository<Employee> empRepo)
        //{
        //    _employeeRepository = empRepo;
        //}
        public HomeController(IService<Employe> empService)
        {
            _employeeService = empService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";            
            return View();
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Employes()
        //{

        //    return View(_employeeRepository.GetAll());
        //}

        public ActionResult EmployeList()
        {
            var empList = _employeeService.GetAll();
            var query = from e in empList 
                        select new Employee {
                            FirstName = e.FirstName, 
                            LastName = e.LastName, 
                            Department = e.Designation, 
                            Email = e.Email, 
                            EmployeeID = e.EmployeeID
                        };

            return View(query.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employe emp)
        {
            if (ModelState.IsValid)
            {
                _employeeService.Add(emp);
                return RedirectToAction("EmployeList");
            }
            else
            {
                return View("Create");
            }
            
        }
    }
}
