using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.m.Controllers
{
    public class mcreateController : Controller
    {
        //
        // GET: /m/mcreate/
        //--------------------------------------------------------------------------------------------------------
        [AllowAnonymous]
        public ActionResult Index()
        {
            hypster.ViewModels.createViewModel model = new ViewModels.createViewModel();


            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        //[Authorize]
        [AllowAnonymous]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult player(string id)
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return View("create_music_player");
            }



            hypster.ViewModels.createPlayer_ViewModel model = new ViewModels.createPlayer_ViewModel();

            switch (id)
            {
                case "BarPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                case "ClassicPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                case "RadioPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                default:
                    break;
            }


            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        //[Authorize]
        [AllowAnonymous]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult playlist()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return View("create_playlist_online");
            }



            hypster.ViewModels.createPlaylist_ViewModel model = new ViewModels.createPlaylist_ViewModel();


            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();


            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------------------




            // 2.proccess user actions if any
            //-----------------------------------------------------------------------------------------------------
            // process user actions
            if (Request.QueryString["ACT"] != null)
            {

                switch (Request.QueryString["ACT"].ToString())
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_playlist":
                        int d_playlist_id = 0;
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out d_playlist_id) == false)
                            d_playlist_id = 0;

                        if (d_playlist_id != 0)
                        {
                            playlistManager.Delete_Playlist(model.curr_user.id, d_playlist_id);


                            //check if this playlist is default
                            if (model.curr_user.active_playlist == d_playlist_id)
                            {
                                memberManager.SetUserDefaultPlaylist(User.Identity.Name, model.curr_user.id, 0);
                            }

                            return RedirectPermanent("/m/mcreate/playlist");
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_song":
                        int d_song_id = 0;
                        if (Int32.TryParse(Request.QueryString["song_id"], out d_song_id) == false)
                            d_song_id = 0;


                        string pl_id = "";
                        if (Request.QueryString["playlist_id"] != null)
                            pl_id = Request.QueryString["playlist_id"].ToString();


                        if (d_song_id != 0)
                        {
                            playlistManager.DeleteSong(model.curr_user.id, d_song_id);
                            return RedirectPermanent("/m/mcreate/playlist?playlist_id=" + pl_id);
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_song_plr":
                        int d_song_id1 = 0;
                        if (Int32.TryParse(Request.QueryString["song_id"], out d_song_id1) == false)
                            d_song_id1 = 0;

                        if (d_song_id1 != 0)
                        {
                            playlistManager.DeleteSong(model.curr_user.id, d_song_id1);

                            if (Request.QueryString["ret_url"] == null)
                            {
                                return RedirectPermanent("/m/mcreate/playlist");
                            }
                            else
                            {
                                return RedirectPermanent("/m/mcreate/playlist");
                            }
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                    default:
                        break;
                }

            }
            //-----------------------------------------------------------------------------------------------------





            // 3.prepare output model
            //-----------------------------------------------------------------------------------------------------
            //hypster.ViewModels.AccountPlaylistsViewModel model = new ViewModels.AccountPlaylistsViewModel();
            //model.curr_user = member_user;
            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------




            // 4. get current requested playlist id
            // if no playlist selected display default
            //-----------------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                if (Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id) == false)
                    playlist_id = 0;
            }
            else
            {
                playlist_id = model.curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------------------




            // 5.get selected playlist details
            //-----------------------------------------------------------------------------------------------------
            foreach (var item in model.userPlaylists_list)
            {
                if (item.id == playlist_id)
                {
                    ViewBag.ActivePlaylistName = item.name;
                    ViewBag.ActivePlaylistID = item.id;
                }
            }
            //-----------------------------------------------------------------------------------------------------





            // 6.get songs for selected playlist
            //-----------------------------------------------------------------------------------------------------
            if (playlist_id != 0)
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, playlist_id);
            }
            else
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------





            //7.get tags for selected playlist
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();
            if (playlist_id != 0)
            {
                model.tags_list = tagManager.GetPlaylistTags(playlist_id);
            }
            else
            {
                model.tags_list = tagManager.GetPlaylistTags(model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------




        public ActionResult DeleteDeadLink()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            //-----------------------------------------------------------------------------------------------------
            // process user actions
            if (Request.QueryString["ACT"] != null)
            {

                switch (Request.QueryString["ACT"].ToString())
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_song":
                        int d_song_id = 0;
                        if (Int32.TryParse(Request.QueryString["song_id"], out d_song_id) == false)
                            d_song_id = 0;


                        string pl_id = "";
                        if (Request.QueryString["playlist_id"] != null)
                            pl_id = Request.QueryString["playlist_id"].ToString();


                        if (d_song_id != 0)
                        {
                            playlistManager.DeleteSong(curr_user.id, d_song_id);
                            return Content("+");
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    default:
                        break;
                }

            }
            //-----------------------------------------------------------------------------------------------------


            return Content("-");
        }










        //--------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult playlistDetails()
        {
            hypster.ViewModels.createPlaylist_ViewModel model = new ViewModels.createPlaylist_ViewModel();


            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();


            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------------------




            // 3.prepare output model
            //-----------------------------------------------------------------------------------------------------
            //hypster.ViewModels.AccountPlaylistsViewModel model = new ViewModels.AccountPlaylistsViewModel();
            //model.curr_user = member_user;
            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------




            // 4. get current requested playlist id
            // if no playlist selected display default
            //-----------------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                if (Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id) == false)
                    playlist_id = 0;
            }
            else
            {
                playlist_id = model.curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------------------




            // 5.get selected playlist details
            //-----------------------------------------------------------------------------------------------------
            foreach (var item in model.userPlaylists_list)
            {
                if (item.id == playlist_id)
                {
                    ViewBag.ActivePlaylistName = item.name;
                    ViewBag.ActivePlaylistID = item.id;
                }
            }
            //-----------------------------------------------------------------------------------------------------





            // 6.get songs for selected playlist
            //-----------------------------------------------------------------------------------------------------
            if (playlist_id != 0)
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, playlist_id);
            }
            else
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------





            //7.get tags for selected playlist
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();
            if (playlist_id != 0)
            {
                model.tags_list = tagManager.GetPlaylistTags(playlist_id);
            }
            else
            {
                model.tags_list = tagManager.GetPlaylistTags(model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult playlistDetailsAD()
        {
            hypster.ViewModels.createPlaylist_ViewModel model = new ViewModels.createPlaylist_ViewModel();


            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();


            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------------------





            // 3.prepare output model
            //-----------------------------------------------------------------------------------------------------
            //hypster.ViewModels.AccountPlaylistsViewModel model = new ViewModels.AccountPlaylistsViewModel();
            //model.curr_user = member_user;
            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------




            // 4. get current requested playlist id
            // if no playlist selected display default
            //-----------------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                if (Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id) == false)
                    playlist_id = 0;
            }
            else
            {
                playlist_id = model.curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------------------




            // 5.get selected playlist details
            //-----------------------------------------------------------------------------------------------------
            foreach (var item in model.userPlaylists_list)
            {
                if (item.id == playlist_id)
                {
                    ViewBag.ActivePlaylistName = item.name;
                    ViewBag.ActivePlaylistID = item.id;
                }
            }
            //-----------------------------------------------------------------------------------------------------





            // 6.get songs for selected playlist
            //-----------------------------------------------------------------------------------------------------
            if (playlist_id != 0)
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, playlist_id);
            }
            else
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------





            //7.get tags for selected playlist
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();
            if (playlist_id != 0)
            {
                model.tags_list = tagManager.GetPlaylistTags(playlist_id);
            }
            else
            {
                model.tags_list = tagManager.GetPlaylistTags(model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------













        //--------------------------------------------------------------------------------------------------------
        //[Authorize]
        [AllowAnonymous]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult station()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("create_radio_station");
            }



            hypster.ViewModels.createStation_ViewModel model = new ViewModels.createStation_ViewModel();

            string action = "";
            if (Request.QueryString["act"] != null)
                action = Request.QueryString["act"];

            switch (action)
            {
                case "err":
                    ViewBag.ErrorMessage = "Please enter Station Name and Artist or Genre";
                    break;
            }

            hypster_tv_DAL.RadioStationManager stationManager = new hypster_tv_DAL.RadioStationManager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();


            model.stations_list = stationManager.GetUserRadioStations(memberManager.getMemberByUserName(User.Identity.Name).id);


            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------





        //--------------------------------------------------------------------------------------------------------
        #region TAGS_LOGIC


        //--------------------------------------------------------------------------------------------------------
        public string addnewtag()
        {
            string ret_res = "";


            string tag_name = "";
            if (Request.QueryString["tag_name"] != null)
            {
                tag_name = Request.QueryString["tag_name"].ToString();
            }


            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id);
            }


            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByUserName(User.Identity.Name);



            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.Playlist curr_plst = new hypster_tv_DAL.Playlist();
            curr_plst = playlistManager.GetUserPlaylistById(member.id, playlist_id);



            if (curr_plst.id != 0)
            {
                hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();

                int tag_ID = 0;
                tag_ID = tagManager.AddNewTag(tag_name);

                tagManager.AddTagToPlaylist(tag_ID, playlist_id);

                ret_res = tag_ID.ToString() + "|" + playlist_id.ToString();
            }
            else
            {
                ret_res = "n/a";
            }



            return ret_res.ToString();
        }
        //--------------------------------------------------------------------------------------------------------





        //--------------------------------------------------------------------------------------------------------
        public string tagsForEdit()
        {
            string ret_res = "";


            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id);
            }


            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.Playlist curr_plst = new hypster_tv_DAL.Playlist();
            curr_plst = playlistManager.GetUserPlaylistById(member.id, playlist_id);




            if (curr_plst.id != 0)
            {
                hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();

                List<hypster_tv_DAL.sp_Tag_GetPlaylistTags_Result> tags_list = new List<hypster_tv_DAL.sp_Tag_GetPlaylistTags_Result>();
                tags_list = tagManager.GetPlaylistTags(playlist_id);
                foreach (var item in tags_list)
                {
                    if (Request.QueryString["toggle"] != null && (Request.QueryString["toggle"] == "on" || Request.QueryString["toggle"] == ""))
                        ret_res += "<a id='tg" + item.Tag_Playlist_ID + "' class='TagI' onclick='delete_plst_tag(" + item.Tag_Playlist_ID + ")'>" + item.Tag_Name + "&nbsp;<span class='delTagSpn'>X</span> </a>";
                    else
                        ret_res += "<a class='TagI' href='/tags/" + item.Tag_Name + "'>" + item.Tag_Name + "</a>";
                }
            }
            else
            {
                ret_res = "n/a";
            }



            return ret_res.ToString();
        }
        //--------------------------------------------------------------------------------------------------------




        //--------------------------------------------------------------------------------------------------------
        public string deletePlaylistTag()
        {
            string ret_res = "";


            int tag_plst_id = 0;
            if (Request.QueryString["tag_plst_id"] != null)
            {
                Int32.TryParse(Request.QueryString["tag_plst_id"], out tag_plst_id);
            }

            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id);
            }



            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            member = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.Playlist curr_plst = new hypster_tv_DAL.Playlist();
            curr_plst = playlistManager.GetUserPlaylistById(member.id, playlist_id);



            if (curr_plst.id != 0)
            {
                hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();

                tagManager.DeletePlaylistTag(tag_plst_id);

                ret_res = "+";
            }
            else
            {
                ret_res = "n/a";
            }

            return ret_res.ToString();
        }
        //--------------------------------------------------------------------------------------------------------


        #endregion
        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        public ActionResult create_playlist_online()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------
        public ActionResult create_music_player()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------
        public ActionResult create_radio_station()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------------------------
        public ActionResult T1()
        {
            hypster.ViewModels.createViewModel model = new ViewModels.createViewModel();

            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------



    }
}
