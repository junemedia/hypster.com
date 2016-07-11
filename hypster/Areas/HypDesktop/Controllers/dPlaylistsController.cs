using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dPlaylistsController : Controller
    {

        //
        // GET: /HypDesktop/Playlists/

        public ActionResult Index()
        {
            return View();
        }



        public string GetUserPlaylists(int id)
        {
            string ret_str = "";


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> playlists = new List<hypster_tv_DAL.Playlist>();
            playlists = playlistManager.GetUserPlaylists(id);


            foreach (hypster_tv_DAL.Playlist item in playlists)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(item.id).Append("|").Append(item.name).Append("~");
                ret_str += sb.ToString();
            }

            return ret_str;
        }


    }
}
