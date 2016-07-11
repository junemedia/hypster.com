using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class bAboutController : Controller
    {
        //
        // GET: /black/bAbout/

        public ActionResult Index()
        {
            return View();
        }



        [AllowAnonymous]
        public ActionResult Presentation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult getChrome()
        {
            return View();
        }



    }
}
