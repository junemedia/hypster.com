using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class GamehypeController : Controller
    {


        //
        // GET: /Gamehype/
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult Stream(string id)
        {
            //ViewBag.StreamName = id;
            //temporary hardcoded while we determine how we use gamehype


            switch (id)
            {
                case "BestRivenNA":
                    ViewBag.StreamName = "BestRivenNA";
                    ViewBag.HypName = "BestRivenNA";
                    break;
                case "BoxBox":
                    ViewBag.StreamName = "Flosd";
                    ViewBag.HypName = "BoxBox";
                    break;
                case "Ellohime":
                    ViewBag.StreamName = "Ellohime";
                    ViewBag.HypName = "Ellohime";
                    break;
                case "OxiChampion":
                    ViewBag.StreamName = "OxiChampion";
                    ViewBag.HypName = "OxiChampion";
                    break;
                case "Smoovez":
                    ViewBag.StreamName = "smoove3";
                    ViewBag.HypName = "Smoovez";
                    break;
                case "Dakotaz":
                    ViewBag.StreamName = "Dakotaz";
                    ViewBag.HypName = "DakotaWolves";
                    break;


                case "Noobcake86":
                    ViewBag.StreamName = "Noobcake86";
                    ViewBag.HypName = "FallerenX";
                    break;
                case "DNKu123":
                    ViewBag.StreamName = "DNKu123";
                    ViewBag.HypName = "dnku";
                    break;
                case "CJ_Zeed":
                    ViewBag.StreamName = "CJ_Zeed";
                    ViewBag.HypName = "Aarthas";
                    break;


                case "ZeeooN":
                    ViewBag.StreamName = "zeeoon";
                    ViewBag.HypName = "necatiakcay";
                    break;
                case "Bowsersaurus":
                    ViewBag.StreamName = "Bowsersaurus";
                    ViewBag.HypName = "Bowsersaurus";
                    break;
                case "totemtoe":
                    ViewBag.StreamName = "totemtoe";
                    ViewBag.HypName = "totemtoe";
                    break;
            }



            return View();
        }




        [OutputCache(Duration = 12)]
        public ActionResult GetGamehypeHomeWidget_PR()
        {
            return View();
        }



    }
}
