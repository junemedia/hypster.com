using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace hypster
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //--------------------------------------------------------------------------------------------
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //--------------------------------------------------------------------------------------------




            // Hipster
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "HypsterVsHipsterDefault",
                url: "what-is-hipster-vs-hypster",
                defaults: new { controller = "hypster", action = "what_is_hipster_vs_hypster", id = RouteParameter.Optional }
            );
            //--------------------------------------------------------------------------------------------




            // TAGS
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "TagsDefault",
                url: "tags/{tag_name}",
                defaults: new { controller = "tags", action = "getTagContent", tag_name = "" }
            );
            //--------------------------------------------------------------------------------------------



            // PUBLIC ACCOUNT
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "ProfileDefault",
                url: "profile/{user_name}",
                defaults: new { controller = "account", action = "getPublicProfile", user_name = "" }
            );
            //--------------------------------------------------------------------------------------------





            // MUSIC GENRE RADIO
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "MusicPageDefault",
                url: "music/{id}",
                defaults: new { controller = "playlists", action = "station", id = "" }
            );
            //--------------------------------------------------------------------------------------------





            // MUSIC PROFILE
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "MusicPagePlaylistDefault",
                url: "music_playlist/{plst_id}",
                defaults: new { controller = "account", action = "PublicMusicPagePlaylist", user_name = "" }
            );
            //--------------------------------------------------------------------------------------------




            // SONG LANDING
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "songLandingPageDefault",
                url: "song/{song_guid}",
                defaults: new { controller = "song", action = "getSongByID", song_guid = "" }
            );
            //--------------------------------------------------------------------------------------------




            // DEFAULT
            //--------------------------------------------------------------------------------------------
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //--------------------------------------------------------------------------------------------




            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            

        }
    }
}