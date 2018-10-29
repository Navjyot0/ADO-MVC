using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["State"] = new List<string> 
            { 
                "Maharashtra",
                "Goa",
                "Tamilnadu",
                "Karnataka",
                "Gujrat",
                "Telangana"
            };
            return View(new List<string> 
            { 
                "United States",
                "United Kingdom",
                "India",
                "Canada",
                "China",
                "Japan"
            });
        }

    }
}
