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



namespace hypster.Controllers
{
    public class hypsterPlayerController : ControllerBase
    {




        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        public ActionResult Index()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------




            
            // 2.get requested media type (playlist, single song, default palylist)
            //--------------------------------------------------------------------------------------------
            string MEDIA_TYPE = "";
            if (Request.QueryString["media_type"] != null)
            {
                MEDIA_TYPE = Request.QueryString["media_type"];
                ViewBag.MEDIA_TYPE = MEDIA_TYPE;
            }
            else
            {
                ViewBag.MEDIA_TYPE = "";
            }
            //--------------------------------------------------------------------------------------------




            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            if (Request.QueryString["us_id"] != null)
            {
                ViewBag.UserID = Request.QueryString["us_id"];
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    member = memberManager.getMemberByUserName(User.Identity.Name);
                    ViewBag.UserID = member.id;
                }
            }





            // 3.parse media type
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            switch (MEDIA_TYPE)
            {

                    //
                    //display requested playlist
                case "playlist":
                    int PLAYLIST_ID = 0;
                    if (Request.QueryString["playlist_id"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out PLAYLIST_ID) == false)
                            PLAYLIST_ID = 0;
                    }

                    playlistManager.AddPlaylistView(PLAYLIST_ID);
                    ViewBag.PlaylistID = PLAYLIST_ID;
                    break;

                    //
                    //display single song
                case "":

                    ViewBag.MEDIA_TYPE = "song";


                    string SongGuid = "";
                    if (Request.QueryString["song_guid"] != null)
                    {
                        SongGuid = Request.QueryString["song_guid"];
                        ViewBag.SongGuid = SongGuid.Replace("&", "amp;");
                    }


                    string SongTitle = "";
                    if (Request.QueryString["song_title"] != null)
                    {
                        SongTitle = Request.QueryString["song_title"];
                        ViewBag.SongTitle = SongTitle.Replace("&", "amp;");
                    }



                    hypster_tv_DAL.PlaylistData_Song song = new hypster_tv_DAL.PlaylistData_Song();
                    song.YoutubeId = SongGuid;
                    song.Title = SongTitle;

                    model.songs_list.Add(song);


                    


                    if (model.songs_list.Count > 0)
                        ViewBag.SongGuid = model.songs_list[0].YoutubeId;
                    break;



                    //
                    //display default palylist
                case "DEFPL":
                    ViewBag.MEDIA_TYPE = "playlist";
                    
                    
                    playlistManager.AddPlaylistView(member.active_playlist);
                    ViewBag.PlaylistID = member.active_playlist;
                    ViewBag.UserID = member.id;
                    break;



                //
                //display default palylist
                case "Radio":
                    ViewBag.MEDIA_TYPE = "radio";


                    string Genre_str = "";
                    if (Request.QueryString["Genre"] != null)
                    {
                        Genre_str = Request.QueryString["Genre"];
                        Genre_str = Genre_str.Replace("&", "amp;");
                        ViewBag.Genre = Genre_str;
                    }

                    string search_str = "";
                    if (Request.QueryString["search"] != null)
                    {
                        search_str = Request.QueryString["search"];
                        ViewBag.search = search_str;
                    }


                    break;



