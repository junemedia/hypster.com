using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dPopularController : Controller
    {
        //
        // GET: /HypDesktop/dPopular/

        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult Index()
        {
            hypster.ViewModels.playlistsViewModel model = new ViewModels.playlistsViewModel();


            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.most_liked_playlists = playlistManager.GetMostLikedPlaylists();
            model.new_playlists = playlistManager.GetNewPlaylists();
            model.most_popular_playlists = playlistManager.GetMostViewedPlaylists();



            return View(model);
        }

    }
}
