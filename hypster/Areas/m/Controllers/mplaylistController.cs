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


namespace hypster.Areas.m.Controllers
{
    public class mplaylistController : Controller
    {
    
        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public ActionResult AddToPlayList()
        {

            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return Content("<script type='text/javascript'>window.location='/m/maccount/SignIn';</script>");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.AddToPlayListViewModel model = new ViewModels.AddToPlayListViewModel();

            //get curr user
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = userManager.getMemberByUserName(User.Identity.Name);


            if (Request.QueryString["song_title"] != null)
                ViewBag.song_title = Request.QueryString["song_title"];

            if (Request.QueryString["song_guid"] != null)
                ViewBag.song_guid = Request.QueryString["song_guid"];

            if (Request.QueryString["ss"] != null)
                ViewBag.ss = Request.QueryString["ss"];


            int PLAYLIST_ID = 0;
            PLAYLIST_ID = curr_user.active_playlist;

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.songs_list = playlistManager.GetSongsForPlayList(curr_user.id, PLAYLIST_ID);

            model.playlists_list = playlistManager.GetUserPlaylists(curr_user.id);

            model.curr_user = curr_user;
            //--------------------------------------------------------------------------------------------




            // NEED TO CHECK HERE
            //--------------------------------------------------------------------------------------------
            foreach (var item in model.playlists_list)
            {
                if (PLAYLIST_ID == item.id)
                {
                    ViewBag.Playlist_ID = item.id;
                    ViewBag.Playlist_Name = item.name;
                }
            }
            //--------------------------------------------------------------------------------------------





            //check if song already exist in playlist
            //--------------------------------------------------------------------------------------------
            foreach (var item in model.songs_list)
            {
                if (item.YoutubeId == ViewBag.song_guid)
                {
                    ViewBag.SongAlreadyExist = "Y";
                }
            }
            //--------------------------------------------------------------------------------------------




            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public ActionResult AddToPlayListPopup()
        {
            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return Content("");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.AddToPlayListViewModel model = new ViewModels.AddToPlayListViewModel();

            //get curr user
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = userManager.getMemberByUserName(User.Identity.Name);


            if (Request.QueryString["song_title"] != null)
                ViewBag.song_title = Request.QueryString["song_title"];

            if (Request.QueryString["song_guid"] != null)
                ViewBag.song_guid = Request.QueryString["song_guid"];


            Random rand = new Random();
            ViewBag.cb_hf = rand.Next(1000, 100000);


            int PLAYLIST_ID = 0;
            PLAYLIST_ID = curr_user.active_playlist;

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.songs_list = playlistManager.GetSongsForPlayList(curr_user.id, PLAYLIST_ID);

            model.playlists_list = playlistManager.GetUserPlaylists(curr_user.id);

            model.curr_user = curr_user;
            //--------------------------------------------------------------------------------------------




            // NEED TO CHECK HERE
            //--------------------------------------------------------------------------------------------
            foreach (var item in model.playlists_list)
            {
                if (PLAYLIST_ID == item.id)
                {
                    ViewBag.Playlist_ID = item.id;
                    ViewBag.Playlist_Name = item.name;
                }
            }
            //--------------------------------------------------------------------------------------------




            //check if song already exist in playlist
            //--------------------------------------------------------------------------------------------
            foreach (var item in model.songs_list)
            {
                if (item.YoutubeId == ViewBag.song_guid)
                {
                    ViewBag.SongAlreadyExist = "Y";
                }
            }
            //--------------------------------------------------------------------------------------------



            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        //authentitication not required
        public ActionResult SubmitFeedbackPopup()
        {

            return View();
        }
        //----------------------------------------------------------------------------------------------------------













        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        public ActionResult SubmitAddNewSong(string Song_Title, string Song_Guid, int? Sel_Playlist_ID, int? StayHereCheck, string ss)
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Hypster_Entities hypDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManagement = new hypster_tv_DAL.playlistManagement();
            //--------------------------------------------------------------------------------------------



            //get curr user
            //-----------------------------------------------------------------------------------------
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = userManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------




            //check if user has default playlist
            //-----------------------------------------------------------------------------------------
            if (curr_user.active_playlist == 0)
            {
                hypster_tv_DAL.Playlist create_playlist = new hypster_tv_DAL.Playlist();
                create_playlist.name = curr_user.username + "'s playlist";
                create_playlist.userid = curr_user.id;


                string crtd = DateTime.Now.ToString("yyyyMMdd");
                int crtd_i = 0;
                Int32.TryParse(crtd, out crtd_i);
                create_playlist.create_time = crtd_i;


                if (create_playlist.name.Length > 60)
                    create_playlist.name = create_playlist.name.Substring(0, 60);


                hypDB.Playlists.AddObject(create_playlist);
                hypDB.SaveChanges();

                List<hypster_tv_DAL.Playlist> playlists_list = playlistManagement.GetUserPlaylists(curr_user.id);
                if (playlists_list.Count > 0)
                {
                    curr_user.active_playlist = playlists_list[0].id;
                    userManager.SetUserDefaultPlaylist(User.Identity.Name, curr_user.id, curr_user.active_playlist);
                }
                //else error - need to have dafult playlist
            }
            //-----------------------------------------------------------------------------------------




            //check if user selected playlist
            //-----------------------------------------------------------------------------------------
            if (Sel_Playlist_ID == null || Sel_Playlist_ID == 0)
            {
                Sel_Playlist_ID = curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------





            // get last sort_number
            //-----------------------------------------------------------------------------------------
            short Sel_Sort_Order = 0;
            //List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
            //songs_list = playlistManagement.GetSongsForPlayList(curr_user.id, (int)Sel_Playlist_ID);
            //if (songs_list.Count > 0)
            //{
            //    Sel_Sort_Order = songs_list[0].sortid;
            //    for (int i = 0; i < songs_list.Count; i++)
            //    {
            //        if (songs_list[i].sortid > Sel_Sort_Order)
            //            Sel_Sort_Order = songs_list[i].sortid;
            //    }
            //}
            //Sel_Sort_Order += 1;

            playlistManagement.IncrementPlaylistSongOrder(curr_user.id, (int)Sel_Playlist_ID);
            //set sort order to first position
            Sel_Sort_Order = 1;
            //-----------------------------------------------------------------------------------------




            //get song by guid
            //-----------------------------------------------------------------------------------------
            hypster_tv_DAL.Song song = new hypster_tv_DAL.Song();
            song = songsManager.GetSongByGUID(Song_Guid);
            //-----------------------------------------------------------------------------------------


            if (Song_Title.Length > 160)
            {
                Song_Title = Song_Title.Substring(0, 160);
            }


            //-----------------------------------------------------------------------------------------
            if (song.id == 0) //add new song
            {

                //need to get song details
                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);
                string feedUrl = "http://gdata.youtube.com/feeds/api/videos/" + Song_Guid;
                Video video = request.Retrieve<Video>(new Uri(feedUrl));


                //need to modify to add more song params
                hypster_tv_DAL.Song new_song = new hypster_tv_DAL.Song();
                new_song.Title = Song_Title;
                new_song.YoutubeId = Song_Guid;
                new_song.adddate = DateTime.Now;
                new_song.YoutubeProcessed = false;

                if(video.Author != null)
                    new_song.Author = video.Uploader;
                if(video.RatingAverage != null)
                    new_song.Rating = (float)video.RatingAverage;
                if (video.AppControl != null)
                    new_song.Syndication = 1;

                hypDB.Songs.AddObject(new_song);
                hypDB.SaveChanges();





                //get newely added song
                song = songsManager.GetSongByGUID(Song_Guid);


                //add to playlist data
                hypster_tv_DAL.PlaylistData new_playlistData = new hypster_tv_DAL.PlaylistData();
                new_playlistData.playlist_id = Sel_Playlist_ID;
                new_playlistData.songid = song.id;
                new_playlistData.sortid = Sel_Sort_Order;
                new_playlistData.userid = userManager.getMemberByUserName(User.Identity.Name).id;

                hypDB.PlaylistDatas.AddObject(new_playlistData);
                hypDB.SaveChanges();

            }
            else //if song exist in database
            {
                //add to playlist data
                hypster_tv_DAL.PlaylistData new_playlistData = new hypster_tv_DAL.PlaylistData();
                new_playlistData.playlist_id = Sel_Playlist_ID;
                new_playlistData.songid = song.id;
                new_playlistData.sortid = Sel_Sort_Order;
                new_playlistData.userid = curr_user.id;


                hypDB.PlaylistDatas.AddObject(new_playlistData);
                hypDB.SaveChanges();
            }
            //-----------------------------------------------------------------------------------------





            //-----------------------------------------------------------------------------------------
            // check first if user not selected to watch related videos
            if (StayHereCheck != null && StayHereCheck == 2)
            {
                //return RedirectPermanent("/relatedVideos?ss=" + ss.Replace(' ', '+'));
                return Content("/relatedVideos?ss=" + ss.Replace(' ', '+'), "text/html");
            }


            if (StayHereCheck != null && StayHereCheck == 1)
            {
                return Content(string.Empty, "text/html");
                /*
                if (ss != null && ss.ToString() != "")
                {
                    //return RedirectPermanent("/listen?ss=" + ss.Replace(' ', '+'));
                    return Content("/listen?ss=" + ss.Replace(' ', '+'), "text/html");
                }
                else
                {
                    //return RedirectPermanent("/listen");
                    return Content("/listen", "text/html");
                }
                */
            }
            else
            {
                //return RedirectPermanent("/create/playlist?playlist_id=" + Sel_Playlist_ID);
                return Content("/m/mcreate/playlist?playlist_id=" + Sel_Playlist_ID, "text/html");
            }
            //-----------------------------------------------------------------------------------------


        }
        //----------------------------------------------------------------------------------------------------------








        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        public ActionResult SubmitAddNewSongPopup(string Song_Title, string Song_Guid, int? Sel_Playlist_ID, int? StayHereCheck, int? cb_hf)
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Hypster_Entities hypDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManagement = new hypster_tv_DAL.playlistManagement();
            //--------------------------------------------------------------------------------------------



            //get curr user
            //-----------------------------------------------------------------------------------------
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = userManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------




            //check if user has default playlist
            //-----------------------------------------------------------------------------------------
            if (curr_user.active_playlist == 0)
            {
                hypster_tv_DAL.Playlist create_playlist = new hypster_tv_DAL.Playlist();
                create_playlist.name = curr_user.username + "'s playlist";
                create_playlist.userid = curr_user.id;


                string crtd = DateTime.Now.ToString("yyyyMMdd");
                int crtd_i = 0;
                Int32.TryParse(crtd, out crtd_i);
                create_playlist.create_time = crtd_i;


                if (create_playlist.name.Length > 60)
                    create_playlist.name = create_playlist.name.Substring(0, 60);


                hypDB.Playlists.AddObject(create_playlist);
                hypDB.SaveChanges();

                List<hypster_tv_DAL.Playlist> playlists_list = playlistManagement.GetUserPlaylists(curr_user.id);
                if (playlists_list.Count > 0)
                {
                    curr_user.active_playlist = playlists_list[0].id;
                    userManager.SetUserDefaultPlaylist(User.Identity.Name, curr_user.id, curr_user.active_playlist);
                }
                //else error - need to have dafult playlist
            }
            //-----------------------------------------------------------------------------------------




            //check if user selected playlist
            //-----------------------------------------------------------------------------------------
            if (Sel_Playlist_ID == null || Sel_Playlist_ID == 0)
            {
                Sel_Playlist_ID = curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------





            // get last sort_number
            //-----------------------------------------------------------------------------------------
            short Sel_Sort_Order = 0;
            playlistManagement.IncrementPlaylistSongOrder(curr_user.id, (int)Sel_Playlist_ID);
            //set sort order to first position
            Sel_Sort_Order = 1;
            //-----------------------------------------------------------------------------------------




            //get song by guid
            //-----------------------------------------------------------------------------------------
            hypster_tv_DAL.Song song = new hypster_tv_DAL.Song();
            song = songsManager.GetSongByGUID(Song_Guid);
            //-----------------------------------------------------------------------------------------




            //-----------------------------------------------------------------------------------------
            if (Song_Title.Length > 160)
            {
                Song_Title = Song_Title.Substring(0, 160);
            }
            //-----------------------------------------------------------------------------------------




            //-----------------------------------------------------------------------------------------
            if (song.id == 0) //add new song
            {
                string video_Author = "";
                float video_RatingAverage = 0;
                byte video_AppControl = 0;
                string video_Title = "";

                try
                {
                    //need to get song details
                    YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                    YouTubeRequest request = new YouTubeRequest(settings);
                    string feedUrl = "http://gdata.youtube.com/feeds/api/videos/" + Song_Guid;
                    Video video = request.Retrieve<Video>(new Uri(feedUrl));


                    if (video.Author != null)
                        video_Author = video.Uploader;
                    if (video.RatingAverage != null)
                        video_RatingAverage = (float)video.RatingAverage;
                    if (video.AppControl != null)
                        video_AppControl = 1;
                    if (video.Title != null)
                        video_Title = video.Title;

                }
                catch(Exception ex)
                {
                }


                //need to modify to add more song params
                hypster_tv_DAL.Song new_song = new hypster_tv_DAL.Song();
                new_song.Title = video_Title;
                new_song.YoutubeId = Song_Guid;
                new_song.adddate = DateTime.Now;
                new_song.YoutubeProcessed = false;

                
                new_song.Author = video_Author;
                new_song.Rating = video_RatingAverage;
                new_song.Syndication = video_AppControl;

                hypDB.Songs.AddObject(new_song);
                hypDB.SaveChanges();





                //get newely added song
                song = songsManager.GetSongByGUID(Song_Guid);


                //add to playlist data
                hypster_tv_DAL.PlaylistData new_playlistData = new hypster_tv_DAL.PlaylistData();
                new_playlistData.playlist_id = Sel_Playlist_ID;
                new_playlistData.songid = song.id;
                new_playlistData.sortid = Sel_Sort_Order;
                new_playlistData.userid = userManager.getMemberByUserName(User.Identity.Name).id;

                hypDB.PlaylistDatas.AddObject(new_playlistData);
                hypDB.SaveChanges();

            }
            else //if song exist in database
            {
                //add to playlist data
                hypster_tv_DAL.PlaylistData new_playlistData = new hypster_tv_DAL.PlaylistData();
                new_playlistData.playlist_id = Sel_Playlist_ID;
                new_playlistData.songid = song.id;
                new_playlistData.sortid = Sel_Sort_Order;
                new_playlistData.userid = curr_user.id;


                hypDB.PlaylistDatas.AddObject(new_playlistData);
                hypDB.SaveChanges();
            }
            //-----------------------------------------------------------------------------------------





            
            return Content("SUCCESS", "text/html");
        }
        //----------------------------------------------------------------------------------------------------------


        

            //----------------------------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        public ActionResult SubmitSendFeedbackPopup(string YourEmail, string Subject, string Message)
        {
            //add logic to send feedback
            hypster_tv_DAL.Hypster_Entities HypDB = new hypster_tv_DAL.Hypster_Entities();


            hypster_tv_DAL.userContact userContact = new hypster_tv_DAL.userContact();
            userContact.contactType = 3;
            userContact.contactEmail = YourEmail;
            userContact.contactSubject = Subject;
            userContact.contactText = Message;
            HypDB.userContacts.AddObject(userContact);
            HypDB.SaveChanges();


            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            emailManager.SendFeedbackEmail(Subject, YourEmail, Message);
    
            

            return Content("SUCCESS", "text/html");
        }
        //----------------------------------------------------------------------------------------------------------

        






        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult AddNewPlaylistLike(int? Playlist_ID, int? Owner_ID)
        {
            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Hypster_Entities hypDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManagement = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playlistLikeManagement playlistLikeManagement = new hypster_tv_DAL.playlistLikeManagement();
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            //get curr user
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = userManager.getMemberByUserName(User.Identity.Name);
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.PlaylistLike new_pl_like = new hypster_tv_DAL.PlaylistLike();
            new_pl_like.PlaylistId = (int)Playlist_ID;
            new_pl_like.OwnerId = (int)Owner_ID;
            new_pl_like.LikeValue = 1;
            new_pl_like.UserId = curr_user.id;
            //--------------------------------------------------------------------------------------------

            string ret_res = playlistLikeManagement.AddNewPlaylistLike(new_pl_like);


            hypster_tv_DAL.Playlist playlist = new hypster_tv_DAL.Playlist();
            playlist = playlistManagement.GetUserPlaylistById((int)Owner_ID, (int)Playlist_ID);


            //-----------------------------------------------------------------------------------------
            return Content(playlist.Likes.ToString(), "text/html");
            //-----------------------------------------------------------------------------------------

        }
        //----------------------------------------------------------------------------------------------------------







        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public string deletePlaylistSong()
        {
            
            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return "not authorized";
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement songManager = new hypster_tv_DAL.playlistManagement();
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            int SONG_ID = 0;
            if (Request.QueryString["SONG_ID"] != null && Int32.TryParse(Request.QueryString["SONG_ID"], out SONG_ID) == true)
            {   
                songManager.DeleteSong(memberManager.getMemberByUserName(User.Identity.Name).id, SONG_ID);
                return "true";
            }
            //--------------------------------------------------------------------------------------------



            return "false";
        }
        //----------------------------------------------------------------------------------------------------------





        
        //----------------------------------------------------------------------------------------------------------
        // apply order to playlist songs
        //
        //authentitication implemented inside (ajax call)
        public string applyOrder()
        {
            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return "not authorized";
            //--------------------------------------------------------------------------------------------



            // 1.parse parameters
            //--------------------------------------------------------------------------------------------
            int SONG_ID = 0;
            int SONG_ORDER = 0;
            int ACTIVE_PL = 0;
            bool params_parsed = true;

            if (Request.QueryString["SONG_ID"] != null && Int32.TryParse(Request.QueryString["SONG_ID"], out SONG_ID) == false) 
                params_parsed = false;

            if (Request.QueryString["SONG_ORDER"] != null && Int32.TryParse(Request.QueryString["SONG_ORDER"], out SONG_ORDER) == false) 
                params_parsed = false;

            if (Request.QueryString["ACTIVE_PL"] != null && Int32.TryParse(Request.QueryString["ACTIVE_PL"], out ACTIVE_PL) == false)
                params_parsed = false;
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            if (params_parsed == true)
            {
                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);


                hypster_tv_DAL.playlistManagement playlistsManagement = new hypster_tv_DAL.playlistManagement();
                List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
                songs_list = playlistsManagement.GetSongsForPlayList(curr_user.id, ACTIVE_PL);




                int UPD_INDEX = -1;
                int i_count = 0;
                hypster_tv_DAL.PlaylistData_Song i_currSong = new hypster_tv_DAL.PlaylistData_Song();



                //2. find position of where new link will be located
                //**************************************************************************************************************************
                i_count = 0;
                foreach (hypster_tv_DAL.PlaylistData_Song item in songs_list)
                {
                    if (item.playlist_track_id == SONG_ID)
                    {
                        int add_ind = 0;
                        if (SONG_ORDER > 0)
                            add_ind = 1;


                        UPD_INDEX = i_count + SONG_ORDER + add_ind;
                        i_currSong = item;

                        break;
                    }

                    i_count++;
                }
                //**************************************************************************************************************************

                

                bool update_order = false;


                //3. get to position of new element and start updating new order
                //**************************************************************************************************************************
                List<hypster_tv_DAL.PlaylistData_Song> upd_links_arr = new List<hypster_tv_DAL.PlaylistData_Song>();
                i_count = 0;
                foreach (hypster_tv_DAL.PlaylistData_Song ii_item in songs_list)
                {

                    //inc next items order
                    if (update_order == true)
                    {
                        if (ii_item.playlist_track_id != SONG_ID)
                        {
                            ii_item.sortid += 1;
                            upd_links_arr.Add(ii_item);
                        }
                    }


                    //find start element
                    if (UPD_INDEX == i_count)
                    {
                        i_currSong.sortid = ii_item.sortid;
                        upd_links_arr.Add(i_currSong);
                        update_order = true;
                        

                        ii_item.sortid += 1;
                        upd_links_arr.Add(ii_item);
                    }


                    i_count++;
                }


                //check for last element position
                if (UPD_INDEX >= songs_list.Count)
                {
                    hypster_tv_DAL.PlaylistData_Song ii_item = i_currSong; //songs_list[0];
                    ii_item.sortid = songs_list[songs_list.Count - 1].sortid;
                    ii_item.sortid += 1;
                    upd_links_arr.Add(ii_item);
                }



                playlistsManagement.Update_CustomOrder(curr_user.id, upd_links_arr);
                //**************************************************************************************************************************

            }
            //--------------------------------------------------------------------------------------------




            return "true";
        }
        //----------------------------------------------------------------------------------------------------------











        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public ActionResult getPlaylistDetails()
        {
            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return RedirectPermanent("/m/maccount/SignIn");
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null && Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id) == true)
            {
                
                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);


