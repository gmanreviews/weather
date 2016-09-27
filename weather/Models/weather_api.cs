using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace weather.Models
{
    public class weather_api
    {
        private static string api_base_url_five_day = "http://api.openweathermap.org/data/2.5/forecast?";
        private static string api_base_url_current = "http://api.openweathermap.org/data/2.5/weather?";
        //http://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=5b585b62ca9e70cf831b4c3d177e1faf
        private static string api_key = "5b585b62ca9e70cf831b4c3d177e1faf";

        private static string build_api_url_five_day(int id)
        {
            return api_base_url_five_day + "id=" + id.ToString() + "&mode=json" + "&units=imperial" + "&appid=" + api_key;
        }

        private static string build_api_url_current(int id)
        {
            return api_base_url_current + "id=" + id.ToString() + "&mode=json" + "&units=imperial" + "&appid=" + api_key;
        }

        private static weather_forecast parse_json_forecast(JObject json, location location)
        {
            weather_forecast forecast = new weather_forecast();
            forecast.location = location;
            forecast.days_of_weather = new List<weather>();
            foreach (var days_data in json["list"])
            {
                weather weather = new weather();
                weather.description = days_data["weather"][0]["description"].ToString(); 
                weather.weather_type = days_data["weather"][0]["main"].ToString();
                weather.humidity = (int)days_data["main"]["humidity"];
                weather.temp_min = double.Parse(days_data["main"]["temp_min"].ToString());
                weather.temp_max = double.Parse(days_data["main"]["temp_max"].ToString());
                weather.date = DateTime.Parse(days_data["dt_txt"].ToString());
                forecast.days_of_weather.Add(weather);
            }
            return forecast;
        }

        private static weather_forecast parse_json_forecast_current(JObject json, location location)
        {
            weather_forecast forecast = new weather_forecast();
            forecast.location = location;
            forecast.days_of_weather = new List<weather>();
            weather weather = new weather();
            weather.description = json["weather"][0]["description"].ToString();
            weather.weather_type = json["weather"][0]["main"].ToString();
            weather.humidity = (int)json["main"]["humidity"];
            weather.temp_min = double.Parse(json["main"]["temp_min"].ToString());
            weather.temp_max = double.Parse(json["main"]["temp_max"].ToString());
            //weather.date = DateTime.Parse(json["dt"].ToString());
            forecast.days_of_weather.Add(weather);
            return forecast;
        }

        private static weather_forecast get_weather_forecast(location location, string type)
        {
            string api_call = (type == "five")? build_api_url_five_day(location.api_id) : build_api_url_current(location.api_id);

            HttpWebRequest request = WebRequest.Create(api_call) as HttpWebRequest;
            weather_forecast forecast = new weather_forecast();
            using (HttpWebResponse response  = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));

                
                Stream datastream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(datastream);
                string json_response = streamreader.ReadToEnd();
                streamreader.Close();
                datastream.Close();

                JObject json = JObject.Parse(json_response);

                forecast = (type == "five") ? parse_json_forecast(json, location) : parse_json_forecast_current(json, location);

            }
            return forecast;
        }

        public static weather_forecast get_weather_forecast_five_day(location location)
        {
            return get_weather_forecast(location, "five");
        }

        public static weather_forecast get_weather_forecast_current(location location)
        {
            return get_weather_forecast(location, "current");
        }

    }
}