﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class EscuchaController : Controller
    {
        //
        // GET: /es/Escucha/

        public const int CHARTS_LIMIT = 16;


        public ActionResult Index()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            //model.genres_list = genreManager.GetMusicGenresList();
            //model.most_popular_songs = songManager.Get_MostPopularSong_Random();
            if (User.Identity.IsAuthenticated == true)
            {
                model.most_viewed_playlists = playlistManager.GetUserPlaylists(memberManager.getMemberByUserName(User.Identity.Name).id);

                //get playlists I like
                model.playlists_I_like = playlistManager.GetPlaylistsILike(memberManager.getMemberByUserName(User.Identity.Name).id);
            }


            //check if search requested
            if (Request.QueryString["ss"] != null)
            {
                ViewBag.searchString = Request.QueryString["ss"];
            }


            return View(model);
        }





        public ActionResult Music()
        {
            return RedirectToAction("Index");
        }


        public ActionResult Radio()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();



            model.genres_list = genreManager.GetMusicGenresList();
            //model.most_popular_songs = songManager.Get_MostPopularSong_Random();


            if (User.Identity.IsAuthenticated == true)
            {
                model.most_viewed_playlists = playlistManager.GetUserPlaylists(memberManager.getMemberByUserName(User.Identity.Name).id);
            }
            else
            {
                model.most_viewed_playlists = playlistManager.GetMostViewedPlaylists();
            }

            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            model.visualSearch_list = visualSearchManager.getVisualSearchArtists_cached();



            //check if search requested
            if (Request.QueryString["ss"] != null)
            {
                ViewBag.searchString = Request.QueryString["ss"];
            }

            return View(model);
        }



        public ActionResult Playlists()
        {
            return RedirectToAction("Index");
        }












        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult visualSearchBar()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            List<hypster_tv_DAL.VisualSearch> model = visualSearchManager.getVisualSearchArtists_cached();

            return View(model);
        }






        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult RadioStationsBar()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model.genres_list = genreManager.GetMusicGenresList();


            return View(model);
        }



        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult GetMostViewedPlaylists()
        {
            hypster.ViewModels.listenViewModel model = new ViewModels.listenViewModel();

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            model.most_viewed_playlists = playlistManager.GetMostViewedPlaylists();

            return View(model);
        }





    }
}
