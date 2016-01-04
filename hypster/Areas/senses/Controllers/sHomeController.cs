using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.senses.Controllers
{
    public class sHomeController : Controller
    {
        //
        // GET: /senses/sHome/


        public ActionResult Index()
        {
            if (Request.QueryString["hypAPPind"] != null)
            {
                System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
                i_chache.Add("hypAPPind", "1", DateTime.Now.AddDays(2));

                HttpCookie myCookie = new HttpCookie("hypAPPind");
                myCookie["hypAPPind"] = "1";
                myCookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Add(myCookie);
            }


            return View();
        }



        public ActionResult Home()
        {
            return View();
        }




    }
}
