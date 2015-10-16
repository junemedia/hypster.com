using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class connectViewModel
    {
        public List<hypster_tv_DAL.Member> members_list = new List<hypster_tv_DAL.Member>();


        public int Current_Page = 1;
        public int Number_Of_Elements = 22;
        public int Number_Of_Pages = 1;

        public int New_User_ID = 0;


        public List<hypster_tv_DAL.sp_Members_GetMembersPublicPagesRandomEx_Result> public_pages = new List<hypster_tv_DAL.sp_Members_GetMembersPublicPagesRandomEx_Result>();

        public connectViewModel()
        {
        }


    }
}