using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace weather.Models
{
    public class person
    {
        public int id { get; set; }
        [Display(Name ="First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        public location location { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "City")]
        public string city { get; set; }
        [Display(Name = "Country")]
        public string country { get; set; }
        [Display(Name = "Telephone")]
        public string telephone { get; set; }
        [Display(Name = "Employ Type")]
        public string employ_type { get; set; }

        public person() { }
        public person(int id)
        {
            this.id = id;
        }
        public person(int id, string first_name, string last_name, string email, string telephone, string employ_type, location location)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.telephone = telephone;
            this.employ_type = employ_type;
            this.location = location;
        }
        
    }
    public class person_model
    {
        public static person get_person_by_id(int id)
        {
            person person = new person(id);
            db db = new db();
            db.connect();
            SqlDataReader reader = db.query_db("EXEC get_person_by_id " + id);
            while (reader.Read())
            {
                person.first_name = reader["first_name"].ToString();
                person.last_name = reader["last_name"].ToString();
                person.email = reader["email"].ToString();
                person.address = reader["address"].ToString();
                person.city = reader["city"].ToString();
                person.country = reader["country"].ToString();
                person.telephone = reader["telephone"].ToString();
                person.location = new location(int.Parse(reader["location_id"].ToString()), reader["location_name"].ToString(), int.Parse(reader["location_api_id"].ToString()));
            }
            reader.Close();
            db.disconnect();
            return person;
        }

        public static List<person> get_person_by_location(location location)
        {
            List<person> people = new List<person>();
            db db = new db();
            db.connect();
            SqlDataReader reader = db.query_db("EXEC get_all_employees_by_location " + location.id);
            while (reader.Read())
            {
                people.Add(new person(
                        int.Parse(reader["id"].ToString()),
                        reader["first_name"].ToString(),
                        reader["last_name"].ToString(),
                        reader["email"].ToString(),
                        reader["telephone"].ToString(),
                        reader["emp_type"].ToString(),
                        location_model.get_location_by_id(location.id)
                    ));
            }
            reader.Close();
            db.disconnect();
            return people;
        }
    }
}