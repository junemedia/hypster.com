using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.resources.Controllers
{
    public class manualsController : Controller
    {
        //
        // GET: /resources/manuals/

        private const int PAGE_LIMIT = 5;




        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //[OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            List<hypster_tv_DAL.Manual> model = new List<hypster_tv_DAL.Manual>();


            //----------------------------------------------------------------------------------------------
            hypster_tv_DAL.manualManagement manualManager = new hypster_tv_DAL.manualManagement();
            List<hypster_tv_DAL.Manual> manuals_list = new List<hypster_tv_DAL.Manual>();
            manuals_list = manualManager.GetActiveManuals();



            /*
            int limit = PAGE_LIMIT;
            foreach (var item in manuals_list)
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
            */


            return View(manuals_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 30)]
        public ActionResult Page(int id)
        {
            List<hypster_tv_DAL.Manual> model = new List<hypster_tv_DAL.Manual>();



            //----------------------------------------------------------------------------------------------
            hypster_tv_DAL.manualManagement manualManager = new hypster_tv_DAL.manualManagement();
            List<hypster_tv_DAL.Manual> manuals_list_tmp = new List<hypster_tv_DAL.Manual>();
            manuals_list_tmp = manualManager.GetActiveManuals();


            int start_pos = 1;
            start_pos = (id * PAGE_LIMIT) - PAGE_LIMIT;

            int end_pos = 1;
            end_pos = start_pos + PAGE_LIMIT;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < manuals_list_tmp.Count)
                {
                    model.Add(manuals_list_tmp[i]);
                }
            }
            //----------------------------------------------------------------------------------------------

            ViewBag.prevPageID = id - 1;
            ViewBag.nextPageID = id + 1;



            //return View("Index", model);
            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++















        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 30)]
        public ActionResult Details(string id)
        {
            string manual_GUID = "";

            manual_GUID = id;
            ViewBag.GUID = manual_GUID;



            hypster.Areas.resources.ViewModels.ManualViewModel model = new ViewModels.ManualViewModel();
            hypster_tv_DAL.manualManagement manualManager = new hypster_tv_DAL.manualManagement();

            model.manual = manualManager.GetManualByGuid(id);
            model.manual_slides = manualManager.GetManualSlides(model.manual.Manual_ID);



            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    }
}
