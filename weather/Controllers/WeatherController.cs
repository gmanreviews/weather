using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using weather.Models;

namespace weather.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult FiveDay(int id = 0)
        {
            return View(id);
        }

        public ActionResult FiveDayForecast(int id = 0, bool trim = false)
        {
            if (id == 0) return new EmptyResult();
            else
            {
                weather_forecast forecast = weather_api.get_weather_forecast_five_day(location_model.get_location_by_id(id));
                if (trim) forecast = weather_forecast_model.trim_to_five_days(forecast);
                return View(forecast);
            }
        }

        public ActionResult Current(int id = 0)
        {
            if (id == 0) return new EmptyResult();
            else
            {
                weather_forecast forecast = weather_api.get_weather_forecast_current(location_model.get_location_by_id(id));
                return View(forecast);
            }
        }
    }
}