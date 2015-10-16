using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class mobileTWController : Controller
    {
        //
        // GET: /mobileTW/


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            Session["No_Mobile"] = "Y";
            HttpCookie myCookie = new HttpCookie("No_Mobile");
            myCookie.Value = "Y";
            myCookie.Expires = DateTime.Now.AddHours(3);
            Response.Cookies.Add(myCookie);

            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




    }
}
