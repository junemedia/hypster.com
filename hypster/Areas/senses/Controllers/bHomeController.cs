using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.senses.Controllers
{
    [AuthorizeBlack]
    public class bHomeController : Controller
    {
        //
        // GET: /senses/


        public ActionResult Index()
        {
            return View();
        }



    }
}
