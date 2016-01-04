using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class CasaController : Controller
    {
        //
        // GET: /es/Casa/

        public ActionResult Index()
        {
            return View();
        }


        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult visualSearchBar()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            List<hypster_tv_DAL.VisualSearch> model = visualSearchManager.getVisualSearchArtists_cached();


            return View(model);
        }



        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Get_MostPopularSong_Random()
        {
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> model = songManager.Get_MostPopularSong_Random();


            return View(model);
        }








        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Get_Home_Slideshow()
        {
            hypster_tv_DAL.homeSlideshowManager homeSlideshowManager = new hypster_tv_DAL.homeSlideshowManager();

            List<hypster_tv_DAL.homeSlideshow> model = new List<hypster_tv_DAL.homeSlideshow>();
            model = homeSlideshowManager.getHomeSlideshow();

            return View(model);
        }

    }
}
