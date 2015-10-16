using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class artistsController : Controller
    {
        //
        // GET: /artists/

        [OutputCache(Duration = 90)]
        public ActionResult Index()
        {
            hypster_tv_DAL.artistManagement artist_manager = new hypster_tv_DAL.artistManagement();


            List<hypster_tv_DAL.ArtistAZ> art_list = new List<hypster_tv_DAL.ArtistAZ>();
            art_list = artist_manager.GetArtistsAZList();


            return View(art_list);
        }

    }
}
