using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster_tv.ViewModels
{
    public class getPostViewModel
    {
        public hypster_tv_DAL.newsPost post = new hypster_tv_DAL.newsPost();
        public List<hypster_tv_DAL.newsComment> comments_list = new List<hypster_tv_DAL.newsComment>();

        public getPostViewModel()
        {
        }
    }


}