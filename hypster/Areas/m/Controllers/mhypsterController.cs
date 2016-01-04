using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recaptcha;
using System.Web.Security;


namespace hypster.Areas.m.Controllers
{
    public class mhypsterController : Controller
    {

        private string fromPassword = "aLameda#503";


        //
        // GET: /hypster/
        //----------------------------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            return RedirectPermanent("/home/index");
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        public ActionResult Contact_Us()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
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


                hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
                emailManager.SendContactUsEmail("noah@baronsmedia.com", "viktor@baronsmedia.com", "jim@baronsmedia.com", Subject, YourEmail, Message);


                return View("Contacts_Thanks");
            }

            return View("Contact_Us");
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        public ActionResult Contacts_Thanks()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------









        //----------------------------------------------------------------------------------------------------------
        public ActionResult About_Us()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Privacy_Policy()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Terms_of_Service()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        public ActionResult How_To_Use()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(Duration = 120)]
        public ActionResult Hypster_On_Mobile()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult How_To_Remove_Account()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(Duration = 120)]
        public ActionResult New_Player_For_Tumblr()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            ViewBag.User_ID = memberManager.getMemberByUserName(User.Identity.Name).id;



            ViewModels.celebsIndexViewModel model = new ViewModels.celebsIndexViewModel();

            hypster_tv_DAL.celebsManagement celebsManager = new hypster_tv_DAL.celebsManagement();

            model.posts_list = celebsManager.GetLatestCelebs_cache(100);

            if (model.posts_list.Count > 0)
                model.featured_seleb = model.posts_list[0];


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
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


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
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


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
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


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
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


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------





        public ActionResult changes_on_hypster()
        {

            return View();
        }









        //----------------------------------------------------------------------------------------------------------
        protected int POSTS_NUM_PAGING = 5;
        protected int CLIPS_NUM_PAGE = 12;
        protected int CLIPS_NUM_PAGING = 16;
        //----------------------------------------------------------------------------------------------------------



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


        [System.Web.Mvc.OutputCache(Duration = 10)]
        public ActionResult T2()
        {
            return View();
        }





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


                return RedirectPermanent("/account/SignIn?ReturnUrl=%2fhypster%2fwelcome");
            }


            return RedirectPermanent("/home");
        }

        public ActionResult welcome()
        {
            return View();
        }




        public ActionResult temp_test()
        {
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




    }
}
