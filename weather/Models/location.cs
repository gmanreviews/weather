using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace weather.Models
{
    public class location
    {
        public int id { get; set; }
        public string name { get; set; }
        public int api_id { get; set; }
        public int other_id { get; set; }

        public location() { }
        public location(int id)
        {
            this.id = id;
            this.other_id = id;
        }
        public location(string name)
        {
            this.name = name;
        }
        public location(int id, string name, int api_id)
        {
            this.id = id;
            this.name = name;
            this.api_id = api_id;
            this.other_id = id;
        }
    }
    public class location_model
    {
        public static location get_location_by_id(int id)
        {
            location location = new location(id);
            db db = new db();
            db.connect();
            SqlDataReader reader = db.query_db("EXEC get_location_by_id " + id);
            while (reader.Read())
            {
                location.name = reader["location"].ToString();
                location.api_id = int.Parse(reader["city_id"].ToString());
            }
            reader.Close();
            db.disconnect();
            return location;
        }
        
    }
}