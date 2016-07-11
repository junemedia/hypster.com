using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{


    public class compatibilityCheckController : ControllerBase
    {
        //
        //------------------------------------------------------------------
        private static int NUM_OF_SEARCHES_LIMIT = 23000;
        //------------------------------------------------------------------


        

        public ActionResult Index()
        {
            return View();
        }

        
        
        public ActionResult cComp_st1()
        {
            return View();
        }






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //cache applied to parent
        //
        //initial left side widget
        public ActionResult cComp_GetUserPlaylists()
        {
            int REQ_PLST = 0;
            int REQ_USER = 0;



            // if user not logged in need to preload default playlists
            if (User.Identity.IsAuthenticated == false)
            {
                REQ_PLST = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Comp_Tool_Plst"]);
                REQ_USER = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Comp_Tool_User"]);
            }


            //if playlists redefined 
            if (Request.QueryString["PL_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["PL_ID"], out REQ_PLST);
            }

            if (Request.QueryString["US_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["US_ID"], out REQ_USER);
            }



            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            if (REQ_USER > 0)
            {
                curr_user = memberManager.getMemberByID(REQ_USER);
                ViewBag.US_ID = curr_user.id;
                ViewBag.ActivePlaylist = REQ_PLST;
            }
            else
            {
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);
                ViewBag.ActivePlaylist = curr_user.active_playlist;
            }



            hypster.ViewModels.Compatibility_UserPlaylists_ViewModel model = new ViewModels.Compatibility_UserPlaylists_ViewModel();
            model.playlists_list = playlistManager.GetUserPlaylists(curr_user.id);

            

            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //when playlists changed ajax request for playlist data
        public string cComp_GetPlaylistData()
        {
            string ret_str = "";


            /*if (User.Identity.IsAuthenticated == false){return "";}*/


            int REQ_PLST = 0;
            if (Request.QueryString["PL_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["PL_ID"], out REQ_PLST);
            }

            int REQ_USER = 0;
            if (Request.QueryString["US_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["US_ID"], out REQ_USER);
            }


            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            if (REQ_USER > 0)
            {
                curr_user = memberManager.getMemberByID(REQ_USER);
            }
            else
            {
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            }

            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
            songs_list = playlistManager.GetSongsForPlayList(curr_user.id, REQ_PLST);




            foreach (var item in songs_list)
            {
                string song_str = "";
                song_str += "<div class=\"plst_song_itm\" onclick=\"LoadStep2_SongSelected(" + item.id + ", '" + item.YoutubeId + "')\" >";
                song_str += "<img alt=\"\" src=\"http://i.ytimg.com/vi/" + item.YoutubeId + "/0.jpg\" />&nbsp;<span id='SongCompTitle_" + item.id + "'>" + item.Title + "</span>";
                song_str += "</div>";


                ret_str += song_str;
            }


            return ret_str;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string getCompatiblePlaylists()
        {
            string RET_DATA_STR = "";

            /*if (User.Identity.IsAuthenticated == false){return "";}*/

            hypster_tv_DAL.CompatibilityManager compManager = new hypster_tv_DAL.CompatibilityManager();

            string [] song_id_str = null;
            if (Request.QueryString["SongID"] != null)
            {
                song_id_str = Request.QueryString["SongID"].Split('|');
            }


            List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result> data = new List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result>();
            int song_id1 = 0;
            int song_id2 = 0;
            int song_id3 = 0;
            int song_id4 = 0;
            int song_id5 = 0;
            

            //if one song selected
            if (song_id_str.Length == 2)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                data = compManager.CompCheck_1(song_id1);
            }

            //if two songs selected
            if (song_id_str.Length == 3)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                Int32.TryParse(song_id_str[1], out song_id2);
                data = compManager.CompCheck_2(song_id1, song_id2);
            }

            //if three songs selected
            if (song_id_str.Length == 4)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                Int32.TryParse(song_id_str[1], out song_id2);
                Int32.TryParse(song_id_str[2], out song_id3);
                data = compManager.CompCheck_3(song_id1, song_id2, song_id3);
            }


            //if three songs selected
            if (song_id_str.Length == 5)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                Int32.TryParse(song_id_str[1], out song_id2);
                Int32.TryParse(song_id_str[2], out song_id3);
                Int32.TryParse(song_id_str[3], out song_id4);
                data = compManager.CompCheck_4(song_id1, song_id2, song_id3, song_id4);
            }


            //if three songs selected
            if (song_id_str.Length == 6)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                Int32.TryParse(song_id_str[1], out song_id2);
                Int32.TryParse(song_id_str[2], out song_id3);
                Int32.TryParse(song_id_str[3], out song_id4);
                Int32.TryParse(song_id_str[4], out song_id5);
                data = compManager.CompCheck_5(song_id1, song_id2, song_id3, song_id4, song_id5);
            }

            //---------------------------------------------------------------------------------------------------------------






            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);



            //---------------------------------------------------------------------------------------------------------------
            int limiter = 200;
            if (data.Count < limiter)
                limiter = data.Count;
            for (var i = 0; i < limiter; i++)
            {
                if (curr_user.id != data[i].userid && data[i].playlist_id != null)
                {
                    RET_DATA_STR += "<div onclick=\"OpenPlayer('media_type=playlist&playlist_id=" + data[i].playlist_id + "&us_id=" + data[i].userid + "&rtg=ct1001');\" class='FndPlsItm'>&nbsp;Playlist " + (i + 1) + " -- " + data[i].SongsMatching + " matches</div>";
                }
            }


            //
            //if not found songs
            if (data.Count == 0 || RET_DATA_STR == "")
            {
                RET_DATA_STR += "<div class='FndPlsItm'>&nbsp;Not Found</div>";
            }
            //---------------------------------------------------------------------------------------------------------------




            return RET_DATA_STR;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 10)]
        public ActionResult getCompatiblePlaylistsMPL()
        {
            
            hypster_tv_DAL.CompatibilityManager compManager = new hypster_tv_DAL.CompatibilityManager();

            string[] song_id_str = null;
            if (Request.QueryString["SongID"] != null)
            {
                song_id_str = Request.QueryString["SongID"].Split('|');
            }


            string song_artist = "";
            if (Request.QueryString["song_artist"] != null)
            {
                song_artist = Request.QueryString["song_artist"].ToString();
                ViewBag.song_artist = song_artist;
            }

            string playlist_id = "";
            if (Request.QueryString["Playlist_ID"] != null)
            {
                playlist_id = Request.QueryString["Playlist_ID"].ToString();
                ViewBag.playlist_id = playlist_id;
            }
            



            List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex> data = new List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex>();
            int song_id1 = 0;
            
            //if one song selected
            if (song_id_str.Length == 2)
            {
                Int32.TryParse(song_id_str[0], out song_id1);
                data = compManager.CompCheck_1_Ex(song_id1);
            }
            //---------------------------------------------------------------------------------------------------------------





            //pending removal
            //
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            
            ViewBag.CURR_USER_ID = curr_user.id;




            return View("getCompatiblePlaylistsMPL", data);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 10)]
        public ActionResult getCompatiblePlaylistsMPLSong()
        {
            
            /*if (User.Identity.IsAuthenticated == false){ return "";}*/



            hypster_tv_DAL.CompatibilityManager compManager = new hypster_tv_DAL.CompatibilityManager();

            string song_guid_str = null;
            if (Request.QueryString["Song_GUID"] != null)
            {
                song_guid_str = Request.QueryString["Song_GUID"];
            }



            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();

            hypster_tv_DAL.Song curr_song = new hypster_tv_DAL.Song();
            curr_song = songManager.GetSongByGUID(song_guid_str);


            List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex> data = new List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex>();
            

            //if one song selected
            if (curr_song != null && curr_song.id != 0)
            {
                data = compManager.CompCheck_1_Ex(curr_song.id);
            }
            //---------------------------------------------------------------------------------------------------------------




            return View("getCompatiblePlaylistsMPL", data);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult getCompatibleTagsPlaylists(string id)
        {
            string tag_name = id;
            ViewBag.Search_Tag = tag_name;



            hypster_tv_DAL.TagManagement tags_manager = new hypster_tv_DAL.TagManagement();

            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>(); 
            hypster_tv_DAL.Tag tag = new hypster_tv_DAL.Tag();


            if (tag_name != null && tag_name != "")
            {
                //get tag by name
                tag = tags_manager.GetTagByName(tag_name);


                //get playlists by term
                if (tag.Tag_ID != 0)
                {
                    playlists_list = tags_manager.GetPlaylistsByTagId(tag.Tag_ID);

                    //increment popular tag
                    tags_manager.IncrementPopularTag(tag.Tag_ID);
                }

            }

            return View(playlists_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult getCompatiblePlaylistDetailsMPL()
        {

            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int user_id = 0;
            if (Request.QueryString["US_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["US_ID"], out user_id);
            }
            else
            {
                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);
                user_id = curr_user.id;
            }
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null && Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id) == true)
            {

                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                hypster.ViewModels.getAccountInfo_PublicViewModel model = new ViewModels.getAccountInfo_PublicViewModel();


                if (Request.QueryString["Sort"] != null && Request.QueryString["Sort"] != "")
                {
                    songs_list = playlistManager.GetSongsForPlayList_Random(user_id, playlist_id);
                }
                else
                {
                    songs_list = playlistManager.GetSongsForPlayList(user_id, playlist_id);
                }
            }
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            string username = "";
            if (Request.QueryString["username"] != null)
            {
                username = Request.QueryString["username"].ToString();
            }

            ViewBag.username = username;
            ViewBag.playlist_id = playlist_id;




            return View(songs_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string check_art_title()
        {

            //----------------------------------------------------------------------------------------
            string playlist_id_str = "";
            int playlist_id = 0;
            if (Request.QueryString["Playlist_ID"] != null)
            {
                playlist_id_str = Request.QueryString["Playlist_ID"].ToString();
                Int32.TryParse(playlist_id_str, out playlist_id);
            }

            string curr_user_id_str = "";
            int user_id = 0;
            if (Request.QueryString["curr_user_id"] != null)
            {
                curr_user_id_str = Request.QueryString["curr_user_id"].ToString();
                Int32.TryParse(curr_user_id_str, out user_id);
            }

            string song_artist_str = "";
            if (Request.QueryString["song_artist"] != null)
            {
                song_artist_str = Request.QueryString["song_artist"].ToString().Trim();
            }
            //----------------------------------------------------------------------------------------




            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.artistManagement artistManager = new hypster_tv_DAL.artistManagement();


            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            curr_playlist = playlistManager.GetUserPlaylistById(user_id, playlist_id);




            hypster_tv_DAL.ArtistCategory check_artist = new hypster_tv_DAL.ArtistCategory();
            check_artist = artistManager.GetArtistByName(song_artist_str);
            if (check_artist.Id == 0)
            {
                hypster_tv_DAL.ArtistCategory artist_add = new hypster_tv_DAL.ArtistCategory();
                artist_add.Name = song_artist_str;
                artistManager.hyDB.ArtistCategories.AddObject(artist_add);
                artistManager.hyDB.SaveChanges();
            }




            if ((curr_playlist.description.Length + song_artist_str.Length) < 100)
            {
                if (!curr_playlist.description.Contains(song_artist_str))
                {
                    string artist_add_str = "";
                    if (curr_playlist.description.Length == 0)
                    {
                        artist_add_str = song_artist_str;
                    }
                    else
                    {
                        artist_add_str = song_artist_str + ", ";
                    }
                    playlistManager.UpdatePlaylistDesc(curr_playlist.id, artist_add_str + curr_playlist.description);
                }
            }


            //update curr playlist description
            // check if description is not too long 
            // check if this artist not in description already
            

            return "";
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    }
}
