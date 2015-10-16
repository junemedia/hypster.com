using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class festivalViewModel
    {
        public hypster_tv_DAL.Festival festival = new hypster_tv_DAL.Festival();
        public List<hypster_tv_DAL.PlaylistData_Song> festival_songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();


        public festivalViewModel()
        {
        }
    }

}