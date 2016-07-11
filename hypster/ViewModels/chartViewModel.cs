using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class chartViewModel
    {
        public hypster_tv_DAL.Chart chart = new hypster_tv_DAL.Chart();
        public List<hypster_tv_DAL.PlaylistData_Song> chart_songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();



        public chartViewModel()
        {
        }



    }
}