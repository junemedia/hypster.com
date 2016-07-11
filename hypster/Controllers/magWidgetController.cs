using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class magWidgetController : Controller
    {
        //
        // GET: /magWidget/

        public ActionResult Index()
        {
            return View();
        }




        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration=240)]
        public ActionResult Mag_1st_song(string name)
        {
            List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex> data = new List<hypster_tv_DAL.sp_Compatibility_CompCheck_Result_Ex>();


            hypster_tv_DAL.CompatibilityManager compManager = new hypster_tv_DAL.CompatibilityManager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            hypster_tv_DAL.Member curr_member = new hypster_tv_DAL.Member();
            curr_member = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
            songs_list = playlistManager.GetSongsForPlayList(curr_member.id, curr_member.active_playlist);


            string song_guid_str = "";
            string full_title_str = "";
            int track_id = 0;
            if (songs_list.Count > 0)
            {
                song_guid_str = songs_list[0].YoutubeId;
                full_title_str = songs_list[0].FullTitle;
                if (songs_list[0].id != null)
                {
                    track_id = (int)songs_list[0].id;
                }
            }




            if (song_guid_str != "")
            {
                hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();

                //hypster_tv_DAL.Song curr_song = new hypster_tv_DAL.Song();
                //curr_song = songManager.GetSongByGUID(song_guid_str);

                ViewBag.Curr_Song = full_title_str;
                

                //if one song selected
                if (track_id != 0)
                {
                    data = compManager.CompCheck_1_Ex(track_id);
                }
            }

            return View(data);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Mag_2nd_song()
        {

            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




    }
}
