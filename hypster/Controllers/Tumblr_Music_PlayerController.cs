using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolveMedia;

namespace hypster.Controllers
{
    public class Tumblr_Music_PlayerController : Controller
    {

        //
        // GET: /Tumblr_Music_Player/
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string processCaptcha(string adcopy_challenge, string adcopy_response)
        {
            //LIVE
            ViewBag.ChallengeKey = System.Configuration.ConfigurationManager.AppSettings["SolveMediaChallengeKey"];


            //CHECK IF RECAPTCHA CODE VALID
            //-----------------------------------------------------------------------------------------------------
            SolveMedia.SolveMediaValidator validator = new SolveMediaValidator();
            string message_recaptcha = "";
            bool is_recaptch_valid = validator.check_answer(System.Configuration.ConfigurationManager.AppSettings["SolveMediaVerificationKey"], System.Configuration.ConfigurationManager.AppSettings["SolveMediaHashKey"], adcopy_challenge, adcopy_response, out message_recaptcha);
            //-----------------------------------------------------------------------------------------------------



            //save captcha
            //-----------------------------------------------------------------------------------------------------

            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();

            string IP_Address;
            IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IP_Address == null)
                IP_Address = Request.ServerVariables["REMOTE_ADDR"];
            else
                IP_Address = "";



            //save captcha code
            //-----------------------------------------------------------------------------------
            hypster_tv_DAL.CaptchaLog captchaLog = new hypster_tv_DAL.CaptchaLog();
            captchaLog.CaptchaIP = IP_Address;
            captchaLog.CatpchaDate = DateTime.Now.AddHours(3);
            captchaLog.CaptchaFrase = adcopy_response;
            captchaLog.CaptchaResponse = is_recaptch_valid.ToString() + ": " + message_recaptcha;
            hyDB.CaptchaLogs.AddObject(captchaLog);
            hyDB.SaveChanges();

            //-----------------------------------------------------------------------------------------------------


            return is_recaptch_valid.ToString();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string processAdCaptcher(string kcodecaptcha, string kcodekey)
        {
            

            //CHECK IF RECAPTCHA CODE VALID
            //-----------------------------------------------------------------------------------------------------
            string message_recaptcha = "";
            bool is_recaptch_valid = false;


            string req_url = "http://code.adcaptcher.com/check" + kcodekey + "/" + kcodecaptcha;

            System.Net.WebRequest request = System.Net.WebRequest.Create(req_url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            System.IO.Stream dataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);

            message_recaptcha = reader.ReadToEnd();
            //-----------------------------------------------------------------------------------------------------



            return message_recaptcha.ToString();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





    }
}
