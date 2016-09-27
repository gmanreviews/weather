using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace weather.Models
{
    public class db
    {
        private SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["weather"].ConnectionString);
        public db() { }

        public bool connect()
        {
            if (this.connection.State == System.Data.ConnectionState.Open) this.connection.Close();
            this.connection.Open();
            return (this.connection.State == System.Data.ConnectionState.Open);
        }

        public bool disconnect()
        {
            this.connection.Close();
            return this.connection.State == System.Data.ConnectionState.Closed;
        }

        public SqlDataReader query_db(string query)
        {
            try
            {

                if (this.connection.State == System.Data.ConnectionState.Closed) this.connect();
                SqlCommand sql_cmd = new SqlCommand();
                //sql_cmd.CommandText = clean_query(query);
                sql_cmd.CommandText = query;
                sql_cmd.Connection = this.connection;
                SqlDataReader result = sql_cmd.ExecuteReader();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string clean_query(string query)
        {
            return query.Replace("'", "''");
        }
    }
}