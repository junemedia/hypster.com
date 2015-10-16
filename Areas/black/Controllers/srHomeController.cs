using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class srHomeController : Controller
    {
        //
        // GET: /black/srHome/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult step_2()
        {
            return View();
        }


        public ActionResult tutorial()
        {   
            return View("tutorial");
        }


    }
}
