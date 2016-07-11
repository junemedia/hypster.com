using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class HomPopularCont_ViewModel
    {

        public List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
        public List<hypster_tv_DAL.Festival> festivals_list = new List<hypster_tv_DAL.Festival>();
        public List<hypster_tv_DAL.Playlist> popular_playlists = new List<hypster_tv_DAL.Playlist>();


        public HomPopularCont_ViewModel()
        {
        }



    }
}