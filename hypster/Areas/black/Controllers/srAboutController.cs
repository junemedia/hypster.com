using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class srAboutController : Controller
    {
        //
        // GET: /black/srAbout/

        public ActionResult Index()
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
