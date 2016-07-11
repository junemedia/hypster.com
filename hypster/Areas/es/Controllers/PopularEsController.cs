using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class PopularEsController : Controller
    {
        //
        // GET: /es/PopularEs/

        public ActionResult Index()
        {
            return View();
        }






        [OutputCache(Duration = 180, VaryByParam = "none")]
        public ActionResult PopularPlaylists()
        {
            hypster_tv_DAL.playlistManagement playlistsManager = new hypster_tv_DAL.playlistManagement();
            List<hypster_tv_DAL.Playlist> most_viewed_playlists = new List<hypster_tv_DAL.Playlist>();
            most_viewed_playlists = playlistsManager.GetMostViewedPlaylists();


            return View(most_viewed_playlists);
        }




        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult PopularMusicOnHypster()
        {
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> most_popular_songs = new List<hypster_tv_DAL.Song>();
            most_popular_songs = songManager.Get_MostPopularSong_Random();


            return View(most_popular_songs);
        }




        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult PopularVideos()
        {
            List<hypster_tv_DAL.videoClip> TopVideos = new List<hypster_tv_DAL.videoClip>();

            hypster_tv_DAL.videoClipManager videoManager = new hypster_tv_DAL.videoClipManager();
            TopVideos = videoManager.getRandomVideos_cache(8);

            return View(TopVideos);
        }




        [OutputCache(Duration = 90, VaryByParam = "none")]
        public ActionResult PopularRadioStations()
        {
            List<hypster_tv_DAL.MusicGenre> model = new List<hypster_tv_DAL.MusicGenre>();


            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model = genreManager.GetMusicGenresList_Random();


            return View(model);
        }




        [OutputCache(Duration = 95, VaryByParam = "none")]
        public ActionResult PopularCharts_ChartBillboard()
        {
            List<hypster_tv_DAL.PlaylistData_Song> char_songs = new List<hypster_tv_DAL.PlaylistData_Song>();


            int chart_user_id = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Charts_Billboard_User"]);
            int chart_playlist_id = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Charts_Billboard_Plst"]);


            ViewBag.chart_user_id = chart_user_id;
            ViewBag.chart_playlist_id = chart_playlist_id;


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            char_songs = playlistManager.GetSongsForPlayList(chart_user_id, chart_playlist_id);



            return View(char_songs);
        }


        [OutputCache(Duration = 95, VaryByParam = "none")]
        public ActionResult PopularCharts_ChartItunes()
        {
            List<hypster_tv_DAL.PlaylistData_Song> char_songs = new List<hypster_tv_DAL.PlaylistData_Song>();


            int chart_user_id = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Charts_Itunes_User"]);
            int chart_playlist_id = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Charts_Itunes_Plst"]);


            ViewBag.chart_user_id = chart_user_id;
            ViewBag.chart_playlist_id = chart_playlist_id;


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            char_songs = playlistManager.GetSongsForPlayList(chart_user_id, chart_playlist_id);



            return View(char_songs);
        }


    }
}
