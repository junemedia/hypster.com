using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class srPlaylistsController : Controller
    {
        //
        // GET: /black/srPlaylists/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult myPlaylists()
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

            return View(playlists_list);
        }



    }
}
