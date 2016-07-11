using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class bHomeController : Controller
    {
        //
        // GET: /black/bHome/


        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated == false)
            //{
            //    return RedirectPermanent("/black/bAccount/Login");
            //}

            return View();
        }



        public PartialViewResult step_1()
        {
            return PartialView();
        }


        public PartialViewResult step_2()
        {
            return PartialView();
        }


        public PartialViewResult tutorial()
        {
            return PartialView();
        }


    }
}
