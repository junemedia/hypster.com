using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;
using System.Collections;


namespace hypster.Areas.senses.Controllers
{
    [AuthorizeBlack]
    public class sListenController : Controller
    {
        //
        // GET: /senses/sListen/

        public ActionResult Index()
        {
            return View();
        }




        //----------------------------------------------------------------------------------------------------
        //+++
        //[OutputCache(Duration = 3)]
        public ActionResult user(string id)
        {

            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------





            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            string plst_id_str = ""; //Request.QueryString.ToString();

            if (Request.QueryString.Count > 0)
            {
                plst_id_str = Request.QueryString[0].ToString();
            }


            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            //detect curr member
            //
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByUserName(id);
            ViewBag.UserID = member.id;
            ViewBag.Username = member.username;
            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();


            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            int PLAYLIST_ID = 0;
            if (plst_id_str != null && plst_id_str != "")
            {
                if (Int32.TryParse(plst_id_str, out PLAYLIST_ID) == false)
                {
                    PLAYLIST_ID = member.active_playlist;
                }
            }
            else
            {
                PLAYLIST_ID = member.active_playlist;
            }




            if (ViewBag.UserID != 0 && PLAYLIST_ID != 0)
            {
                playlistManager.AddPlaylistView(PLAYLIST_ID);
                curr_playlist = playlistManager.GetUserPlaylistById(Int32.Parse(ViewBag.UserID.ToString()), PLAYLIST_ID);
                model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
            }
            //-----------------------------------------------------------------------------------------------------






            //-----------------------------------------------------------------------------------------------------
            //set Page Title
            string title_str = "";
            if (curr_playlist.description != "")
            {
                title_str = curr_playlist.description;
            }
            else
            {
                title_str = curr_playlist.name + " ";
            }
            ViewBag.PageTitle = title_str + "- music playlist";
            ViewBag.Desc = curr_playlist.name + " with music of " + curr_playlist.description + "...";
            ViewBag.DescLength = curr_playlist.description.Length;


            ViewBag.PlaylistID = PLAYLIST_ID;
            ViewBag.PlaylistName = curr_playlist.name;
            ViewBag.PlaylistLikes = curr_playlist.Likes;
            ViewBag.PlaylistViews = curr_playlist.ViewsNum;
            //-----------------------------------------------------------------------------------------------------





            //7.get tags for selected playlist
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();

            List<hypster_tv_DAL.sp_Tag_GetPlaylistTags_Result> tags_list = new List<hypster_tv_DAL.sp_Tag_GetPlaylistTags_Result>();
            tags_list = tagManager.GetPlaylistTags(PLAYLIST_ID);

            string tags_string = "";
            foreach (var item in tags_list)
            {
                tags_string += item.Tag_Name + ", ";
            }


            if (tags_string != "")
            {
                ViewBag.TagsList = tags_string.Split(',');
            }
            else
            {
                ViewBag.TagsList = curr_playlist.description.Split(',');
            }
            //-----------------------------------------------------------------------------------------------------






            //-----------------------------------------------------------------------------------------------------
            // temp tracking code for BRNA playlist
            try
            {
                if (PLAYLIST_ID == 6739915)
                {
                    HttpCookie myCookie = new HttpCookie("ETT");
                    myCookie["ETT"] = "1";
                    myCookie.Expires = DateTime.Now.AddDays(3);
                    HttpContext.Response.Cookies.Add(myCookie);
                }
            }
            finally { }
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            if (member.id != 0)
            {
                model.userPlaylists_list = playlistManager.GetUserPlaylists(member.id);
            }
            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------







        //----------------------------------------------------------------------------------------------------
        //++
        //[OutputCache(Duration = 10)]
        public ActionResult artist(string id)
        {
            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            string curr_page_str = ""; //Request.QueryString.ToString();

            if (Request.QueryString.Count > 0)
            {
                curr_page_str = Request.QueryString[0].ToString();
            }

            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }




            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            ViewBag.MEDIA_TYPE = "artist";


            string Genre_str = "";
            if (id != null)
            {
                Genre_str = id.Replace("_and_", "&").Replace("+", " "); ;
                ViewBag.Genre = Genre_str;
            }

            //set Page Title
            ViewBag.PageTitle = Genre_str + " - music playlist";
            ViewBag.Desc = Genre_str + ".";

            ViewBag.PlaylistName = Genre_str + " - music playlist";
            //--------------------------------------------------------------------------------------------






            //************************************************************************************************************************
            bool __genre_found = false;
            //************************************************************************************************************************





            //--------------------------------------------------------------------------------------------
            string search_str1 = "";
            if (Request.QueryString["search"] != null)
            {
                search_str1 = Request.QueryString["search"];
                ViewBag.search = search_str1;
            }
            YouTubeRequestSettings settings1 = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
            YouTubeRequest request1 = new YouTubeRequest(settings1);




            string orderBy = "relevance";
            if (orderBy != "")
                orderBy = "&orderby=" + orderBy;

            int this_page1 = 1;
            if (curr_page_str != null)
            {
                if (Int32.TryParse(curr_page_str, out this_page1) == false)
                    this_page1 = 1;
            }


            //string feedUrl1 = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}" + orderBy, HttpUtility.UrlEncode(id.Replace("+", " ")), Request.ServerVariables["REMOTE_ADDR"], (this_page1 * 25) - 25 + 1);

            string feedUrl1 = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&start-index={1}" + orderBy, HttpUtility.UrlEncode(id.Replace("+", " ")), (this_page1 * 25) - 25 + 1);
            Feed<Video> videoFeed1 = request1.Get<Video>(new Uri(feedUrl1));
            foreach (Video item in videoFeed1.Entries)
            {
                hypster_tv_DAL.PlaylistData_Song song_add1 = new hypster_tv_DAL.PlaylistData_Song();
                song_add1.YoutubeId = item.VideoId;
                song_add1.Title = item.Title;
                model.songs_list.Add(song_add1);
            }



            ViewBag.UserID = 0;
            ViewBag.PlaylistID = 0;
            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------







        //----------------------------------------------------------------------------------------------------
        //++
        //[OutputCache(Duration = 10)]
        public ActionResult chart(string id)
        {
            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }




            //1.1
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();

            hypster_tv_DAL.Chart curr_chart = new hypster_tv_DAL.Chart();
            curr_chart = chartManager.GetChartByGuid(id);
            //--------------------------------------------------------------------------------------------





            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            string plst_id_str = curr_chart.Chart_Playlist_ID.ToString();


            //detect curr member
            //
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByID((int)curr_chart.Chart_User_ID);
            ViewBag.UserID = member.id;
            ViewBag.Username = member.username;
            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            int PLAYLIST_ID = 0;

            PLAYLIST_ID = (int)curr_chart.Chart_Playlist_ID;



            if (ViewBag.UserID != 0 && PLAYLIST_ID != 0)
            {
                playlistManager.AddPlaylistView(PLAYLIST_ID);
                curr_playlist = playlistManager.GetUserPlaylistById(Int32.Parse(ViewBag.UserID.ToString()), PLAYLIST_ID);
                model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
            }

            //set Page Title
            ViewBag.PageTitle = curr_chart.Chart_Name + " - chart playlist ";
            //ViewBag.Desc = curr_chart.Chart_Desc + ".";
            ViewBag.Desc = "Chart with music of " + curr_playlist.description + "...";
            ViewBag.DescLength = curr_playlist.description.Length;
            ViewBag.TagsList = curr_playlist.description.Split(',');


            ViewBag.PlaylistID = PLAYLIST_ID;
            ViewBag.PlaylistName = curr_chart.Chart_Name + " - chart playlist ";
            ViewBag.PlaylistLikes = curr_playlist.Likes;
            ViewBag.PlaylistViews = curr_playlist.ViewsNum;

            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------
        //++
        [OutputCache(Duration = 10)]
        public ActionResult festival(string id)
        {
            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }




            //1.1
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();

            hypster_tv_DAL.Festival curr_festival = new hypster_tv_DAL.Festival();
            curr_festival = festivalManager.GetFestivalByGuid(id);
            //--------------------------------------------------------------------------------------------





            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            string plst_id_str = curr_festival.Festival_Playlist_ID.ToString();


            //detect curr member
            //
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByID((int)curr_festival.Festival_User_ID);
            ViewBag.UserID = member.id;
            ViewBag.Username = member.username;
            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            int PLAYLIST_ID = 0;

            PLAYLIST_ID = (int)curr_festival.Festival_Playlist_ID;



            if (ViewBag.UserID != 0 && PLAYLIST_ID != 0)
            {
                playlistManager.AddPlaylistView(PLAYLIST_ID);
                curr_playlist = playlistManager.GetUserPlaylistById(Int32.Parse(ViewBag.UserID.ToString()), PLAYLIST_ID);
                model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
            }


            //set Page Title
            ViewBag.PageTitle = curr_festival.Festival_Name + " - festival playlist";
            //ViewBag.Desc = curr_festival.Festival_Desc + ".";
            ViewBag.Desc = "Festival with music of " + curr_playlist.description + "...";
            ViewBag.DescLength = curr_playlist.description.Length;
            ViewBag.TagsList = curr_playlist.description.Split(',');


            ViewBag.PlaylistID = PLAYLIST_ID;
            ViewBag.PlaylistName = curr_festival.Festival_Name + " - festival playlist";
            ViewBag.PlaylistLikes = curr_playlist.Likes;
            ViewBag.PlaylistViews = curr_playlist.ViewsNum;

            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------
        //++
        //[OutputCache(Duration = 10)]
        public ActionResult song(string id)
        {

            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            string song_guid = id;        //Request.QueryString.ToString();





            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();



            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();



            ViewBag.MEDIA_TYPE = "song";


            string SongGuid = "";
            if (song_guid != null)
            {
                SongGuid = song_guid;
                ViewBag.SongGuid = SongGuid.Replace("&", "amp;");
            }


            string SongTitle = "";
            if (Request.QueryString != null) //id != null)
            {
                SongTitle = HttpContext.Server.UrlDecode(Request.QueryString.ToString());       //id;  //ViewBag.SongTitle = SongTitle.Replace("&", "amp;");
                ViewBag.SongTitle = SongTitle;       //SongTitle.Replace("&", "amp;");
            }






            hypster_tv_DAL.PlaylistData_Song song = new hypster_tv_DAL.PlaylistData_Song();
            song.YoutubeId = SongGuid;
            song.Title = SongTitle;
            //.Replace("+", " "); //.Replace("%26", "&");



            //set Page Title
            ViewBag.PageTitle = song.Title + " - song";
            ViewBag.Desc = song.Title + ".";

            ViewBag.PlaylistName = song.Title;




            model.songs_list.Add(song);



            if (model.songs_list.Count > 0)
                ViewBag.SongGuid = model.songs_list[0].YoutubeId;
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            ViewBag.ShowRecommendations = 1;
            //--------------------------------------------------------------------------------------------



            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------







        //----------------------------------------------------------------------------------------------------
        //+++
        //[OutputCache(Duration = 10)]
        public ActionResult station(string id)
        {
            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------




            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }




            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            ViewBag.MEDIA_TYPE = "radio";


            string Genre_str = "";
            if (id != null)
            {
                Genre_str = id.Replace("_and_", "&").Replace("+", " ").Replace(" 27", "'");
                ViewBag.Genre = Genre_str;
            }



            //set Page Title
            ViewBag.PageTitle = Genre_str + " - music station";
            ViewBag.Desc = Genre_str + ".";

            ViewBag.PlaylistName = Genre_str + " - music station";



            //************************************************************************************************************************
            hypster_tv_DAL.MemberMusicGenreManager musicGenre_Manager = new hypster_tv_DAL.MemberMusicGenreManager();

            List<hypster_tv_DAL.MusicGenre> music_genres_list = new List<hypster_tv_DAL.MusicGenre>();

            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["MPL_Genres"] != null)
            {
                music_genres_list = (List<hypster_tv_DAL.MusicGenre>)i_chache["MPL_Genres"];
            }
            else
            {
                music_genres_list = musicGenre_Manager.GetMusicGenresList();
                i_chache.Add("MPL_Genres", music_genres_list, DateTime.Now.AddSeconds(1500)); //15 mins
            }
            //************************************************************************************************************************





            //************************************************************************************************************************
            bool __genre_found = false;
            foreach (var item in music_genres_list)
            {
                if (Genre_str == item.GenreName)
                {
                    playlistManager.AddPlaylistView(item.Playlist_ID);
                    model.songs_list = playlistManager.GetSongsForPlayList_Random((int)item.User_ID, (int)item.Playlist_ID);
                    __genre_found = true;


                    ViewBag.UserID = item.User_ID;
                    ViewBag.PlaylistID = item.Playlist_ID;
                }
            }
            //************************************************************************************************************************





            //************************************************************************************************************************
            if (__genre_found == false)
            {
                string search_str = "";
                if (Request.QueryString["search"] != null)
                {
                    search_str = Request.QueryString["search"];
                    ViewBag.search = search_str;
                }
                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);

                int this_page = 1;
                for (this_page = 1; this_page < 10; this_page++)
                {
                    //string feedUrl = "http://gdata.youtube.com/feeds/api/videos?q=" + Genre_str + "&category=Music&format=5&restriction=" + Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + ((this_page * 25) - 24) + "&orderby=viewCount";

                    string feedUrl = "http://gdata.youtube.com/feeds/api/videos?q=" + Genre_str + "&category=Music&format=5&start-index=" + ((this_page * 25) - 24) + "&orderby=viewCount";
                    Feed<Video> videoFeed = request.Get<Video>(new Uri(feedUrl));
                    foreach (Video item in videoFeed.Entries)
                    {
                        hypster_tv_DAL.PlaylistData_Song song_add1 = new hypster_tv_DAL.PlaylistData_Song();
                        song_add1.YoutubeId = item.VideoId;
                        song_add1.Title = item.Title;
                        model.songs_list.Add(song_add1);
                    }
                }


                ViewBag.UserID = 0;
                ViewBag.PlaylistID = 0;
            }
            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------
        //---
        //[OutputCache(Duration = 10)]
        public ActionResult featured(string id)
        {
            //1.
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            if (Request.QueryString["ba"] != null)
            {
                ViewBag.BA = Request.QueryString["ba"].ToString();
            }



            //1.1
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.FeaturedPlaylistManagement featuredManager = new hypster_tv_DAL.FeaturedPlaylistManagement();

            hypster_tv_DAL.FeaturedPlaylist_Result feat_playlist = new hypster_tv_DAL.FeaturedPlaylist_Result();
            feat_playlist = featuredManager.FeaturedPlaylistByGuid(id);
            //--------------------------------------------------------------------------------------------





            //2.
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            string plst_id_str = feat_playlist.FeaturedPlaylist_PlaylistID.ToString();


            //detect curr member
            //
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByID((int)feat_playlist.FeaturedPlaylist_UserID);
            ViewBag.UserID = member.id;
            ViewBag.Username = member.username;
            //--------------------------------------------------------------------------------------------





            //3.
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            int PLAYLIST_ID = 0;

            PLAYLIST_ID = (int)feat_playlist.FeaturedPlaylist_PlaylistID;



            if (ViewBag.UserID != 0 && PLAYLIST_ID != 0)
            {
                playlistManager.AddPlaylistView(PLAYLIST_ID);
                curr_playlist = playlistManager.GetUserPlaylistById(Int32.Parse(ViewBag.UserID.ToString()), PLAYLIST_ID);
                model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
            }

            //set Page Title
            ViewBag.PageTitle = feat_playlist.FeaturedPlaylist_PlaylistName + " - music playlist";
            ViewBag.Desc = "Playlist with music of " + curr_playlist.description + "...";
            ViewBag.DescLength = curr_playlist.description.Length;
            ViewBag.TagsList = curr_playlist.description.Split(',');


            ViewBag.PlaylistID = PLAYLIST_ID;
            ViewBag.PlaylistName = feat_playlist.FeaturedPlaylist_PlaylistName;
            ViewBag.PlaylistLikes = curr_playlist.Likes;
            ViewBag.PlaylistViews = curr_playlist.ViewsNum;

            //--------------------------------------------------------------------------------------------





            return View("MPL", model);
        }
        //----------------------------------------------------------------------------------------------------





    }
}
