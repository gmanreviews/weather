using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace weather.Models
{
    public class weather
    {
        public DateTime date { get; set; }
        public string weather_type { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int humidity { get; set; }


        public weather() { }
        public weather(DateTime date, string short_description, string description, double temp_min, double temp_max, int humidity)
        {
            this.date = date;
            this.short_description = short_description;
            this.description = description;
            this.temp_min = temp_min;
            this.temp_max = temp_max;
            this.humidity = humidity;
        }

    }
}