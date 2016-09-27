using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace weather.Models
{
    public class general
    {
        public static string clean_string(string input)
        {
            return input.Replace("'", "''");
        }
    }
}