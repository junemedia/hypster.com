using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class contestController : Controller
    {
        //
        // GET: /contest/



        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            hypster_tv_DAL.voteManagement voteManager = new hypster_tv_DAL.voteManagement();
            List<hypster_tv_DAL.CoverContest> covers_list = new List<hypster_tv_DAL.CoverContest>();
            covers_list = voteManager.GetAllCoverContestSongs();


            return View(covers_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }
}
