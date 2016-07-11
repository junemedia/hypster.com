using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{

    public class playlistsViewModel
    {
        public List<hypster_tv_DAL.Playlist> most_popular_playlists = new List<hypster_tv_DAL.Playlist>();
        public List<hypster_tv_DAL.Playlist> most_liked_playlists = new List<hypster_tv_DAL.Playlist>();
        public List<hypster_tv_DAL.Playlist> new_playlists = new List<hypster_tv_DAL.Playlist>();


        public List<hypster_tv_DAL.FeaturedPlaylist_Result> featured_playlists = new List<hypster_tv_DAL.FeaturedPlaylist_Result>();



        public playlistsViewModel()
        {
        }

    }
}