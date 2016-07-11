using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.hypAPI.Controllers
{
    public class hypCommandController : Controller
    {
        //
        // GET: /hypAPI/hypCommand/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult process()
        {
            string view_name = "process";
            string command_guid = "";


            if (Request.QueryString["a"] != null)
            {
                command_guid = Request.QueryString["a"].ToString();
                ViewBag.a = command_guid;
            }


            return View(view_name);
        }

    }
}
