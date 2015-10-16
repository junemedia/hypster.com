using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{

    public class MemberPublicPageViewModel
    {
        public hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
        public hypster_tv_DAL.MemberPublicPage member_page = new hypster_tv_DAL.MemberPublicPage();
        public List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();



        public List<hypster_tv_DAL.Playlist> userPlaylists_list = new List<hypster_tv_DAL.Playlist>();


        public int New_User_ID = 0;
        public List<hypster_tv_DAL.Photo> userPhotos_list = new List<hypster_tv_DAL.Photo>();



        public MemberPublicPageViewModel()
        {
        }


    }
}