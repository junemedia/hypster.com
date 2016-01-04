using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster_tv.ViewModels
{
    public class HomePageViewModel
    {
        public int NumOfPosts = 0;
        public int NumOfPages = 1;

        public List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
        


        public HomePageViewModel()
        {
        }



    }
}