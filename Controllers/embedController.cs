using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class embedController : Controller
    {
        //
        // GET: /embed/

        [System.Web.Mvc.OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
