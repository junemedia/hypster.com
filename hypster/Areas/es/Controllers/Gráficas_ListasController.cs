using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class Gráficas_ListasController : Controller
    {
        //
        // GET: /es/Gráficas_Listas/

        private const int PAGE_LIMIT = 5;



        [OutputCache(Duration = 90)]
        public ActionResult Index()
        {
            List<hypster.ViewModels.chartViewModel> charts_list_model = new List<hypster.ViewModels.chartViewModel>();



            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();
            List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
            charts_list = chartManager.GetAllCharts();



            //----------------------------------------------------------------------------------------------
            int limit = PAGE_LIMIT;
            foreach (var item in charts_list)
            {
                hypster.ViewModels.chartViewModel chart_model = new ViewModels.chartViewModel();
                chart_model.chart = item;

                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                chart_model.chart_songs_list = playlistManager.GetSongsForPlayList((int)item.Chart_User_ID, (int)item.Chart_Playlist_ID);

                charts_list_model.Add(chart_model);


                limit -= 1;
                if (limit == 0)
                    break;
            }
            //----------------------------------------------------------------------------------------------


            ViewBag.prevPageID = 0;
            ViewBag.nextPageID = 2;


            return View(charts_list_model);
        }






        [OutputCache(Duration = 90)]
        public ActionResult Page(int id)
        {
            List<hypster.ViewModels.chartViewModel> charts_list_model = new List<hypster.ViewModels.chartViewModel>();



            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();
            List<hypster_tv_DAL.Chart> charts_list_tmp = new List<hypster_tv_DAL.Chart>();
            charts_list_tmp = chartManager.GetAllCharts();



            //----------------------------------------------------------------------------------------------
            int start_pos = 1;
            start_pos = (id * PAGE_LIMIT) - PAGE_LIMIT;

            int end_pos = 1;
            end_pos = start_pos + PAGE_LIMIT;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < charts_list_tmp.Count)
                {
                    hypster.ViewModels.chartViewModel chart_model = new ViewModels.chartViewModel();
                    chart_model.chart = charts_list_tmp[i];

                    hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                    chart_model.chart_songs_list = playlistManager.GetSongsForPlayList((int)chart_model.chart.Chart_User_ID, (int)chart_model.chart.Chart_Playlist_ID);

                    charts_list_model.Add(chart_model);
                }
            }
            //----------------------------------------------------------------------------------------------


            ViewBag.prevPageID = id - 1;
            ViewBag.nextPageID = id + 1;


            return View("Index", charts_list_model);
        }







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult BuildChart(hypster.ViewModels.chartViewModel model)
        {
            return PartialView(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }
}
