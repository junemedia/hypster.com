﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class listenViewModel
    {
        public List<hypster_tv_DAL.MusicGenre> genres_list = new List<hypster_tv_DAL.MusicGenre>();

        public List<hypster_tv_DAL.Song> most_popular_songs = new List<hypster_tv_DAL.Song>();

        public List<hypster_tv_DAL.Playlist> most_viewed_playlists = new List<hypster_tv_DAL.Playlist>();

        public List<hypster_tv_DAL.VisualSearch> visualSearch_list = new List<hypster_tv_DAL.VisualSearch>();

        public List<hypster_tv_DAL.Playlist> playlists_I_like = new List<hypster_tv_DAL.Playlist>();



        public List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
        public List<hypster_tv_DAL.Festival> festivals_list = new List<hypster_tv_DAL.Festival>();


        public listenViewModel()
        {
        }


    }
}