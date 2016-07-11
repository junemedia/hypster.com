using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.ru.Controllers
{
    public class ГлавнаяController : Controller
    {
        //
        // GET: /ru/Главная/

        public ActionResult Index()
        {
            return View();
        }



        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult Визуальный_Поиск()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            List<hypster_tv_DAL.VisualSearch> model = visualSearchManager.getVisualSearchArtists_cached();


            return View(model);
        }




        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Популярные_песни_Случайные()
        {
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> model = songManager.Get_MostPopularSong_Random();


            return View(model);
        }



    }
}
