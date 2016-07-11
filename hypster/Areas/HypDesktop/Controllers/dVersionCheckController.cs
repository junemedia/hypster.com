using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dVersionCheckController : Controller
    {
        //
        // GET: /HypDesktop/Version_Check/

        public ActionResult Index()
        {
            ViewBag.ServerVer = "v1.1";



            if (Request.QueryString["CV"] != null)
            {
                ViewBag.UserVer = Request.QueryString["CV"];
            }
            else
            {
                ViewBag.UserVer = "N/A";
            }

            

            return View();
        }

    }
}
