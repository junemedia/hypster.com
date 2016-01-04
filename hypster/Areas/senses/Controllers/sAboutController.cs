using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.senses.Controllers
{
    [AuthorizeBlack]
    public class sAboutController : Controller
    {
        //
        // GET: /senses/sAbout/

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
