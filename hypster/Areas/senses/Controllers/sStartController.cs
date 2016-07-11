using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.senses.Controllers
{
    [AuthorizeBlack]
    public class sStartController : Controller
    {
        //
        // GET: /senses/sStart/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult home()
        {
            return View();
        }


        public ActionResult tutorial()
        {   
            return View("tutorial");
        }


    }
}
