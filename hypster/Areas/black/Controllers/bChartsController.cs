using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class bChartsController : Controller
    {
        //
        // GET: /black/bCharts/

        public PartialViewResult Index()
        {
            hypster_tv_DAL.chartsManager chartManager = new hypster_tv_DAL.chartsManager();


            List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
            charts_list = chartManager.GetTopCharts();


            return PartialView(charts_list);
        }

    }
}
