using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class FestivalesController : Controller
    {
        //
        // GET: /es/Festivales/

        private const int PAGE_LIMIT = 5;



        [OutputCache(Duration = 90)]
        public ActionResult Index()
        {
            List<hypster.ViewModels.festivalViewModel> festivals_list_model = new List<hypster.ViewModels.festivalViewModel>();



            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();
            List<hypster_tv_DAL.Festival> festivals_list = new List<hypster_tv_DAL.Festival>();
            festivals_list = festivalManager.GetAllFestivals();



            //----------------------------------------------------------------------------------------------
            int limit = PAGE_LIMIT;
            foreach (var item in festivals_list)
            {
                hypster.ViewModels.festivalViewModel festival_model = new ViewModels.festivalViewModel();
                festival_model.festival = item;

                hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                festival_model.festival_songs_list = playlistManager.GetSongsForPlayList((int)item.Festival_User_ID, (int)item.Festival_Playlist_ID);

                festivals_list_model.Add(festival_model);


                limit -= 1;
                if (limit == 0)
                    break;
            }
            //----------------------------------------------------------------------------------------------


            ViewBag.prevPageID = 0;
            ViewBag.nextPageID = 2;


            return View(festivals_list_model);
        }






        [OutputCache(Duration = 90)]
        public ActionResult Page(int id)
        {
            List<hypster.ViewModels.festivalViewModel> festivals_list_model = new List<hypster.ViewModels.festivalViewModel>();



            hypster_tv_DAL.FestivalManager festivalManager = new hypster_tv_DAL.FestivalManager();
            List<hypster_tv_DAL.Festival> festivals_list_tmp = new List<hypster_tv_DAL.Festival>();
            festivals_list_tmp = festivalManager.GetAllFestivals();



            //----------------------------------------------------------------------------------------------
            int start_pos = 1;
            start_pos = (id * PAGE_LIMIT) - PAGE_LIMIT;

            int end_pos = 1;
            end_pos = start_pos + PAGE_LIMIT;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < festivals_list_tmp.Count)
                {
                    hypster.ViewModels.festivalViewModel festival_model = new ViewModels.festivalViewModel();
                    festival_model.festival = festivals_list_tmp[i];

                    hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
                    festival_model.festival_songs_list = playlistManager.GetSongsForPlayList((int)festival_model.festival.Festival_User_ID, (int)festival_model.festival.Festival_Playlist_ID);

                    festivals_list_model.Add(festival_model);
                }
            }
            //----------------------------------------------------------------------------------------------


            ViewBag.prevPageID = id - 1;
            ViewBag.nextPageID = id + 1;


            return View("Index", festivals_list_model);
        }







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult BuildFestival(hypster.ViewModels.festivalViewModel model)
        {
            return PartialView(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    }
}
