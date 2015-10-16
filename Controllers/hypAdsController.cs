using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class hypAdsController : Controller
    {
        //
        // GET: /hypAds/


        public ActionResult Index()
        {
            return View();
        }





        /// <summary>
        /// regular with refresh (refresh removed)
        /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 60)]
        public ActionResult ad_728x90()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult ad_300x250()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult ad_160x600()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 60)]
        public ActionResult i_ad_728x90()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult i_ad_300x250()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult i_ad_160x600()
        {
            return View();
        }
        /// <summary>
        /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// </summary>
        /// <returns></returns>






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 60)]
        public ActionResult i_pl_left_300x250()
        {
            return View();
        }

        [OutputCache(Duration = 60)]
        public ActionResult i_pl_right_300x250()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++









    }
}
