using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using weather.Models;

namespace weather.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult location(int id = 0)
        {
            if (id == 0) return RedirectToAction("Index", "Home");
            else return View(person_model.get_person_by_location(new location(id)));
        }
    }
}