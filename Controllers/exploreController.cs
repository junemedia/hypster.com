using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class exploreController : ControllerBase
    {
        //
        // GET: /explore/

        private const int PAGE_LIMIT = 5;




        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 90)]
        public ActionResult Index()
        {
            hypster_tv_DAL.homeSlideshowManager homeSlideshowManager = new hypster_tv_DAL.homeSlideshowManager();

            List<hypster_tv_DAL.homeSlideshow> model_list = new List<hypster_tv_DAL.homeSlideshow>();
            model_list = homeSlideshowManager.getHomeSlideshow();



            List<hypster_tv_DAL.homeSlideshow> model = new List<hypster_tv_DAL.homeSlideshow>();
            //----------------------------------------------------------------------------------------------
            int limit = PAGE_LIMIT;
            foreach (var item in model_list)
            {
                model.Add(item);


                limit -= 1;
                if (limit == 0)
                    break;
            }
            //----------------------------------------------------------------------------------------------
            

            ViewBag.prevPageID = 0;
            ViewBag.nextPageID = 2;


            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 90)]
        public ActionResult Page(int id)
        {
            hypster_tv_DAL.homeSlideshowManager homeSlideshowManager = new hypster_tv_DAL.homeSlideshowManager();

            List<hypster_tv_DAL.homeSlideshow> model_list = new List<hypster_tv_DAL.homeSlideshow>();
            model_list = homeSlideshowManager.getHomeSlideshow();



            List<hypster_tv_DAL.homeSlideshow> model = new List<hypster_tv_DAL.homeSlideshow>();
            //----------------------------------------------------------------------------------------------
            int start_pos = 1;
            start_pos = (id * PAGE_LIMIT) - PAGE_LIMIT;

            int end_pos = 1;
            end_pos = start_pos + PAGE_LIMIT;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < model_list.Count)
                {
                    model.Add(model_list[i]);
                }
            }
            //----------------------------------------------------------------------------------------------


            ViewBag.prevPageID = id - 1;
            ViewBag.nextPageID = id + 1;


            return View("Index", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    }
}
