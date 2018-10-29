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

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Employee employee = new Employee();
            employee.FirstName = formCollection["FirstName"];
            employee.LastName = formCollection["LastName"];
            employee.Age = Convert.ToByte(formCollection["Age"]);

            EmployeeBAL emp = new EmployeeBAL();
            int NewId = emp.Add(employee);
            return RedirectToAction("Details", "Employee", new { id = NewId });
        }

        public ActionResult Edit(int Id)
        {
            EmployeeBAL emp = new EmployeeBAL();
            Employee FirstEmployee = emp.GetById(Id);
            return View(FirstEmployee);
        }

        [HttpPut]
        public void Edit(Employee Emp)
        {

        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            EmployeeBAL emp = new EmployeeBAL();
            Employee FirstEmployee = emp.GetById(Id);
            return View(FirstEmployee);
        }

        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            EmployeeBAL emp = new EmployeeBAL();
            int NewId = emp.Delete(Id);
            return RedirectToAction("Index", "Employee");
        }
    }
}
