using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers.account
{
    public class AcctController : Controller
    {

        private string str_user_version = "";
        private string str_curr_version = "";

        private double i_USER_VERSION = 0;
        private double i_CURR_VERSION = 0;


        //
        // GET: /Acct/

        public ActionResult Index()
        {
            return View();
        }


        public string Interface(string id)
        {
            switch (id)
            {
                case "VersionCheck.aspx":
                    
                    //------------------------------------------------------------
                    // get user version
                    if (Request.QueryString["CV"] != null)
                        str_user_version = Request.QueryString["CV"].ToString();
                    else
                        str_user_version = "0";

                    double.TryParse(str_user_version, out i_USER_VERSION);
                    //------------------------------------------------------------



                    //------------------------------------------------------------
                    // get current version
                    str_curr_version = "13";
                    double.TryParse(str_curr_version, out i_CURR_VERSION);
                    //------------------------------------------------------------



                    if (i_USER_VERSION < i_CURR_VERSION)
                    {
                        //return "TRUE NEW VERSION";
                    }


                    break;

                default:
                    break;
            }

            return "";
        }



    }
}
