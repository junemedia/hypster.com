using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class flashController : Controller
    {
        //
        // GET: /flash/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult N(string id)
        {
            return RedirectPermanent("/");
        }



    }
}
