using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dAccountController : Controller
    {
        //
        // GET: /HypDesktop/dAccount/


        public ActionResult Index()
        {
            return View();
        }


        public string Login()
        {
            string ret_str = "FAILED";



            string GUID = "";
            if (Request.QueryString["GUID"] != null)
            {
                GUID = Request.QueryString["GUID"].ToString();


                //strip guid and get id and pass phrase
                string guid_USNAME = "";
                string guid_VAL = "";
                string [] guid_arr = GUID.Split('~');

                if (guid_arr.Length >= 2)
                {
                    guid_USNAME = guid_arr[0];
                    guid_VAL = guid_arr[1];
                    
                    
                    hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                    hypster_tv_DAL.Member member_check = new hypster_tv_DAL.Member();
                    member_check = memberManager.getMemberByUserName(guid_USNAME);


                    if (member_check.id > 0)
                    {
                        string guid_check = "";
                        guid_check = hypster_tv_DAL.EncryptionManager.EncryptString(member_check.username, member_check.password);


                        if (guid_check == guid_VAL)
                        {
                            string p_user_id = member_check.id.ToString();
                            string p_user_playlist = member_check.active_playlist.ToString();
                            string p_user_name = member_check.username.Replace('|',' ');
                            ret_str = "SUCCESS" + "|" + p_user_id + "|" + p_user_playlist + "|" + p_user_name + "|";
                        }
                    }
                    

                }
            }


            



            return ret_str;
        }



        public string Exception()
        {
            string ret_str = "";


            if (Request.QueryString["EXC"] != null)
            {
                hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();


                //-----------------------------------------------------------------------------
                //collect exc data
                string action = "";
                string controller = "HypDesktop";
                string message = "";
                message += " | " + Request.QueryString["EXC"].ToString();
                

                string url_string = "";
                string IP_Address;
                IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (IP_Address == null)
                    IP_Address = Request.ServerVariables["REMOTE_ADDR"];
                else
                    IP_Address = "";
                //-----------------------------------------------------------------------------


                string member_str = "";
                if (User.Identity.IsAuthenticated)
                {
                    member_str = User.Identity.Name;
                }


                //-----------------------------------------------------------------------------
                // save exc data
                hypster_tv_DAL.HypException i_exception = new hypster_tv_DAL.HypException();
                i_exception.ExcDate = DateTime.Now;
                i_exception.ExcIP = IP_Address;
                i_exception.ExcUrl = url_string;
                i_exception.ExcMethod = controller + ":" + action + ":" + member_str;
                i_exception.ExcMessage = message;

                if (i_exception.ExcUrl.Length > 145)
                {
                    i_exception.ExcUrl = i_exception.ExcUrl.Substring(0, 145);
                }

                if (i_exception.ExcMethod.Length > 145)
                {
                    i_exception.ExcMethod = i_exception.ExcMethod.Substring(0, 145);
                }

                if (i_exception.ExcMessage.Length > 345)
                {
                    i_exception.ExcMessage = i_exception.ExcMessage.Substring(0, 345);


                }

                hyDB.HypExceptions.AddObject(i_exception);
                hyDB.SaveChanges();
                //-----------------------------------------------------------------------------

            }

            return ret_str;
        }






        //
        // install / uninstall
        //
        // InsUnins?ActionType=INSTALL
        //
        public string InsUnins()
        {

            string action = "";

            if (Request.QueryString["ActionType"] != null)
            {
                action = Request.QueryString["ActionType"].ToString();

                hypster_tv_DAL.sysHypster_Management sysManager = new hypster_tv_DAL.sysHypster_Management();

                switch (action)
                {
                    case "INSTALL":
                        sysManager.AddNewInstall();
                        break;
                    case "UNINSTALL":
                        sysManager.AddNewUninstall();
                        break;
                    default:
                        break;
                }
            }



            return "";
        }




    }
}
