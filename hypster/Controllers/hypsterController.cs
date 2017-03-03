using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recaptcha;
using System.Web.Security;
using hypster.Models;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace hypster.Controllers
{
    public class hypsterController : ControllerBase
    {
        private string fromPassword = "aLameda#503";


        //
        // GET: /hypster/
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            return RedirectPermanent("/home/index");
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Contact_Us()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Contact_Us(string YourEmail, string Subject, string Message, bool captchaValid, string captchaErrorMessage)
        {
            if (captchaValid)
            {
                hypster_tv_DAL.Hypster_Entities HypDB = new hypster_tv_DAL.Hypster_Entities();


                hypster_tv_DAL.userContact userContact = new hypster_tv_DAL.userContact();
                userContact.contactType = 1;
                userContact.contactEmail = YourEmail;
                userContact.contactSubject = Subject;
                userContact.contactText = Message;
                HypDB.userContacts.AddObject(userContact);
                HypDB.SaveChanges();


                //hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
                //emailManager.SendContactUsEmail("noah@baronsmedia.com", "viktor@baronsmedia.com", "jim@baronsmedia.com", Subject, YourEmail, Message);
                Code.SendMails sendEmail = new Code.SendMails();
                Response resp = new Response();
                SendEMail sendContactUs = new SendEMail();
                Contact contact;
                Tags tags;
                try
                {
                    contact = new Contact { email = "info@hypster.com" };
                    tags = new Tags { email = YourEmail, subject = Subject, message = Message };
                    sendContactUs = new SendEMail { campaign_id = "2863827", content_id = "2149018", contact = contact, tags = tags };
                    sendEmail.sendMailJson(sendContactUs);
                }
                catch (Exception ex)
                {
                    string tmp_str = ex.Message.ToString();
                    resp.status_description += "\r\n\r\n" + tmp_str;
                    sendContactUs.response.status_description = resp.status_description;
                }

                return View("Contacts_Thanks");
            }

            return View("Contact_Us");
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Contacts_Thanks()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++









        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult About_Us()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Gamers_Rewards_Program()
        {
            ViewBag.Msg = "";
            ViewBag.YourName = "";
            ViewBag.TwitchName = "";
            ViewBag.HypsterName = "";
            ViewBag.YourEmail = "";
            ViewBag.ConfEmail = "";
            ViewBag.Message = "";

            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Gamers_Rewards_Program(string YourName, string TwitchName, string HypsterName, string YourEmail, string ConfEmail, string Message, bool captchaValid, string captchaErrorMessage)
        {
            if (captchaValid)
            {
                if (YourEmail == ConfEmail)
                {
                    hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
                    emailManager.SendGamersRewardsEmail(YourName, TwitchName, HypsterName, YourEmail, ConfEmail, Message);

                    ViewBag.Msg = "<div style='color:#3fbc91; font-size:22px;'>THANKS!</div>WELL CONTACT YOU WITH MORE INFORMATION.<br/> MAKE SURE TO CHECK YOUR SPAM FOLDER IF YOU DON'T HEAR FROM US IN 24 HOURS. <div style='color:#d4ae52;'>HAPPY STREAMING</div>";
                }
                else
                {
                    ViewBag.Msg = "Emails doesn't match. Please double check.";
                    ViewBag.YourName = YourName;
                    ViewBag.TwitchName = TwitchName;
                    ViewBag.HypsterName = HypsterName;
                    ViewBag.YourEmail = YourEmail;
                    ViewBag.ConfEmail = ConfEmail;
                    ViewBag.Message = Message;
                }
            }
            else
            {
                ViewBag.Msg = "Captcha is not valid!";
            }

            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Privacy_Policy()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Terms_of_Service()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult How_To_Use()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [System.Web.Mvc.OutputCache(Duration = 120)]
        public ActionResult Hypster_On_Mobile()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult How_To_Remove_Account()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //[System.Web.Mvc.OutputCache(Duration = 120)]
        public ActionResult New_Player_For_Tumblr()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            ViewBag.User_ID = memberManager.getMemberByUserName(User.Identity.Name).id;



            ViewModels.celebsIndexViewModel model = new ViewModels.celebsIndexViewModel();

            hypster_tv_DAL.celebsManagement celebsManager = new hypster_tv_DAL.celebsManagement();

            model.posts_list = celebsManager.GetLatestCelebs_cache(100);

            if (model.posts_list.Count > 0)
                model.featured_seleb = model.posts_list[0];





            // vvs_tracker
            //----------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "New_Player_For_Tumblr";
            recTrack.TrackWebsite_KeyID = 230011;
            recTrack.TrackWebsite_Val = User.Identity.Name;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------------------




            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code()
        {
            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------




            // vvs_tracker
            //----------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "New_Player_For_Tumblr_Code";
            recTrack.TrackWebsite_KeyID = 230011;
            recTrack.TrackWebsite_Val = User.Identity.Name;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------------------



            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code_1()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------





            // vvs_tracker
            //----------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "New_Player_For_Tumblr_Code_1";
            recTrack.TrackWebsite_KeyID = 230011;
            recTrack.TrackWebsite_Val = User.Identity.Name;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------------------





            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code_2()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------




            // vvs_tracker
            //----------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "New_Player_For_Tumblr_Code_2";
            recTrack.TrackWebsite_KeyID = 230011;
            recTrack.TrackWebsite_Val = User.Identity.Name;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------------------





            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [Authorize]
        public ActionResult New_Classic_Compact_Player()
        {
            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------





            // vvs_tracker
            //----------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "New_Classic_Compact_Player";
            recTrack.TrackWebsite_KeyID = 230011;
            recTrack.TrackWebsite_Val = User.Identity.Name;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------------------




            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        public ActionResult changes_on_hypster()
        {

            return View();
        }









        //----------------------------------------------------------------------------------------------------------
        protected int POSTS_NUM_PAGING = 5;
        protected int CLIPS_NUM_PAGE = 12;
        protected int CLIPS_NUM_PAGING = 16;
        //----------------------------------------------------------------------------------------------------------






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [System.Web.Mvc.OutputCache(Duration = 10)]
        public ActionResult T1()
        {
            hypster_tv.ViewModels.HomePageViewModel model = new hypster_tv.ViewModels.HomePageViewModel();


            ViewBag.CurrPage_Start = 0;
            ViewBag.CurrPage_End = POSTS_NUM_PAGING;


            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            model.posts_list = newsManager.GetLatestNews_cache();



            model.NumOfPosts = model.posts_list.Count;
            double tmp_numPages = (double)model.NumOfPosts / (double)POSTS_NUM_PAGING;
            if ((tmp_numPages - (int)tmp_numPages) > 0)
                tmp_numPages++;
            model.NumOfPages = (int)tmp_numPages;


            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [System.Web.Mvc.OutputCache(Duration = 10)]
        public ActionResult T2()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Conf_Account()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();


            if (Request.QueryString["rid"] != null)
            {
                string email_str = hypster_tv_DAL.EncryptionManager.DecryptString(Request.QueryString["rid"].ToString(), fromPassword);

                hypster_tv_DAL.Member member_conf = new hypster_tv_DAL.Member();
                member_conf = memberManager.getMemberByEmail(email_str);
                member_conf.email_confirmed = 1;
                memberManager.ConfirmMember(member_conf);

                if (User.Identity.IsAuthenticated)
                {
                    return RedirectPermanent("/hypster/welcome");
                }
                else
                {
                    return RedirectPermanent("/account/SignIn?ReturnUrl=%2fhypster%2fwelcome");
                }
            }

            
            return RedirectPermanent("/home");
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++









        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult welcome()
        {
            return View();
        }



        [hypster.Code.TrackUser]
        public ActionResult temp_test()
        {
            return View();
        }


        [hypster.Code.TrackUser]
        public ActionResult temp_test_2()
        {
            hypster_tv_DAL.Email_Manager email_manager = new hypster_tv_DAL.Email_Manager();
            ViewBag.STR = email_manager.GenerateConfirmString("sayeh1999@gmail.com");


            return View();
        }


        public ActionResult widget_977music()
        {

            return View();
        }


        public ActionResult gowilkes()
        {

            return View();
        }


        public ActionResult Ayah()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Ayah(string email, string username)
        {

            return View();
        }



        public ActionResult media_net()
        {
            return View();
        }



        public ActionResult apps()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




        public ActionResult what_is_hipster_vs_hypster()
        {
            return View();
        }
    }
}
