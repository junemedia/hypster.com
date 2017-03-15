using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.apps.Controllers
{
    public class appBreakingController : Controller
    {
        //
        // GET: /breaking/

        protected int POSTS_NUM_PAGING = 8;




        [System.Web.Mvc.OutputCache(Duration = 16)]
        public ActionResult Index()
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNews_cache();



            ViewBag.CurrPage_Start = 0;
            ViewBag.CurrPage_End = POSTS_NUM_PAGING;
            ViewBag.CurrPage = 1;

            ViewBag.NumOfPosts = posts_list.Count;
            double tmp_numPages = (double)ViewBag.NumOfPosts / (double)POSTS_NUM_PAGING;
            if ((tmp_numPages - (int)tmp_numPages) > 0)
                tmp_numPages++;
            ViewBag.NumOfPages = (int)tmp_numPages;


            return View(posts_list);
        }






        [System.Web.Mvc.OutputCache(Duration = 16)]
        public ActionResult page(int id)
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNews_cache();



            //Page ID
            int page_id = id;

            int _start = ((page_id * POSTS_NUM_PAGING) - POSTS_NUM_PAGING);
            int _end = _start + POSTS_NUM_PAGING;

            ViewBag.CurrPage_Start = _start;
            ViewBag.CurrPage_End = _end;
            ViewBag.CurrPage = page_id;


            ViewBag.NumOfPosts = posts_list.Count;
            double tmp_numPages = (double)ViewBag.NumOfPosts / (double)POSTS_NUM_PAGING;
            if ((tmp_numPages - (int)tmp_numPages) > 0)
                tmp_numPages++;
            ViewBag.NumOfPages = (int)tmp_numPages;



            return View("Index", posts_list);
        }










        [OutputCache(Duration = 360)]
        public ActionResult GetPopularNewsTags_PR()
        {
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();
            List<hypster_tv_DAL.sp_Tag_GetPopularNewsTags_Result> popular_news_tags_list = new List<hypster_tv_DAL.sp_Tag_GetPopularNewsTags_Result>();
            popular_news_tags_list = tagManager.GetPopularNewsTags();
            return View(popular_news_tags_list);
        }




        // modification
        //
        [OutputCache(Duration = 12)]
        public ActionResult GetBreakingHomeWidget_PR()
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNews_cache();
            return View(posts_list);
        }




        [OutputCache(Duration = 12)]
        public ActionResult GetBreakingGenre(string genre)
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNewsOnGenre_cache(genre).ToList();
            ViewBag.Title = genre;
            ViewBag.ID = newsManager.GetGenreIdByLabel(genre)[0];
            ViewBag.Length = posts_list.Count;
            //ViewBag.Count = posts_list.Count;            
            return View(posts_list);
        }






        //[OutputCache(Duration = 120)]
        public ActionResult Tags(string id)
        {
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();
            hypster_tv_DAL.Tag curr_tag = new hypster_tv_DAL.Tag();
            curr_tag = tagManager.GetTagByName(id.Replace("+", " "));
            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = tagManager.GetNewsByTagId(curr_tag.Tag_ID);
            return View(posts_list);
        }






        //[OutputCache(Duration = 120)]
        public ActionResult Details(string id)
        {
            string post_guid = "";
            post_guid = id;


            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();


            hypster_tv_DAL.newsPost post = new hypster_tv_DAL.newsPost();
            post = newsManager.GetPostByGUID(post_guid);


            if (post.post_id < Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["breaking_new_id"]))
            {
                return View("DetailsO", post);
            }

            return View(post);
        }





        [OutputCache(Duration = 120)]
        public ActionResult pr_GetRelatedNews(int? id)
        {
            int post_id = (int)id;


            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetRelatedNews_cache(post_id);



            return View(posts_list);
        }











        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Redirect to display Prev Post
        /// </summary>
        /// <returns></returns>
        public ActionResult prevPost()
        {
            if (Request.QueryString["PID"] != null)
            {
                int post_ID = 0;
                if (!Int32.TryParse(Request.QueryString["PID"], out post_ID))
                    post_ID = 0;

                if (post_ID != 0)
                {
                    hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
                    List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
                    posts_list = newsManager.GetLatestNews_cache();

                    for (int i = 1; i < posts_list.Count; i++)
                    {
                        if (posts_list[i].post_id == post_ID)
                        {
                            //return RedirectToAction("getPost", "post", new { post_guid = posts_list[i - 1].post_guid });
                            return RedirectPermanent("/apps/appBreaking/details/" + posts_list[i - 1].post_guid);
                        }
                    }
                }

            }


            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/apps/appBreaking");
        }






        /// <summary>
        /// Redirect to display Next Post
        /// </summary>
        /// <returns></returns>
        public ActionResult nextPost()
        {
            if (Request.QueryString["PID"] != null)
            {
                int post_ID = 0;
                if (!Int32.TryParse(Request.QueryString["PID"], out post_ID))
                    post_ID = 0;


                if (post_ID != 0)
                {
                    hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
                    List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
                    posts_list = newsManager.GetLatestNews_cache();

                    for (int i = 0; i < posts_list.Count - 1; i++)
                    {
                        if (posts_list[i].post_id == post_ID)
                        {
                            //return RedirectToAction("getPost", "post", new { post_guid = posts_list[i + 1].post_guid });
                            return RedirectPermanent("/apps/appBreaking/details/" + posts_list[i + 1].post_guid);
                        }
                    }
                }

            }


            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/apps/appBreaking");
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++











        [OutputCache(Duration = 12)]
        public ActionResult GetBreakingPlayerWidget_PR()
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();


            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNews_cache();


            return View(posts_list);
        }







    }
}
