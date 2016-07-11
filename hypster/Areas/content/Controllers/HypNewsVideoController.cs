using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.content.Controllers
{
    public class HypNewsVideoController : Controller
    {
        //
        // GET: /content/HypNewsVideo/

        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult GetSideVideos()
        {
            hypster_tv_DAL.videoClipManager videoManager = new hypster_tv_DAL.videoClipManager();
            List<hypster_tv_DAL.videoClip> videos_list = videoManager.getTopRatedVideos();



            List<hypster_tv_DAL.videoClip> news_list_Display = new List<hypster_tv_DAL.videoClip>();

            int maxPostsNum = 3; // WILL NEED TO MOVE TO CONFIG
            if (videos_list.Count > maxPostsNum)
            {
                Random randomGen = new Random();
                do
                {
                    int next_article = randomGen.Next(0, videos_list.Count);

                    hypster_tv_DAL.videoClip item = new hypster_tv_DAL.videoClip();
                    item = videos_list[next_article];

                    if (!news_list_Display.Contains(item))
                        news_list_Display.Add(item);


                } while (news_list_Display.Count < maxPostsNum);
            }




            return View(news_list_Display);
        }

    }
}
