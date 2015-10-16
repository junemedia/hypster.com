using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dRadioController : Controller
    {
        //
        // GET: /HypDesktop/dRadio/


        //
        //------------------------------------------------------------------------------------------------------
        public const int CHARTS_LIMIT = 16;
        //------------------------------------------------------------------------------------------------------




        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult Index()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model.genres_list = genreManager.GetMusicGenresList();


            hypster_tv_DAL.chartsManager chartsManager = new hypster_tv_DAL.chartsManager();
            model.charts_list = chartsManager.GetAllCharts();
            if (model.charts_list.Count > CHARTS_LIMIT)
            {
                model.charts_list.RemoveRange(CHARTS_LIMIT, model.charts_list.Count - CHARTS_LIMIT);
            }


            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();
            model.festivals_list = festivalManager.GetAllFestivals();
            if (model.festivals_list.Count > CHARTS_LIMIT)
            {
                model.festivals_list.RemoveRange(CHARTS_LIMIT, model.festivals_list.Count - CHARTS_LIMIT);
            }

            return View(model);
        }




    }
}
