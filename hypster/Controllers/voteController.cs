using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class voteController : Controller
    {
        //
        // GET: /vote/


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 20)]
        public ActionResult VoteIndex()
        {
            hypster_tv_DAL.voteManagement voteManager = new hypster_tv_DAL.voteManagement();



            ViewBag.Song1_ViewNum = voteManager.GetVotesNum(1);
            ViewBag.Song2_ViewNum = voteManager.GetVotesNum(2);

            hypster_tv_DAL.VoteForSong vfs = new hypster_tv_DAL.VoteForSong();
            vfs = voteManager.Get_Active_VoteForSong();


            return View(vfs);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string VoteFor()
        {
            hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
            int votesNum = 0;

            if (User.Identity.IsAuthenticated)
            {
                hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
                member = membersManager.getMemberByUserName(User.Identity.Name);


                int VOTE_FOR = 0;
                if (Request.QueryString["V_F"] != null)
                {
                    Int32.TryParse(Request.QueryString["V_F"].ToString(), out VOTE_FOR);
                }


                hypster_tv_DAL.voteManagement voteManager = new hypster_tv_DAL.voteManagement();
                votesNum = voteManager.VoteFor(member.id, VOTE_FOR);
            }

            return votesNum.ToString();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public string VoteForContest()
        {
            hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
            int votesNum = 0;

            
                
            int VOTE_FOR = 0;
            if (Request.QueryString["V_F"] != null)
            {
                Int32.TryParse(Request.QueryString["V_F"].ToString(), out VOTE_FOR);
            }

            string IP_Address;
            IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IP_Address == null)
                IP_Address = Request.ServerVariables["REMOTE_ADDR"];
            else
                IP_Address = "";


            hypster_tv_DAL.voteManagement voteManager = new hypster_tv_DAL.voteManagement();
            votesNum = voteManager.VoteForContest(VOTE_FOR, IP_Address, DateTime.Now);
            


            return votesNum.ToString();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





    }
}
