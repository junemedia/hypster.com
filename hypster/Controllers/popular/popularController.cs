using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers.popular
{
    public class popularController : ControllerBase
    {


        // GET: /popular/
        //
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





        [OutputCache(Duration = 180, VaryByParam = "none")]
        public ActionResult FeaturedPlaylists()
        {
            hypster_tv_DAL.FeaturedPlaylistManagement fplst_manager = new hypster_tv_DAL.FeaturedPlaylistManagement();


            List<hypster_tv_DAL.FeaturedPlaylist_Result> model = new List<hypster_tv_DAL.FeaturedPlaylist_Result>();
            model = fplst_manager.ReturnFeaturedPlaylists();


            return View(model);
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
            model = genreManager.GetMusicGenresListPopular_Random();


            return View(model);
        }



        [OutputCache(Duration = 90, VaryByParam = "none")]
        public ActionResult PopularRadioStations_NA()
        {
            List<hypster_tv_DAL.MusicGenre> model = new List<hypster_tv_DAL.MusicGenre>();


            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model = genreManager.GetMusicGenresListPopular_Random();


            return View(model);
        }



        
        public ActionResult PopularRadioStations_Random()
        {
            List<hypster_tv_DAL.MusicGenre> model = new List<hypster_tv_DAL.MusicGenre>();


            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model = genreManager.GetMusicGenresList_Random();


            return View(model);
        }




        [OutputCache(Duration = 195, VaryByParam = "none")]
        public ActionResult PopularCharts_Chart()
        {
            List<hypster_tv_DAL.PlaylistData_Song> char_songs = new List<hypster_tv_DAL.PlaylistData_Song>();


            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();

            List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
            charts_list = chartManager.GetAllCharts();


            //Random random_chart = new Random();
            int chart_num = 0; //random_chart.Next(0, charts_list.Count - 1);

            ViewBag.chart_user_id = charts_list[chart_num].Chart_User_ID;
            ViewBag.chart_playlist_id = charts_list[chart_num].Chart_Playlist_ID;
            ViewBag.chart_name = charts_list[chart_num].Chart_Name;
            ViewBag.chart_guid = charts_list[chart_num].Chart_GUID;


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            char_songs = playlistManager.GetSongsForPlayList((int)charts_list[chart_num].Chart_User_ID, (int)charts_list[chart_num].Chart_Playlist_ID);


            return View(char_songs);
        }





        //dynamic charts generation
        [OutputCache(Duration = 95)]
        public ActionResult PopularCharts_GetChartByGuid(string id)
        {
            List<hypster_tv_DAL.PlaylistData_Song> char_songs = new List<hypster_tv_DAL.PlaylistData_Song>();


            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();

            hypster_tv_DAL.Chart chart = new hypster_tv_DAL.Chart();
            chart = chartManager.GetChartByGuid(id);



            ViewBag.chart_user_id = chart.Chart_User_ID;
            ViewBag.chart_playlist_id = chart.Chart_Playlist_ID;
            ViewBag.chart_name = chart.Chart_Name;


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            char_songs = playlistManager.GetSongsForPlayList((int)chart.Chart_User_ID, (int)chart.Chart_Playlist_ID);

            // same as "PopularCharts_Chart" but with tracking
            return View(char_songs);
        }









        //DEPRECATED
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


        //DEPRECATED
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
