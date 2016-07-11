using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.content.Controllers
{
    public class HypNewsController : Controller
    {
        //
        // GET: /content/news/

        protected int POSTS_NUM_PAGING = 5;



        /// <summary>
        /// this is paging functionality
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult Index()
        {
            /*
            hypster_tv.ViewModels.HomePageViewModel model = new hypster_tv.ViewModels.HomePageViewModel();


            ViewBag.CurrPage_Start = 0;
            ViewBag.CurrPage_End = POSTS_NUM_PAGING;
            

            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            model.posts_list = newsManager.GetLatestNews_cache();



            model.NumOfPosts = model.posts_list.Count;
            double tmp_numPages = (double)model.NumOfPosts / (double)POSTS_NUM_PAGING;
            if ((tmp_numPages - (int)tmp_numPages) > 0)
                tmp_numPages++;
            model.NumOfPages = (int)tmp_numPages;


            return View(model);
            */

            return RedirectPermanent("/breaking");
        }



        /// <summary>
        /// this is paging functionality
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult page(int id)
        {
            /*
            hypster_tv.ViewModels.HomePageViewModel model = new hypster_tv.ViewModels.HomePageViewModel();

            //Page ID
            int page_id = id;


            int _start = ((page_id * POSTS_NUM_PAGING) - POSTS_NUM_PAGING);
            int _end = _start + POSTS_NUM_PAGING;

            ViewBag.CurrPage_Start = _start;
            ViewBag.CurrPage_End = _end;
            ViewBag.CurrPage = page_id;


            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            model.posts_list = newsManager.GetLatestNews_cache();



            model.NumOfPosts = model.posts_list.Count;
            double tmp_numPages = (double)model.NumOfPosts / (double)POSTS_NUM_PAGING;
            if ((tmp_numPages - (int)tmp_numPages) > 0)
                tmp_numPages++;
            model.NumOfPages = (int)tmp_numPages;

            return View("Index", model);
            */



            return RedirectPermanent("/breaking/" + id);
        }








        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult Post(string id)
        {
            /*
            hypster_tv.ViewModels.getPostViewModel viewModel = new hypster_tv.ViewModels.getPostViewModel();
            string post_guid = id;
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            viewModel.post = newsManager.GetPostByGUID(post_guid);
            viewModel.comments_list = newsManager.GetPostComments_cache(viewModel.post.post_id);
            return View(viewModel);
            */

            return RedirectPermanent("/breaking/details/" + id);
        }



        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult PostPreview(string id)
        {
            hypster_tv.ViewModels.getPostViewModel viewModel = new hypster_tv.ViewModels.getPostViewModel();

            string post_guid = id;



            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

            viewModel.post = newsManager.GetPostByGUID(post_guid);
            viewModel.comments_list = newsManager.GetPostComments_cache(viewModel.post.post_id);

            return View(viewModel);
        }










        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        [HttpPost]
        public ActionResult SubmitPostComment(hypster_tv_DAL.newsComment p_comment, string post_guid)
        {
            /*
            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();


            //---------------------------------------------------------------------------------------
            p_comment.comment = System.Text.RegularExpressions.Regex.Replace(p_comment.comment, @"<(.|\n)*?>", string.Empty);

            p_comment.ipAddress = IpAddress();
            p_comment.status = (int)hypster_tv_DAL.newsCommentStatus.NoActive;
            p_comment.user_ID = memberManager.getMemberByUserName(User.Identity.Name).id;
            p_comment.userName = User.Identity.Name;
            p_comment.postDate = DateTime.Now;


            hyDB.newsComments.AddObject(p_comment);
            hyDB.SaveChanges();
            //---------------------------------------------------------------------------------------




            //need to reset data cache (if exist)
            // this will allow to show new comment
            //---------------------------------------------------------------------------------------
            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["NewsComment_For_Tv_" + p_comment.newsPost_ID] != null)
                i_chache.Remove("NewsComment_For_Tv_" + p_comment.newsPost_ID);
            //---------------------------------------------------------------------------------------




            //return RedirectToAction("Post", "HypNews", new { @post_guid = post_guid });
            return RedirectPermanent("/content/HypNews/post/" + post_guid);
            */

            return RedirectPermanent("/breaking");
        }



        /// <summary>
        /// Service function - gives user ip address
        /// </summary>
        /// <returns></returns>
        private string IpAddress()
        {
            string strIpAddress;
            strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null) strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            return strIpAddress;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++









        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>
        /// Redirect to display Prev Post
        /// </summary>
        /// <returns></returns>
        public ActionResult prevPost()
        {
            /*
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
                            return RedirectPermanent("/content/HypNews/post/" + posts_list[i - 1].post_guid);
                        }
                    }
                }

            }


            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/content/HypNews");
            */

            return RedirectPermanent("/breaking");
        }



        /// <summary>
        /// Redirect to display Next Post
        /// </summary>
        /// <returns></returns>
        public ActionResult nextPost()
        {
            /*
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
                            return RedirectPermanent("/content/HypNews/post/" + posts_list[i + 1].post_guid);
                        }
                    }
                }

            }


            //return to news home if no next post
            //return RedirectToAction("Index", "home");
            return RedirectPermanent("/content/HypNews");
            */

            return RedirectPermanent("/breaking");
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++













        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [System.Web.Mvc.OutputCache(Duration = 160)]
        public ActionResult Featured()
        {
            hypster_tv.ViewModels.HomePageViewModel model = new hypster_tv.ViewModels.HomePageViewModel();


            ViewBag.CurrPage_Start = 0;
            ViewBag.CurrPage_End = POSTS_NUM_PAGING;


            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            model.posts_list = newsManager.GetLatestNews_cache();


            return View(model);
        }


        public ActionResult Bad_Diver_Bill()
        {
            return View();
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    }
}
