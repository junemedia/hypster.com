using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.apps.Controllers
{
    public class appListenController : Controller
    {

        //
        //------------------------------------------------------------------
        public const int CHARTS_LIMIT = 16;
        //------------------------------------------------------------------




        public ActionResult Index()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            //model.genres_list = genreManager.GetMusicGenresList();
            //model.most_popular_songs = songManager.Get_MostPopularSong_Random();
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






        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult GetMostViewedPlaylists()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.most_viewed_playlists = playlistManager.GetMostViewedPlaylists();

            return View(model);
        }





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






    }
}

