using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class listenController : ControllerBase
    {

        public const int CHARTS_LIMIT = 16;
        





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            
            


            if (User.Identity.IsAuthenticated == true)
            {
                model.most_viewed_playlists = playlistManager.GetUserPlaylists(memberManager.getMemberByUserName(User.Identity.Name).id);

                //get playlists I like
                model.playlists_I_like = playlistManager.GetPlaylistsILike(memberManager.getMemberByUserName(User.Identity.Name).id);
            }
            


            //check if search requested
            if (Request.QueryString["ss"] != null)
            {
                ViewBag.searchString = Request.QueryString["ss"];
            }



            if (Request.QueryString["PLST"] != null)
            {
                string _action = Request.QueryString["PLST"].ToString();
                if (_action != "")
                {
                    ViewBag.PLST = true;
                }
            }


            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Music()
        {
            return RedirectToAction("Index");
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Radio()
        {

            ViewBag.ActiveMenu = "RADIO";




            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();




            if (User.Identity.IsAuthenticated == true)
            {
                model.most_viewed_playlists = playlistManager.GetUserPlaylists(memberManager.getMemberByUserName(User.Identity.Name).id);

                //get playlists I like
                model.playlists_I_like = playlistManager.GetPlaylistsILike(memberManager.getMemberByUserName(User.Identity.Name).id);
            }



            //check if search requested
            if (Request.QueryString["ss"] != null)
            {
                ViewBag.searchString = Request.QueryString["ss"];
            }


            if (Request.QueryString["PLST"] != null)
            {
                string _action = Request.QueryString["PLST"].ToString();
                if (_action != "")
                {
                    ViewBag.PLST = true;
                }
            }


            return View("Index", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Playlists(string id)
        {
            return RedirectPermanent("/");
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // default visual search based on most popular genres
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult visualSearchBar()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            
            int genre_1 = 18;
            int genre_2 = 10;
            int genre_3 = 19;
            int genre_4 = 1;
            int genre_5 = 9;
            int genre_6 = 7;

            List<hypster_tv_DAL.VisualSearch> model = new List<hypster_tv_DAL.VisualSearch>();
            model = visualSearchManager.getVisualSearchArtistsByGenres(genre_1, genre_2, genre_3, genre_4, genre_5, genre_6);

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //no cache since authentication applied
        public ActionResult visualSearchBarUser()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            

            int genre_1 = 18;
            int genre_2 = 10;
            int genre_3 = 19;
            int genre_4 = 1;
            int genre_5 = 9;
            int genre_6 = 7;


            if (User.Identity.IsAuthenticated)
            {
                List<hypster_tv_DAL.MemberMusicGenre> genres_list = new List<hypster_tv_DAL.MemberMusicGenre>();
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();


                curr_user = memberManager.getMemberByUserName(User.Identity.Name);
                genres_list = genreManager.GetUserMusicGenres(curr_user.id);

                if (genres_list.Count > 0)
                    genre_1 = (int)genres_list[0].MusicGenre_ID;
                if (genres_list.Count > 1)
                    genre_2 = (int)genres_list[1].MusicGenre_ID;
                if (genres_list.Count > 2)
                    genre_3 = (int)genres_list[2].MusicGenre_ID;
                if (genres_list.Count > 3)
                    genre_4 = (int)genres_list[3].MusicGenre_ID;
                if (genres_list.Count > 4)
                    genre_5 = (int)genres_list[4].MusicGenre_ID;
                if (genres_list.Count > 5)
                    genre_6 = (int)genres_list[5].MusicGenre_ID;
            }


            List<hypster_tv_DAL.VisualSearch> model = new List<hypster_tv_DAL.VisualSearch>();
            model = visualSearchManager.getVisualSearchArtistsByGenres(genre_1, genre_2, genre_3, genre_4, genre_5, genre_6);


            return View("visualSearchBar", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //ajax calls served here
        public string visualSearchBarLoad(int id)
        {
            string visual_search_string = "";

            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();

            int genre_1 = 18;
            int genre_2 = 10;
            int genre_3 = 19;
            int genre_4 = 1;
            int genre_5 = 9;
            int genre_6 = 7;

            List<hypster_tv_DAL.VisualSearch> model = new List<hypster_tv_DAL.VisualSearch>();
            model = visualSearchManager.getVisualSearchArtistsByGenres(genre_1, genre_2, genre_3, genre_4, genre_5, genre_6);



            if (id >= model.Count)
                return "";

            int limiter = 10;
            int wwWidth = 0;
            for (int i = id; i < model.Count; i++)
            {
                string ar_name = model[i].Artist_Name.Replace("_", " ");
                visual_search_string += "<img class=\"VisSearchItm\" title=\"click to search for " + ar_name + "\" alt=\"" + ar_name + "\" src=\"/imgs/visualSearch/" + model[i].Artist_Name + ".jpg\" onclick=\"VisualSearchClick('" + model[i].Artist_Name.Replace("_", "+") + "');\" />";

                if (limiter <= 0)
                    break;

                limiter--;

                wwWidth += (int)model[i].ImWidth;
            }

            return visual_search_string + "|" + wwWidth;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult RadioStationsBar()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model.genres_list = genreManager.GetMusicGenresList();


            hypster_tv_DAL.chartsManager chartsManager = new hypster_tv_DAL.chartsManager();
            model.charts_list = chartsManager.GetAllCharts();
            if (model.charts_list.Count > CHARTS_LIMIT)
            {
                model.charts_list.RemoveRange(CHARTS_LIMIT, model.charts_list.Count - CHARTS_LIMIT); 
            }


            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();
            model.festivals_list = festivalManager.GetAllFestivals();
            if (model.festivals_list.Count > CHARTS_LIMIT)
            {
                model.festivals_list.RemoveRange(CHARTS_LIMIT, model.festivals_list.Count - CHARTS_LIMIT);
            }

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult GetMostViewedPlaylists()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.most_viewed_playlists = playlistManager.GetMostViewedPlaylists();

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //similar tool
        public ActionResult Similar()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    }
}
