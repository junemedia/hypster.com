using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class flashController : Controller
    {
        //
        // GET: /flash/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult N(string id)
        {
            return RedirectPermanent("/");
        }

        // Send tracking information to the server before handling the player SWF file.
        private void sendGACode(string cd)
        {
            // Making a http web request object to the URL: http://www.google-analytics.com/collect by using POST method.
            var request = (HttpWebRequest)WebRequest.Create("http://www.google-analytics.com/collect");
            request.Method = "POST";

            // Pass the following dataset to the URL while requesting:
            // v(version)=1, tid(Tracking ID)=UA-99868480-2, cid(randomly generated Client ID), t(Event hit type)=pageview, dh(Document hostname), dp(Document Page), cd5(Custom Dimension).
            var postData = new Dictionary<string, string>
            {
                { "v", "1" },
                { "tid", "UA-99868480-2" },
                { "cid", Guid.NewGuid().ToString() },
                { "t", "pageview" },
                { "dh", Request.ServerVariables["REMOTE_HOST"].ToString() },
                { "dp", Request.ServerVariables["SCRIPT_NAME"].ToString() },
                //{ "ds", "app" },
                { "cd5", cd }, // Custom Dimension is defined here.
            };
            // Aggregate the postDataString:
            var postDataString = postData.Aggregate("", (data, next) => string.Format("{0}&{1}={2}", data, next.Key, HttpUtility.UrlEncode(next.Value))).TrimEnd('&');

            // set the Content-Length header to the correct value
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataString);

            // write the request body to the request
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(postDataString);
            }

            try
            {
                // Get Response:
                var webResponse = (HttpWebResponse)request.GetResponse();
                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    // Throws HttpException exception if the status code is NOT OK.
                    throw new HttpException((int)webResponse.StatusCode, "Google Analytics tracking did not return OK 200");
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
            }
        }

        // Management of the Classic player OR Radio player:
        [HttpGet]
        public ActionResult player(string qs)
        {
            string cd = "Classic";
            // Thess mean radio players:
            if (qs.Contains(":0:0") || qs.Contains(":0:1"))
            {
                cd = "Radio";
            }
            try
            {
                sendGACode(cd);
            }
            catch (Exception e) { }
            return Redirect("~/flash/player.swf?" + qs + "&tracked=1");
        }

        // Management of the Compact player:
        [HttpGet]
        public ActionResult player_n(string qs)
        {
            string cd = "Compact";
            try
            {
                sendGACode(cd);
            }
            catch (Exception e) { }
            return Redirect("~/flash_n/player.swf?" + qs + "&tracked=1");
        }
    }
}
