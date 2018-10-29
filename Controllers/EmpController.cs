using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Model;

namespace MvcApplication.Controllers
{
    public class EmpController : Controller
    {
        public ActionResult Details()
        {
            Emp emp = new Emp()
            {
                Id = 101,
                Name = "Navjyot",
                City = "Pune",
                Gender = Gender.Male
            };
            return View(emp);
        }

    }
}
