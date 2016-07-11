using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recaptcha;
using System.Web.Security;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dFeedbackController : Controller
    {
        //
        // GET: /HypDesktop/Feedback/

        public ActionResult Index()
        {
            return View();
        }


        //----------------------------------------------------------------------------------------------------------
        [RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Index(string YourEmail, string Message, bool captchaValid, string captchaErrorMessage)
        {
            if (captchaValid)
            {
                hypster_tv_DAL.Hypster_Entities HypDB = new hypster_tv_DAL.Hypster_Entities();


                hypster_tv_DAL.userContact userContact = new hypster_tv_DAL.userContact();
                userContact.contactType = 1;
                userContact.contactEmail = YourEmail;
                userContact.contactSubject = "HypDesktop Feedback";
                userContact.contactText = Message;
                HypDB.userContacts.AddObject(userContact);
                HypDB.SaveChanges();


                hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
                emailManager.SendContactUsEmail("noah@baronsmedia.com", "viktor@baronsmedia.com", "jim@baronsmedia.com", "HypDesktop Feedback", YourEmail, Message);


                ViewBag.RES = "1";
                return View("Index");
            }

            
            return View("Index");
        }
        //----------------------------------------------------------------------------------------------------------

    }
}
