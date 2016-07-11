using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class sitemapsController : Controller
    {
        //
        // GET: /sitemaps/

        public ActionResult Index()
        {
            return View();
        }


        //
        // need to complete logic to put date updated from db
        //

        
        public ActionResult sitemap_index()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();

            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            return View(sitemap_ct_list);
        }



        
        public ActionResult website_en_sitemap()
        {
            Response.ContentType = "application/xml";



            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("website_en_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View();
        }

        
        public ActionResult website_ru_sitemap()
        {
            Response.ContentType = "application/xml";


            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("website_ru_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }


            return View();
        }

        
        public ActionResult website_es_sitemap()
        {
            Response.ContentType = "application/xml";


            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("website_es_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }


            return View();
        }



        
        public ActionResult news_sitemap()
        {
            Response.ContentType = "application/xml";

            //string hyp_sitemap = "";

            hypster_tv_DAL.newsManagement newsManagement = new hypster_tv_DAL.newsManagement();

            List<hypster_tv_DAL.newsPost> news_posts = new List<hypster_tv_DAL.newsPost>();
            news_posts = newsManagement.GetAllNews();


            return View(news_posts);
        }



        
        public ActionResult manuals_sitemap()
        {
            Response.ContentType = "application/xml";

            string hyp_sitemap = "";

            hypster_tv_DAL.manualManagement manualManagement = new hypster_tv_DAL.manualManagement();

            List<hypster_tv_DAL.Manual> manuals_list = new List<hypster_tv_DAL.Manual>();
            manuals_list = manualManagement.GetActiveManuals();




            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("manuals_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }




            foreach (var item in manuals_list)
            {
                string item_str = "";

                item_str += "<url><loc>http://hypster.com/resources/manuals/details/" + item.Manual_Guid + "</loc><lastmod>" + ViewBag.ModDate + "</lastmod><changefreq>weekly</changefreq><priority>0.40</priority></url>" + System.Environment.NewLine;

                hyp_sitemap += item_str;
            }

            ViewBag.SiteMap_STR = hyp_sitemap;

            return View();
        }




        
        public ActionResult charts_sitemap()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.chartsManager chartManagement = new hypster_tv_DAL.chartsManager();

            List<hypster_tv_DAL.Chart> charts_list = new List<hypster_tv_DAL.Chart>();
            charts_list = chartManagement.GetAllCharts();



            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("charts_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(charts_list);
        }



        
        public ActionResult festivals_sitemap()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.FestivalManager festivalManagement = new hypster_tv_DAL.FestivalManager();

            List<hypster_tv_DAL.Festival> festivals_list = new List<hypster_tv_DAL.Festival>();
            festivals_list = festivalManagement.GetAllFestivals();



            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("festivals_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }


            return View(festivals_list);
        }



        
        public ActionResult most_liked_playlists_sitemap(int id)
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> model = new List<hypster_tv_DAL.Playlist>();

            model = playlistManager.GetMostLikedPlaylistsAdmin();

            ViewBag.Start_Pos = id * 5000 - 5000;
            ViewBag.Pos_Count = 5000;




            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("most_liked_playlists_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(model);
        }


        
        public ActionResult most_viewed_playlists_sitemap(int id)
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> model = new List<hypster_tv_DAL.Playlist>();

            model = playlistManager.GetMostViewedPlaylistsAdmin();

            ViewBag.Start_Pos = id * 5000 - 5000;
            ViewBag.Pos_Count = 5000;



            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("most_viewed_playlists_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(model);
        }


        
        public ActionResult desc_playlists_sitemap(int id)
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> model = new List<hypster_tv_DAL.Playlist>();

            model = playlistManager.GetWithDescPlaylists();

            ViewBag.Start_Pos = id * 5000 - 5000;
            ViewBag.Pos_Count = 5000;




            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("desc_playlists_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }




            return View(model);
        }





        
        public ActionResult radio_station_sitemap()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();

            List<hypster_tv_DAL.MusicGenre> genres_list = new List<hypster_tv_DAL.MusicGenre>();
            genres_list = genreManager.GetMusicGenresList();



            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("radio_station_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(genres_list);
        }

        

        public ActionResult featured_sitemap()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.FeaturedPlaylistManagement fp_manager = new hypster_tv_DAL.FeaturedPlaylistManagement();

            List<hypster_tv_DAL.FeaturedPlaylist_Result> fp_list = new List<hypster_tv_DAL.FeaturedPlaylist_Result>();
            fp_list = fp_manager.ReturnFeaturedPlaylists();




            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach (var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("featured_sitemap"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(fp_list);
        }





        public ActionResult artist_directory()
        {
            Response.ContentType = "application/xml";

            hypster_tv_DAL.artistManagement artist_manager = new hypster_tv_DAL.artistManagement();


            List<hypster_tv_DAL.ArtistAZ> art_list = new List<hypster_tv_DAL.ArtistAZ>();
            art_list = artist_manager.GetArtistsAZList();




            hypster_tv_DAL.SitemapManagement manager = new hypster_tv_DAL.SitemapManagement();
            List<hypster_tv_DAL.Sitemap_Content> sitemap_ct_list = new List<hypster_tv_DAL.Sitemap_Content>();
            sitemap_ct_list = manager.GetSitemapContent();

            string mod_date = "";
            foreach(var item in sitemap_ct_list)
            {
                if (item.Sitemap_URL.Contains("artist_directory"))
                {
                    DateTime dt = (DateTime)item.Sitemap_Updated;
                    mod_date = dt.ToString("s") + "+00:00";
                }
            }

            if (mod_date != "")
            {
                ViewBag.ModDate = mod_date;
            }
            else
            {
                ViewBag.ModDate = System.Configuration.ConfigurationManager.AppSettings["sitemap_update_date"];
            }



            return View(art_list);
        }





    }
}
