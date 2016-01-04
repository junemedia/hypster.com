using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recaptcha;

namespace hypster.Areas.ru.Controllers
{
    public class ХипстерController : Controller
    {
        //
        // GET: /ru/Хипстер/

        public ActionResult Index()
        {
            return RedirectPermanent("/ru/Главная");
        }


        //----------------------------------------------------------------------------------------------------------
        public ActionResult Контакты()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        [RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Контакты(string YourEmail, string Subject, string Message, bool captchaValid, string captchaErrorMessage)
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


                return View("Контакты_Спасибо");
            }

            return View("Контакты");
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        public ActionResult Контакты_Спасибо()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------









        //----------------------------------------------------------------------------------------------------------
        public ActionResult Про_Нас()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Конфиденциальность()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Пользовательское_Соглашение()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        public ActionResult Как_использовать()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(Duration = 120)]
        public ActionResult Хипстер_на_мобильном()
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
        public ActionResult Плеер_для_Тумблера()
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



    }
}
