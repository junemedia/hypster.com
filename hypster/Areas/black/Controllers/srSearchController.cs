using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;


namespace hypster.Areas.black.Controllers
{

    [AuthorizeBlack]
    public class srSearchController : Controller
    {
        //
        // GET: /black/srSearch/

        public ActionResult Index()
        {
            return View();
        }

    }


}