                default:
                    break;

            }
            //--------------------------------------------------------------------------------------------



            





            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        private ActionResult PreparePlayer()
        {
            
            
            //--------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "");
                return RedirectPermanent(new_url);
            }
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------





            // 2.get requested media type (playlist, single song, default palylist)
            //--------------------------------------------------------------------------------------------
            string MEDIA_TYPE = "";
            if (Request.QueryString["media_type"] != null)
            {
                MEDIA_TYPE = Request.QueryString["media_type"];
                ViewBag.MEDIA_TYPE = MEDIA_TYPE;
            }
            else
            {
                ViewBag.MEDIA_TYPE = "";
            }
            //--------------------------------------------------------------------------------------------


            // 3.autoplay
            //--------------------------------------------------------------------------------------------
            string AUTO_PLAY = "";
            if (Request.QueryString["apl"] != null)
            {
                AUTO_PLAY = Request.QueryString["apl"];
                ViewBag.AUTO_PLAY = AUTO_PLAY;
            }
            else
            {
                ViewBag.AUTO_PLAY = "";
            }
            //--------------------------------------------------------------------------------------------







            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            if (Request.QueryString["us_id"] != null)
            {

                int us_id = 0;
                if (Int32.TryParse(Request.QueryString["us_id"], out us_id) == true)
                {
                    if (us_id > 0)
                    {
                        member = memberManager.getMemberByID(us_id);
                    }
                }
                else
                {
                    us_id = 0;
                }

                ViewBag.UserID = us_id;
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    member = memberManager.getMemberByUserName(User.Identity.Name);
                    ViewBag.UserID = member.id;
                }
            }
            //--------------------------------------------------------------------------------------------



            ViewBag.PageTitle = "Hypster's online music playlist | Listen and love";


            // 3.parse media type
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            switch (MEDIA_TYPE)
            {

                //************************************************************************************************************************
                //display requested playlist
                case "playlist":
                    hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
                    int PLAYLIST_ID = 0;
                    if (Request.QueryString["playlist_id"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out PLAYLIST_ID) == false)
                            PLAYLIST_ID = 0;
                    }


                    if (ViewBag.UserID != null && PLAYLIST_ID != null)
                    {
                        playlistManager.AddPlaylistView(PLAYLIST_ID);
                        curr_playlist = playlistManager.GetUserPlaylistById(Int32.Parse(ViewBag.UserID.ToString()), PLAYLIST_ID);
                        model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
                    }

                    //set Page Title
                    ViewBag.PageTitle = curr_playlist.name + " | Hypster's online music playlist";


                    ViewBag.PlaylistID = PLAYLIST_ID;
                    ViewBag.PlaylistLikes = curr_playlist.Likes;
                    ViewBag.PlaylistViews = curr_playlist.ViewsNum;


                    // temp tracking code for BRNA playlist
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



                    //-----------------------------------------------------------------------------------------------------
                    // rev tracking
                    try
                    {
                        if (Request.QueryString["TMRC"] != null && HttpContext.Request.Cookies["AD_TMRC"] == null)
                        {
                            if (System.Configuration.ConfigurationManager.AppSettings["TMRC_" + Request.QueryString["TMRC"].ToString()] != null)
                            {
                                HttpCookie myCookie = new HttpCookie("AD_TMRC");
                                myCookie.Value = System.Configuration.ConfigurationManager.AppSettings["TMRC_" + Request.QueryString["TMRC"].ToString()].ToString();
                                myCookie.Expires = DateTime.Now.AddDays(30);
                                HttpContext.Response.Cookies.Add(myCookie);
                            }
                        }
                    }
                    finally { }
                    //--------------------------------------------------------------------------------------------



                    break;

                //************************************************************************************************************************
                //display single song
                case "":

                    ViewBag.MEDIA_TYPE = "song";


                    string SongGuid = "";
                    if (Request.QueryString["song_guid"] != null)
                    {
                        SongGuid = Request.QueryString["song_guid"];
                        ViewBag.SongGuid = SongGuid.Replace("&", "amp;");
                    }


                    string SongTitle = "";
                    if (Request.QueryString["song_title"] != null)
                    {
                        SongTitle = Request.QueryString["song_title"];
                        ViewBag.SongTitle = SongTitle.Replace("&", "amp;");
                    }

                    //set Page Title
                    ViewBag.PageTitle = SongTitle + " | Hypster's online music playlist";


                    hypster_tv_DAL.PlaylistData_Song song = new hypster_tv_DAL.PlaylistData_Song();
                    song.YoutubeId = SongGuid;
                    song.Title = SongTitle;

                    model.songs_list.Add(song);



                    if (model.songs_list.Count > 0)
                        ViewBag.SongGuid = model.songs_list[0].YoutubeId;
                    break;



                //************************************************************************************************************************
                //display default palylist
                //
                // REDIRECTED
                //
                case "DEFPL":


                    ViewBag.MEDIA_TYPE = "playlist";
                    int PLAYLIST_ID_DEF = member.active_playlist;

                    playlistManager.AddPlaylistView(member.active_playlist);

                    if (ViewBag.UserID != null && PLAYLIST_ID_DEF != null)
                    {
                        model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID_DEF);
                    }


                    ViewBag.PlaylistID = member.active_playlist;
                    ViewBag.UserID = member.id;


                    hypster_tv_DAL.Playlist curr_playlist_deff = new hypster_tv_DAL.Playlist();
                    curr_playlist_deff = playlistManager.GetUserPlaylistById(member.id, member.active_playlist);
                    ViewBag.PlaylistLikes = curr_playlist_deff.Likes;
                    ViewBag.PlaylistViews = curr_playlist_deff.ViewsNum;


                    //redirects playlist to permanent url for sharing
                    return RedirectPermanent("/hypsterPlayer/MPL?media_type=playlist&playlist_id=" + member.active_playlist + "&us_id=" + member.id + "&rtg=DEFPL");

                    break;



                //************************************************************************************************************************
                //display default palylist
                case "Radio":
                    ViewBag.MEDIA_TYPE = "radio";


                    string Genre_str = "";
                    if (Request.QueryString["Genre"] != null)
                    {
                        Genre_str = Request.QueryString["Genre"];
                        ViewBag.Genre = Genre_str;
                    }


                    //set Page Title
                    ViewBag.PageTitle = Genre_str + " Playlist Station | Hypster's online music playlist";




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
                            playlistManager.AddPlaylistView(member.active_playlist);
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
                    //************************************************************************************************************************
                    break;


                case "Search":
                    ViewBag.MEDIA_TYPE = "radio";


                    string Genre_str1 = "";
                    if (Request.QueryString["Genre"] != null)
                    {
                        Genre_str1 = Request.QueryString["Genre"];
                        ViewBag.Genre = Genre_str1;
                    }


                    //set Page Title
                    ViewBag.PageTitle = Genre_str1 + " | Hypster's online music playlist";

                    //************************************************************************************************************************


                    //************************************************************************************************************************

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
                    if (Request.QueryString["pageS"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["pageS"], out this_page1) == false)
                            this_page1 = 1;
                    }

                    //string feedUrl = "http://gdata.youtube.com/feeds/api/videos?q=" + Genre_str + "&category=Music&format=5&restriction=" + Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + ((this_page * 25) - 24) + "&orderby=viewCount";

                    //string feedUrl1 = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}" + orderBy, HttpUtility.UrlEncode(Genre_str1.Replace("+", " ")), Request.ServerVariables["REMOTE_ADDR"], (this_page1 * 25) - 25 + 1);

                    string feedUrl1 = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&start-index={1}" + orderBy, HttpUtility.UrlEncode(Genre_str1.Replace("+", " ")), (this_page1 * 25) - 25 + 1);
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

                    //************************************************************************************************************************
                    break;



                default:
                    break;

            }
            //--------------------------------------------------------------------------------------------




            





            //--------------------------------------------------------------------------------------------
            if (member.id != 0)
            {
                model.userPlaylists_list = playlistManager.GetUserPlaylists(member.id);
            }
            //--------------------------------------------------------------------------------------------





            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        public ActionResult MPL()
        {
            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        public ActionResult MPL_D()
        {
            if (Request.QueryString["us_id"] != null)
            {
                int us_id = 0;
                if (Int32.TryParse(Request.QueryString["us_id"], out us_id) == true)
                {
                    if (us_id == 0)
                    {
                        return RedirectPermanent("/hypDesktop/dPopular");
                    }
                }
                else
                {
                    return RedirectPermanent("/hypDesktop/dPopular");
                }
            }


            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player news
        // 
        public ActionResult MPL_News()
        {
            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // music player news
        // 
        public ActionResult WHATTHEHELLZ()
        {
            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // player popup - horizontal 
        public ActionResult vplayer()
        {
            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // player popup - horizontal 
        public ActionResult hplayer()
        {
            return PreparePlayer();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






    }
}
