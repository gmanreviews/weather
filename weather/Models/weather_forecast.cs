using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace weather.Models
{
    public class weather_forecast
    {
        public location location { get; set; }
        public List<weather> days_of_weather { get; set; }

        public weather_forecast() { }
        public weather_forecast(location location, List<weather> days_of_weather)
        {
            this.location = location;
            this.days_of_weather = days_of_weather;
        }
    }
    public class weather_forecast_model
    {
        public static weather_forecast trim_to_five_days(weather_forecast forecast)
        {
            List<weather> trimmed = new List<weather>();
            foreach (DateTime date in get_datetimes_from_forecast(forecast.days_of_weather))
            {
                trimmed.Add(get_appropriate_weather_for_date(date, forecast.days_of_weather));
            }
            forecast.days_of_weather = trimmed;
            return forecast;
        }
        private static bool is_date_in_list(DateTime date, List<weather> list)
        {
            return list.Find(x => x.date.Date == date.Date) != null;
        }
        private static weather get_appropriate_weather_for_date(DateTime date, List<weather> list)
        {
            return list.Find(x => x.date.Hour >= 9 && x.date.Date == date.Date);
        }
        private static List<DateTime> get_datetimes_from_forecast(List<weather> list)
        {
            List<DateTime> dt = new List<DateTime>();
            foreach (weather w in list)
            {
                if (!(dt.Exists(x => x.Date.Date == w.date.Date)))
                { 
                    dt.Add(w.date.Date);
                }
            }
            return dt;
        }
    }

    
}