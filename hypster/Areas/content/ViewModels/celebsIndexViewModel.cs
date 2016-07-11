using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster_tv.ViewModels
{
    public class celebsIndexViewModel
    {
        public hypster_tv_DAL.newsCeleb featured_seleb = new hypster_tv_DAL.newsCeleb();
        public List<hypster_tv_DAL.newsCeleb> posts_list = new List<hypster_tv_DAL.newsCeleb>();


        public celebsIndexViewModel()
        {
        }



    }
}