                List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
                playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


                foreach (var item in playlists_list)
                {
                    if (playlist_id == item.id) //check if playlist belong to user
                    {
                        ViewBag.Playlist_ID = item.id;
                        ViewBag.Playlist_Name = item.name;


                        hypster.ViewModels.getAccountInfo_PublicViewModel model = new ViewModels.getAccountInfo_PublicViewModel();
                        songs_list = playlistManager.GetSongsForPlayList(curr_user.id, playlist_id);
                    }
                }

            }
            //--------------------------------------------------------------------------------------------



            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public ActionResult getPlaylistDetailsPlayer()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int user_id = 0;
            if (Request.QueryString["US_ID"] != null )
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



            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------


        

        //----------------------------------------------------------------------------------------------------------
        // segment playlist need for large playlists
        // to load rest of playlist
        //
        //authentitication implemented inside (ajax call)
        public ActionResult getPlaylistSegmentPlayer()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------



            int curr_segment = 0;
            if (Request.QueryString["Seg"] != null)
            {
                Int32.TryParse(Request.QueryString["Seg"], out curr_segment);
            }



            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int user_id = 0;
            if (Request.QueryString["US_ID"] != null )
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




            List<hypster_tv_DAL.PlaylistData_Song> model = new List<hypster_tv_DAL.PlaylistData_Song>();

            //--------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null && Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id) == true)
            {
   
                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                songs_list = playlistManager.GetSongsForPlayList(user_id, playlist_id);


                int Song_LIMIT = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Song_LIMIT"]);

                int arr_start = curr_segment * Song_LIMIT + 1;
                int arr_end = (curr_segment * Song_LIMIT) + Song_LIMIT;

                if (arr_start < songs_list.Count)
                {
                    if (arr_end > songs_list.Count)
                    {
                        arr_end = songs_list.Count;
                    }
                    else
                    {
                        ViewBag.LoadMoreBtn = 1;
                    }

                    for (var i = arr_start; i < arr_end; i++)
                    {
                        model.Add(songs_list[i]);
                    }
                }

                ViewBag.Start_Pos = arr_start;
                ViewBag.End_Pos = arr_end;
            }
            //--------------------------------------------------------------------------------------------



            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        // preview method
        //
        // cache applied
        //authentitication implemented inside (ajax call)
        [OutputCache(Duration = 100)]
        public ActionResult getPlaylistDetailsPreview()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------


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



            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        // preview method
        //
        // cache applied
        //authentitication implemented inside (ajax call)
        [OutputCache(Duration = 100)]
        public ActionResult getPlaylistDetailsPreviewPop()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------


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


            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        // preview method
        //
        // cache applied
        //authentitication implemented inside (ajax call)
        [OutputCache(Duration = 100)]
        public ActionResult getPlaylistDetailsPreviewPopDG()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------


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


            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        // preview method radio
        //
        // cache applied
        //authentitication implemented inside (ajax call)
        [OutputCache(Duration = 100)]
        public ActionResult getPlaylistDetailsPreviewRadio()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------


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



            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        // preview for popular playlists
        //
        // cache applied
        //authentitication implemented inside (ajax call)
        [OutputCache(Duration = 100)]
        public ActionResult getPlaylistDetailsPreviewPlst()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------


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



            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------








        //----------------------------------------------------------------------------------------------------------
        //authentitication implemented inside (ajax call)
        public ActionResult getPlaylistDetailsEdt()
        {

            //--------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated == false)
                return RedirectPermanent("/m/maccount/SignIn");
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null && Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id) == true)
            {
                ViewBag.Playlist = playlist_id;

                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);


                List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
                playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


                foreach (var item in playlists_list)
                {
                    if (playlist_id == item.id) //check if playlist belong to user
                    {
                        hypster.ViewModels.getAccountInfo_PublicViewModel model = new ViewModels.getAccountInfo_PublicViewModel();
                        songs_list = playlistManager.GetSongsForPlayList(curr_user.id, playlist_id);
                    }
                }

            }
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            if (Request.QueryString["PL_TYPE"] != null)
            {
                ViewBag.PlayerType = Request.QueryString["PL_TYPE"];
            }
            //--------------------------------------------------------------------------------------------


            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        public JsonpResult exPl_Playlist_Npl_1()
        {
            //--------------------------------------------------------------------------------------------
            // CAN BE ANONYMOUS
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();

            int user_id = 0;
            if (Request.QueryString["US_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["US_ID"], out user_id);
            }
            else //need to verify this logic (since mainly using for external players for anonymous access)
            {
                if (User.Identity.IsAuthenticated == true)
                {
                    hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                    hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                    curr_user = memberManager.getMemberByUserName(User.Identity.Name);
                    user_id = curr_user.id;
                }
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






            string songs_str = "";
            //--------------------------------------------------------------------------------------------
            foreach (hypster_tv_DAL.PlaylistData_Song item in songs_list)
            {
                songs_str = songs_str + item.YoutubeId + ",";
            }
            //--------------------------------------------------------------------------------------------


           

            return this.Jsonp(new { songs = songs_str });
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult CleanDeadLinks()
        {
            
            //--------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.sp_Playlist_GetSongsDeadLinks_Result> songs_list = new List<hypster_tv_DAL.sp_Playlist_GetSongsDeadLinks_Result>();



            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null && Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id) == true)
            {
                ViewBag.Playlist = playlist_id;
                ViewBag.ActivePlaylistID = playlist_id;


                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);


                List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
                playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


                foreach (var item in playlists_list)
                {
                    if (playlist_id == item.id) //check if playlist belong to user
                    {
                        ViewBag.PlaylistName = item.name;
                        songs_list = playlistManager.GetSongsForPlayListDeadLinks(curr_user.id, playlist_id);
                        // need to get dead links based on member playlist
                    }
                }

            }
            //--------------------------------------------------------------------------------------------


            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        [HttpPost]
        public ActionResult CleanDeadLinksSubmit(int PlaylistID)
        {
            List<hypster_tv_DAL.sp_Playlist_GetSongsDeadLinks_Result> songs_list = new List<hypster_tv_DAL.sp_Playlist_GetSongsDeadLinks_Result>();

            int playlist_id = PlaylistID;
            if (playlist_id != 0)
            {
                ViewBag.Playlist = playlist_id;
                ViewBag.ActivePlaylistID = playlist_id;


                hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                

                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(User.Identity.Name);


                songs_list = playlistManager.GetSongsForPlayListDeadLinks(curr_user.id, playlist_id);

                foreach (var item in songs_list)
                {
                    if (item.Dead_Link_ID != null)
                    {
                        playlistManager.DeleteSong(curr_user.id, item.playlist_track_id);
                    }
                }

            }


            return RedirectPermanent("/m/mcreate/playlist?playlist_id=" + PlaylistID);
        }
        //----------------------------------------------------------------------------------------------------------





        public string chSSres()
        {

            //--------------------------------------------------------------------------------------------
            int user_id = 0;
            if (Request.QueryString["US_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["US_ID"], out user_id);
            }
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["PL_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["PL_ID"], out playlist_id);
            }
            //--------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------
            int shares_num = 0;
            if (Request.QueryString["SN"] != null)
            {
                Int32.TryParse(Request.QueryString["SN"], out shares_num);
            }
            //--------------------------------------------------------------------------------------------





            hypster_tv_DAL.playlistManagement pl_manager = new hypster_tv_DAL.playlistManagement();
            if (shares_num > 0)
            {
                pl_manager.UpdatePlaylistShares(playlist_id, user_id, shares_num);
            }



            return shares_num.ToString();
        }


    }
}
