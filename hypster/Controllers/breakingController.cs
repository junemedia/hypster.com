using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class breakingController : Controller
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



        [OutputCache(Duration = 50)]
        public ActionResult GetBreakingGenre(string genre, string title)
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            if (genre.Contains(";"))
            {
                string last_genre = "";
                string[] genres = genre.Split(';');
                foreach (var g in genres)
                {
                    List<hypster_tv_DAL.newsPost> list = newsManager.GetLatestNewsOnGenre_cache(g).ToList();
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (!posts_list.Contains(list[j]))
                            posts_list.Add(list[j]);
                    }
                    last_genre = g;
                }
                posts_list = posts_list.OrderByDescending(a => a.post_date).ToList();
                ViewBag.Title = title;
                ViewBag.ID = newsManager.GetGenreIdByLabel(last_genre)[0];
            }
            else
            {
                posts_list = newsManager.GetLatestNewsOnGenre_cache(genre).ToList();
                ViewBag.Title = genre;
                ViewBag.ID = newsManager.GetGenreIdByLabel(genre)[0];
            }

            ViewBag.Length = posts_list.Count;
            ViewBag.Width = 235 * posts_list.Count;
            return View(posts_list);
        }


        //[OutputCache(Duration = 60)]
        public ActionResult Tags(string id)
        {
            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();


            hypster_tv_DAL.Tag curr_tag = new hypster_tv_DAL.Tag();
            curr_tag = tagManager.GetTagByName(id.Replace("+"," "));


            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = tagManager.GetNewsByTagId(curr_tag.Tag_ID);


            return View(posts_list);
        }


        





        [OutputCache(Duration = 60)]
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





        //------------------------------------------------------------------------------------------
        public ActionResult getNewsTags(int? id)
        {
            int post_id = (int)id;

            hypster_tv_DAL.TagManagement tagManager = new hypster_tv_DAL.TagManagement();

            List<hypster_tv_DAL.sp_Tag_GetNewsTags_Result> news_tags = new List<hypster_tv_DAL.sp_Tag_GetNewsTags_Result>();
            news_tags = tagManager.GetNewsTags(post_id);

            return View(news_tags);
        }
        //------------------------------------------------------------------------------------------





        //------------------------------------------------------------------------------------------
        [OutputCache(Duration = 120)]
        public ActionResult pr_GetRelatedNews(int? id)
        {
            int post_id = (int)id;

            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetRelatedNews_cache(post_id);

            return View(posts_list);
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        [OutputCache(Duration = 120)]
        public ActionResult pr_GetLatestNews()
        {
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            List<hypster_tv_DAL.newsPost> posts_list = new List<hypster_tv_DAL.newsPost>();
            posts_list = newsManager.GetLatestNews(3);
            return View(posts_list);
        }
        //------------------------------------------------------------------------------------------


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
                            return RedirectPermanent("/breaking/details/" + posts_list[i - 1].post_guid);
                        }
                    }
                }

            }
            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/breaking");
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
                            return RedirectPermanent("/breaking/details/" + posts_list[i + 1].post_guid);
                        }
                    }
                }

            }


            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/breaking");
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
