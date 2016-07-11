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
using System.Web.Security;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class srAccountController : Controller
    {
        //
        // GET: /black/srAccount/

        public PartialViewResult Index()
        {
            return PartialView();
        }


        [AllowAnonymous]
        public PartialViewResult Login()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public string LoginUser()
        {
            string username = "";
            string password = "";


            if (Request.QueryString["username"] != null)
            {
                username = Request.QueryString["username"].ToString();
            }

            if (Request.QueryString["password"] != null)
            {
                password = Request.QueryString["password"].ToString();
            }



            hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
            if (membersManager.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);

                return "<script type='text/javascript'>window.location='/black/srHome';</script>";
            }


            return "wrong user/name password";
        }






        [AllowAnonymous]
        public PartialViewResult Register()
        {
            return PartialView();
        }


        [AllowAnonymous]
        public string RegisterUser()
        {
            string username = "";
            string password = "";
            string name = "";
            string email = "";


            if (Request.QueryString["username"] != null)
            {
                username = Request.QueryString["username"].ToString();
            }

            if (Request.QueryString["password"] != null)
            {
                password = Request.QueryString["password"].ToString();
            }

            if (Request.QueryString["name"] != null)
            {
                name = Request.QueryString["name"].ToString();
            }

            if (Request.QueryString["email"] != null)
            {
                email = Request.QueryString["email"].ToString();
            }
            //-----------------------------------------------------------------------------------------------------




            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement member_manager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            //-----------------------------------------------------------------------------------------------------






            if (email != "" && username != "" && password != "")
            {
                if (member_manager.getMemberByUserName(username).id == 0)
                {
                    bool member_validate = false;
                    member_validate = member_manager.ValidateUser(username, password);

                    hypster_tv_DAL.Member member_email = new hypster_tv_DAL.Member();
                    member_email = member_manager.getMemberByEmail(email);


                    if (member_validate == false && member_email.id == 0) //user not exist (add new)
                    {
                        hypster_tv_DAL.Member MEMBER_TO_ADD = new hypster_tv_DAL.Member();
                        MEMBER_TO_ADD.username = username;
                        MEMBER_TO_ADD.password = password;
                        MEMBER_TO_ADD.email = email;
                        MEMBER_TO_ADD.regdate = DateTime.Now;


                        string IP_Address;
                        IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (IP_Address == null)
                            IP_Address = Request.ServerVariables["REMOTE_ADDR"];
                        else
                            IP_Address = "";


                        MEMBER_TO_ADD.RegistrationIp = IP_Address;
                        MEMBER_TO_ADD.LastActivityIp = IP_Address;
                        MEMBER_TO_ADD.LastActivityDate = DateTime.Now;
                        MEMBER_TO_ADD.AutoshareEnabled = true;


                        //add new member
                        hyDB.Members.AddObject(MEMBER_TO_ADD);
                        hyDB.SaveChanges();






                        //need to add email verification logic
                        //-----------------------------------------------------------------------------------
                        FormsAuthentication.SetAuthCookie(username, false);
                        emailManager.SendWelcomeEmail("Welcome to Hypster", MEMBER_TO_ADD.email);
                        return "Thanks you for registering.";
                    }
                    else //if user already exist - need to verify what is used and display message
                    {
                        if (member_validate == true)
                        {
                            return "You already registrered. Please login.";
                        }

                        if (member_email.id != 0)
                        {
                            return "User with following email already registered.";
                        }
                    }


                }
                else
                {
                    return "This username is occupied by someone else.";
                }
            }
            else //missing some info
            {
                if (email == "")
                {
                    return "Missing email.";
                }

                if (username == "")
                {
                    return "Missing username.";
                }

                if (password == "")
                {
                    return "Missing password.";
                }
            }




            return "";
        }


        public string logOut()
        {
            FormsAuthentication.SignOut();

            return "<script>window.location='/black/srAccount/Login';</script>";
        }


        [AllowAnonymous]
        public string AccessDenied()
        {
            return "<script>window.location = '/black/srAccount/login';</script>";
        }

        //------------------------------------------------------------------------------------------------------------------












        public string SetDefaultPlaylist(int id)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
            playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


            foreach (var item in playlists_list)
            {
                if (id == item.id)
                {
                    curr_user.active_playlist = item.id;
                    memberManager.SetUserDefaultPlaylist(User.Identity.Name, curr_user.id, curr_user.active_playlist);
                }
            }


            return "ok";
        }




        public string DeletePlaylist(int id)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
            playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


            foreach (var item in playlists_list)
            {
                if (id == item.id)
                {
                    playlistManager.Delete_Playlist(curr_user.id, item.id);
                }
            }

            return "ok";
        }




        public ActionResult RenamePlaylist(int id)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
            playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


            hypster_tv_DAL.Playlist curr_playlist = new hypster_tv_DAL.Playlist();
            foreach (var item in playlists_list)
            {
                if (id == item.id)
                {
                    curr_playlist = item;
                }
            }

            return View(curr_playlist);
        }


        public string ConfirmRenamePlaylist(int id)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();
            playlists_list = playlistManager.GetUserPlaylists(curr_user.id);


            foreach (var item in playlists_list)
            {
                if (id == item.id)
                {
                    string new_name = "";
                    if (Request.QueryString["plst_name"] != null)
                    {
                        new_name = Request.QueryString["plst_name"].ToString();

                        playlistManager.Edit_Playlist(curr_user.id, item.id, new_name);
                    }
                }
            }

            return "ok";
        }




        public ActionResult AddNewPlaylist()
        {

            return View();
        }

        public string AddNewPlaylistPost(string id)
        {
            string playlist_name = id;

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);

            hypster_tv_DAL.Playlist add_new_playlist = new hypster_tv_DAL.Playlist();
            add_new_playlist.name = playlist_name;
            add_new_playlist.userid = curr_user.id;
            add_new_playlist.username = curr_user.username;

            playlistManager.AddNewPlaylist(add_new_playlist);


            return "ok";
        }
        //------------------------------------------------------------------------------------------------------------------




        //------------------------------------------------------------------------------------------------------------------
        public PartialViewResult ManageAccount()
        {
            return PartialView();
        }


        public PartialViewResult ManageAccountInfo()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_member = new hypster_tv_DAL.Member();
            curr_member = memberManager.getMemberByUserName(User.Identity.Name);

            return PartialView(curr_member);
        }





        public PartialViewResult SaveAccountInfo()
        {
            string name = "";
            string old_pass = "";
            string new_pass = "";

            if (Request.QueryString["name"] != null)
            {
                name = Request.QueryString["name"].ToString();
            }

            if (Request.QueryString["old_pass"] != null)
            {
                old_pass = Request.QueryString["old_pass"].ToString();
            }

            if (Request.QueryString["new_pass"] != null)
            {
                new_pass = Request.QueryString["new_pass"].ToString();
            }



            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            if (name != "")
            {
                curr_user.name = name;
                memberManager.UpdateMemberProfileDetails(curr_user.username, curr_user.id, curr_user.name, curr_user.AutoshareEnabled, (DateTime)curr_user.birth, curr_user.city, curr_user.country, curr_user.zipcode, (byte)curr_user.sex);
            }


            if (old_pass != "******" && old_pass != "")
            {
                if (curr_user.password == old_pass)
                {
                    if (new_pass != "******" && new_pass != "")
                    {
                        curr_user.password = new_pass;
                        memberManager.UpdateMemberPassword(User.Identity.Name, curr_user.id, curr_user.password);
                    }
                }
            }




            return PartialView();
        }






        public PartialViewResult managePlaylists()
        {
            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();


            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.isAuthorized = false;
                return PartialView(playlists_list);
            }


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            playlists_list = playlistManager.GetUserPlaylists(memberManager.getMemberByUserName(User.Identity.Name).id);

            return PartialView(playlists_list);
        }
        //------------------------------------------------------------------------------------------------------------------









        //------------------------------------------------------------------------------------------------------------------
        public PartialViewResult AddToMyPlaylist(string id)
        {
            string song_guid = id;

            ViewBag.song_guid = song_guid;

            return PartialView();
        }



        public string SubmitAddToMyPlaylist(string id)
        {
            string song_guid = id;

            hypster_tv_DAL.Hypster_Entities hypDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();



            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            short Sel_Sort_Order = 0;
            playlistManager.IncrementPlaylistSongOrder(curr_user.id, curr_user.active_playlist);
            Sel_Sort_Order = 1; //set sort order to first position


            hypster_tv_DAL.Song curr_song = new hypster_tv_DAL.Song();
            curr_song = songManager.GetSongByGUID(song_guid);



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

                List<hypster_tv_DAL.Playlist> playlists_list = playlistManager.GetUserPlaylists(curr_user.id);
                if (playlists_list.Count > 0)
                {
                    curr_user.active_playlist = playlists_list[0].id;
                    memberManager.SetUserDefaultPlaylist(User.Identity.Name, curr_user.id, curr_user.active_playlist);
                }
                //else error - need to have dafult playlist
            }
            //-----------------------------------------------------------------------------------------



            //need to add song to database 
            if (curr_song.id == 0)
            {
                //need to get song details
                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);
                string feedUrl = "http://gdata.youtube.com/feeds/api/videos/" + song_guid;
                Video video = request.Retrieve<Video>(new Uri(feedUrl));


                //need to modify to add more song params
                hypster_tv_DAL.Song new_song = new hypster_tv_DAL.Song();
                if (video.Title != null)
                {
                    new_song.Title = video.Title;
                }
                new_song.YoutubeId = song_guid;
                new_song.adddate = DateTime.Now;
                new_song.YoutubeProcessed = false;

                if (video.Author != null)
                    new_song.Author = video.Uploader;
                if (video.RatingAverage != null)
                    new_song.Rating = (float)video.RatingAverage;
                if (video.AppControl != null)
                    new_song.Syndication = 1;

                hypDB.Songs.AddObject(new_song);
                hypDB.SaveChanges();





                //get newely added song
                curr_song = songManager.GetSongByGUID(song_guid);


                //add to playlist data
                hypster_tv_DAL.PlaylistData new_playlistData = new hypster_tv_DAL.PlaylistData();
                new_playlistData.songid = curr_song.id;
                new_playlistData.playlist_id = curr_user.active_playlist;
                new_playlistData.userid = curr_user.id;
                new_playlistData.sortid = Sel_Sort_Order;


                hypDB.PlaylistDatas.AddObject(new_playlistData);
                hypDB.SaveChanges();
            }
            else //if song exist in database
            {

                hypster_tv_DAL.PlaylistData plst_data = new hypster_tv_DAL.PlaylistData();
                plst_data.songid = curr_song.id;
                plst_data.playlist_id = curr_user.active_playlist;
                plst_data.userid = curr_user.id;
                plst_data.sortid = Sel_Sort_Order;


                hypDB.PlaylistDatas.AddObject(plst_data);
                hypDB.SaveChanges();

            }


            return "ok";
        }




        public PartialViewResult DeleteFromMyPlaylist(int id)
        {
            int curr_song_id = id;

            ViewBag.CURR_SONG_ID = curr_song_id;

            return PartialView();
        }



        public string SubmitDeleteFromMyPlaylist(int id)
        {
            int curr_song_id = id;

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);

            playlistManager.DeleteSong(curr_user.id, curr_song_id);



            return "ok";
        }
        //------------------------------------------------------------------------------------------------------------------







        //------------------------------------------------------------------------------------------------------------------
        [AllowAnonymous]
        public string SetSpeechSound(string id)
        {
            HttpContext.Response.Cookies.Remove("speech_sound");


            HttpCookie myCookie = new HttpCookie("speech_sound");
            myCookie.Value = id;
            myCookie.Expires = DateTime.Now.AddDays(3);
            HttpContext.Response.Cookies.Add(myCookie);


            return "ok";
        }
        //------------------------------------------------------------------------------------------------------------------



    }
}
