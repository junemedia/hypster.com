using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class Billboard_Year_End_Hot_100Controller : Controller
    {
        //
        // GET: /Billboard_Year_End_Hot_100/


        public ActionResult Index()
        {
            int CURR_USER_ID = 5251398;  //int CURR_USER_ID = 1;

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();



            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByID(CURR_USER_ID);


            List<hypster_tv_DAL.Playlist> userPlaylists_list = new List<hypster_tv_DAL.Playlist>();
            userPlaylists_list = playlistManager.GetUserPlaylists(curr_user.id);


            return View(userPlaylists_list);
        }





    }
}
