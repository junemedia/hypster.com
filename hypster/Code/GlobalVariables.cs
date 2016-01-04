using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.Code
{
    public static class GlobalVariables
    {
        // readonly variable
        public static string GV
        {
            get
            {
                return "GV";
            }
        }

        // read-write variable
        public static string NumOfSearches
        {
            get
            {
                return HttpContext.Current.Application["NumOfSearches"] as string;
            }
            set
            {
                HttpContext.Current.Application["NumOfSearches"] = value;
            }
        }
    }
}