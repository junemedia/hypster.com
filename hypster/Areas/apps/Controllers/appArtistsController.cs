using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.apps.Controllers
{
    public class appArtistsController : Controller
    {
        //
        // GET: /apps/appArtists/


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
