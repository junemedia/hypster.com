using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.content.Controllers
{
    public class HypCelebsController : Controller
    {
        //
        // GET: /content/HypCelebs/

        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult Index()
        {
            hypster_tv.ViewModels.celebsIndexViewModel model = new hypster_tv.ViewModels.celebsIndexViewModel();

            hypster_tv_DAL.celebsManagement celebsManager = new hypster_tv_DAL.celebsManagement();

            model.posts_list = celebsManager.GetLatestCelebs_cache(100);

            if (model.posts_list.Count > 0)
                model.featured_seleb = model.posts_list[0];


            return View(model);
        }

    }
}
