using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace hypster.Controllers
{
    //
    // controller for music search //ControllerBase
    public class searchController : ControllerBase
    {

        private int MAX_RECENT_SEARCHES_NUM = 255;







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // perform youtube search
        public ActionResult Music()
        {

            if (Request.QueryString["ss"] != null)
            {
                //---------------------------------------------------------------------
                string search_string = Request.QueryString["ss"].ToString();
                ViewBag.search_string = search_string.Replace(" ", "+");
                //---------------------------------------------------------------------



                //---------------------------------------------------------------------
                string Curr_Page = "";
                if (Request.QueryString["page"] != null)
                {
                    //if (Int32.TryParse(Request.QueryString["page"], out Curr_Page) == false)
                    //    Curr_Page = 1;
                    Curr_Page = Request.QueryString["page"];
                }
                //ViewBag.CurrPage = Curr_Page;
                //---------------------------------------------------------------------



                // "orderBy"
                //---------------------------------------------------------------------
                string orderBy = "";
                if (Request.QueryString["orderBy"] != null)
                {
                    orderBy = Request.QueryString["orderBy"].ToString();
                }
                ViewBag.orderBy = orderBy;
                //---------------------------------------------------------------------





                //---------------------------------------------------------------------
                #region save_recent_searches_to_application-varibles
                //save recent searches to application varibles
                if (HttpContext.Application["RECENT_SEARCHES"] != null)
                {
                    List<string> recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];
                    recent_searches.Add(search_string);
                    if (recent_searches.Count > MAX_RECENT_SEARCHES_NUM)
                        recent_searches.RemoveAt(recent_searches.Count - 1);

                    HttpContext.Application["RECENT_SEARCHES"] = recent_searches;
                }
                else
                {
                    List<string> recent_searches = new List<string>();
                    recent_searches.Add(search_string);

                    HttpContext.Application["RECENT_SEARCHES"] = recent_searches;
                }

                if (HttpContext.Application["RECENT_SEARCHES"] != null)
                    ViewBag.recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];
                #endregion
                //---------------------------------------------------------------------
                ;


                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {

                    ApiKey = System.Configuration.ConfigurationManager.AppSettings["YouTubeAPIKEY"],
                    ApplicationName = System.Configuration.ConfigurationManager.AppSettings["YouTubeAPIKEYName"]

                   });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = search_string; // Replace with your search term.
                if (Curr_Page != "")
                {
                    searchListRequest.PageToken = Curr_Page;
                }
                searchListRequest.MaxResults = 25;


                var searchListResponse = searchListRequest.Execute();

                List<SearchResult> videos = new List<SearchResult>();

                foreach (var searchResult in searchListResponse.Items)
                {
                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            videos.Add(searchResult);
                            break;
                    }
                }

                ViewBag.TotalResults = searchListResponse.PageInfo.TotalResults;
                ViewBag.PageSize = searchListResponse.PageInfo.ResultsPerPage;


                ViewBag.NextPageToken = searchListResponse.NextPageToken;
                ViewBag.PrevPageToken = searchListResponse.PrevPageToken;

                return View(videos);




            } // NEED TO CHECK AND FIX THIS SECTION
            else
            {
                if (HttpContext.Application["RECENT_SEARCHES"] != null)
                    ViewBag.recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];

                return View();
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // perform youtube search
        public ActionResult MusicYTID()
        {
            string search_string = "";
            if (Request.QueryString["ss"] != null)
            {
                //---------------------------------------------------------------------
                search_string = Request.QueryString["ss"].ToString();
                ViewBag.search_string = search_string.Replace(" ", "+");
                //---------------------------------------------------------------------
            }


            // 2.get video dynamic details
            //-----------------------------------------------------------------------------------------------------
            //Video video = null;
            try
            {

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = System.Configuration.ConfigurationManager.AppSettings["YouTubeAPIKEY"],
                    ApplicationName = System.Configuration.ConfigurationManager.AppSettings["YouTubeAPIKEYName"]
                });

                var searchListRequest = youtubeService.Search.List("id,snippet");
                searchListRequest.Q = search_string; // Replace with your search term.
                searchListRequest.MaxResults = 1;

                var searchListResponse = searchListRequest.Execute();
                List<SearchResult> videos = new List<SearchResult>();
                foreach (var searchResult in searchListResponse.Items)
                {
                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            videos.Add(searchResult);
                            break;
                    }
                }

            
                return View(videos);

            }
            catch (Exception ex) { }
            //-----------------------------------------------------------------------------------------------------


            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 30)]
        public ActionResult SearchInPlaylists(string id)
        {
            List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();

            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            string search_string = id.Replace("+", " ");
            ViewBag.search_string = search_string;

            if (!search_string.Contains("-") && search_string.Length < 20 && search_string.Length > 3)
            {
                playlists_list = playlistManager.SearchPlaylistByDesc(search_string + ",");
            }


            return View(playlists_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult SA(string id)
        {
            hypster_tv_DAL.artistManagement artistManager = new hypster_tv_DAL.artistManagement();
            List<hypster_tv_DAL.ArtistCategory> list_arr = new List<hypster_tv_DAL.ArtistCategory>();


            try
            {

                System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
                if (i_chache["SA_" + id] != null)
                {
                    list_arr = (List<hypster_tv_DAL.ArtistCategory>)i_chache["SA_" + id];
                }
                else
                {
                    list_arr = artistManager.SearchForArtist(id);
                    i_chache.Add("SA_" + id, list_arr, DateTime.Now.AddMinutes(12));
                }

            }
            catch (Exception ex)
            {
            }


            return View(list_arr);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult breakingTags(string id)
        {
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();


            List<hypster_tv_DAL.Tag> breaking_tags_list = new List<hypster_tv_DAL.Tag>();
            breaking_tags_list = tagManager.SearchTags(id);


            return View(breaking_tags_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult breakingTagsMob(string id)
        {
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();


            List<hypster_tv_DAL.Tag> breaking_tags_list = new List<hypster_tv_DAL.Tag>();
            breaking_tags_list = tagManager.SearchTags(id);


            return View(breaking_tags_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    }
}
