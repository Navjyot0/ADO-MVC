using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Model;
using System.Data;
using System.Data.SqlClient;

namespace MvcApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeBAL emp = new EmployeeBAL();
            List<Employee> FirstEmployee = emp.GetAll.ToList();
            return View(FirstEmployee);
        }

        public ActionResult Details(int Id)
        {
            EmployeeBAL emp = new EmployeeBAL();
            Employee FirstEmployee = emp.GetById(Id);
            return View(FirstEmployee);
        }

        public ActionResult Create()
        {
            return View();
        }

        //public void Create(Employee Emp)
        //{ 
            
        //}
    }
}
