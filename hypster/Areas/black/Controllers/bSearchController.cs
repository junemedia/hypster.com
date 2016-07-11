using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;


namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class bSearchController : Controller
    {
        //
        // GET: /black/bSearch/

        public ActionResult Index()
        {
            return View();
        }



        public PartialViewResult searchFor()
        {
            string search_string = "";
            int Curr_Page = 1;

            if (Request.QueryString["ss"] != null)
            {
                search_string = Request.QueryString["ss"].ToString();
            }


            YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
            YouTubeRequest request = new YouTubeRequest(settings);

            string orderBy = "viewCount";
            if (orderBy != "")
                orderBy = "&orderby=" + orderBy;


            string IP_Address;
            IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IP_Address == null)
                IP_Address = Request.ServerVariables["REMOTE_ADDR"];
            else
                IP_Address = "";


            string feedUrl = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&start-index={1}" + orderBy, HttpUtility.UrlEncode(search_string.Replace("+", " ")), (Curr_Page * 25) - 25 + 1);
            Feed<Video> videoFeed = null;


            try
            {
                videoFeed = request.Get<Video>(new Uri(feedUrl));
            }
            catch (Exception ex)
            {
            }
           


            return PartialView(videoFeed);
        }




    }
}
