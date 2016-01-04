using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace hypster.Controllers
{
    public class homeController : ControllerBase
    {

        private const int PAGE_LIMIT = 5;

        //
        // GET: /home/



        public ActionResult Index()
        {   
            return View();
        }



        public ActionResult HomeNA()
        {
            return RedirectPermanent("/");
        }







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult visualSearchBar()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            List<hypster_tv_DAL.VisualSearch> model = visualSearchManager.getVisualSearchArtists_cached();

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Get_MostPopularSong_Random()
        {
            hypster_tv_DAL.FeaturedPlaylistManagement fplst_manager = new hypster_tv_DAL.FeaturedPlaylistManagement();

            List<hypster_tv_DAL.FeaturedPlaylist_Result> model = new List<hypster_tv_DAL.FeaturedPlaylist_Result>();
            model = fplst_manager.ReturnFeaturedPlaylists();

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Get_Home_Slideshow()
        {
            hypster_tv_DAL.homeSlideshowManager homeSlideshowManager = new hypster_tv_DAL.homeSlideshowManager();

            List<hypster_tv_DAL.homeSlideshow> model = new List<hypster_tv_DAL.homeSlideshow>();
            model = homeSlideshowManager.getHomeSlideshowActive();

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Get_Featured_Slideshow()
        {
            hypster_tv_DAL.homeSlideshowManager homeSlideshowManager = new hypster_tv_DAL.homeSlideshowManager();

            List<hypster_tv_DAL.homeSlideshow> model = new List<hypster_tv_DAL.homeSlideshow>();
            model = homeSlideshowManager.getFeaturedSlideshowActive();

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult pr_HomPopularCont()
        {
            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();
            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster.ViewModels.HomPopularCont_ViewModel model = new ViewModels.HomPopularCont_ViewModel();
            model.charts_list = chartManager.GetTopCharts();
            model.festivals_list = festivalManager.GetTopFestivals();
            model.popular_playlists = playlistManager.GetMostLikedPlaylists();

            

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult pr_HomExpCont()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 15)]
        public ActionResult Get_More_Content(int id)
        {
            hypster_tv_DAL.DynamicContent_Management dynContent_manager = new hypster_tv_DAL.DynamicContent_Management();

            List<hypster_tv_DAL.DynamicContent> model_list = new List<hypster_tv_DAL.DynamicContent>();

            model_list = dynContent_manager.GetDynamicPages();




            List<hypster_tv_DAL.DynamicContent> model = new List<hypster_tv_DAL.DynamicContent>();
            //----------------------------------------------------------------------------------------------
            int start_pos = 1;
            start_pos = (id * PAGE_LIMIT) - PAGE_LIMIT;

            int end_pos = 1;
            end_pos = start_pos + PAGE_LIMIT;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < model_list.Count)
                {
                    model.Add(model_list[i]);
                }
            }
            //----------------------------------------------------------------------------------------------



            ViewBag.prevPageID = id - 1;
            ViewBag.nextPageID = id + 1;




            //----------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "Endless Scroll";
            recTrack.TrackWebsite_KeyID = 10001;
            recTrack.TrackWebsite_Val = id + " section loaded";
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------



            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string trkskip(string id)
        {
            //----------------------------------------------------------------------------------------------
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = "Skip Songs";
            recTrack.TrackWebsite_KeyID = 30001;
            recTrack.TrackWebsite_Val = id;
            recTrack.TrackDate = DateTime.Now;
            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);


            if (User.Identity.IsAuthenticated)
            {
                int song_id = 0;
                if (Request.QueryString["SID"] != null)
                {
                    Int32.TryParse(Request.QueryString["SID"].ToString(), out song_id);
                }

                hypster_tv_DAL.MemberSongSkip skipSong = new hypster_tv_DAL.MemberSongSkip();
                skipSong.SongSkip_SongID = song_id;
                skipSong.SongSkip_UserID = memberManager.getMemberByUserName(User.Identity.Name).id;
                skipSong.SongSkip_Date = DateTime.Now;
                skipSong.SongSkip_SongGuid = id;
                trackLogin.hyDB.MemberSongSkips.AddObject(skipSong);
            }

            trackLogin.hyDB.SaveChanges();
            //----------------------------------------------------------------------------------------------

            return "";
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string reps(string id)
        {
            //----------------------------------------------------------------------------------------------
            /*
            hypster_tv_DAL.TrackLoginManagement trackLogin = new hypster_tv_DAL.TrackLoginManagement();


            string curr_action = "";
            if (Request.QueryString["a"] != null)
            {
                curr_action = Request.QueryString["a"].ToString();
            }

            int song_id = 0;
            if (Request.QueryString["sid"] != null)
            {
                Int32.TryParse(Request.QueryString["sid"].ToString(), out song_id);
            }




            int action_id = 50001;
            switch (curr_action)
            {
                case "error":
                    {
                        action_id = 50002;
                        
                        hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
                        hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
                        member = membersManager.getMemberByUserName(User.Identity.Name);

                        string song_guid = id;


                        hypster_tv_DAL.DeadLinksManagement deadLinksManager = new hypster_tv_DAL.DeadLinksManagement();

                        hypster_tv_DAL.Dead_Link dl = new hypster_tv_DAL.Dead_Link();
                        dl.DL_Member_ID = member.id;
                        dl.DL_Song_Guid = song_guid;
                        dl.DL_Song_ID = song_id;
                        
                        //mark as dead link
                        deadLinksManager.SubmitDeadLink(dl);
                    }
                    break;
                case "ended":
                    action_id = 50003;
                    break;
                case "prev":
                    action_id = 50004;
                    break;
                case "next":
                    action_id = 50005;
                    break;
                case "dsk":
                    action_id = 50007;
                    break;
                case "MB":
                    action_id = 50009;
                    break;
                case "APP":
                    action_id = 500013;
                    break;
                case "MOVpos":
                    action_id = 500018;
                    break;
                case "NWS":
                    action_id = 500015;
                    break;
                case "SB":
                    action_id = 500023;
                    break;

                default: break;
            }




            hypster_tv_DAL.TrackWebsite recTrack = new hypster_tv_DAL.TrackWebsite();
            recTrack.TrackWebsite_Key = curr_action;
            recTrack.TrackWebsite_KeyID = action_id;
            recTrack.TrackWebsite_Val = id;
            recTrack.TrackDate = DateTime.Now;

            trackLogin.hyDB.TrackWebsites.AddObject(recTrack);
            trackLogin.hyDB.SaveChanges();
            */
            //----------------------------------------------------------------------------------------------


            return "";
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult pr_interstitial()
        {
            //check if 20K views in 24 hours
            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;



            ViewBag.isShow = false;



            if (HttpContext.Request.Cookies["inrs_timeout"] == null || (HttpContext.Request.Cookies["inrs_timeout"] != null && HttpContext.Request.Cookies["inrs_timeout"].Value != "inrs_timeout=1"))
            {
                if (HttpContext.Request.Cookies["inrs_count"] == null)
                {
                    ViewBag.isShow = true;

                    HttpCookie myCookie = new HttpCookie("inrs_timeout");
                    myCookie["inrs_timeout"] = "1";
                    myCookie.Expires = DateTime.Now.AddMinutes(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["inrs_User_Delay_MINUTES"].ToString()));
                    HttpContext.Response.Cookies.Add(myCookie);



                    HttpCookie inrs_countCookie = new HttpCookie("inrs_count");
                    inrs_countCookie["inrs_count"] = "1";
                    inrs_countCookie.Expires = DateTime.Now.AddHours(24);
                    HttpContext.Response.Cookies.Add(inrs_countCookie);
                }
                else
                {
                    if (HttpContext.Request.Cookies["inrs_count"].Value == "inrs_count=1")
                    {
                        ViewBag.isShow = true;


                        HttpContext.Response.Cookies.Remove("inrs_count");
                        HttpCookie inrs_countCookie = new HttpCookie("inrs_count");
                        inrs_countCookie["inrs_count"] = "2";
                        inrs_countCookie.Expires = DateTime.Now.AddHours(24);
                        HttpContext.Response.Cookies.Add(inrs_countCookie);
                    }
                    else
                    {
                        ViewBag.isShow = false;
                    }
                }

            }
            else
            {
                ViewBag.isShow = false;
            }


            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult SplashPage()
        {
            hypster_tv_DAL.newsPost splash_post = new hypster_tv_DAL.newsPost();


            if (Request.Cookies["splashPage"] == null)
            {
                HttpCookie splashCookie = new HttpCookie("splashPage");
                splashCookie.Expires = DateTime.Now.AddDays(1);         //.AddMinutes(1); //need to modify to 24 hours
                Response.Cookies.Add(splashCookie);


                hypster_tv_DAL.sysHypster_Management sysManager = new hypster_tv_DAL.sysHypster_Management();
                int splash_id = 0;
                splash_id = sysManager.GetSplashID();


                hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
                splash_post = newsManager.GetPostByID(splash_id);
            }
            

            return View(splash_post);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    }
}
