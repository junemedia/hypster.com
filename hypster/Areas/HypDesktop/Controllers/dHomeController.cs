using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.HypDesktop.Controllers
{
    public class dHomeController : Controller
    {
        //
        // GET: /HypDesktop/dHome/

        public ActionResult Index()
        {
            if (Request.QueryString["playlist_id"] != null)
            {
                ViewBag.playlist_id = Request.QueryString["playlist_id"];
            }

            if (Request.QueryString["us_id"] != null)
            {
                ViewBag.user_id = Request.QueryString["us_id"];
            }


            return View();
        }



    }
}
