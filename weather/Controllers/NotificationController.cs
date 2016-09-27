using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using weather.Models;

namespace weather.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult notification(int id = 0, string type = null, string from = null)
        {
            if (id == 0 || type == null || from == null) return RedirectToAction("Index", "Home");
            else
            {
                notification_model.send_note_to_many(person_model.get_person_by_location(new location(id)), type);
                switch (from)
                {
                    case "more":
                        return RedirectToAction("MoreWeatherDetails", "Home");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
        }

        public JsonResult send_all_day_note(int id = 0, string type = null)
        {
            if (id == 0 || type == null) return Json(new { success = 0 }, JsonRequestBehavior.AllowGet);
            else
            {
                notification_model.send_note_to_many(person_model.get_person_by_location(new location(id)), type);
                return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}