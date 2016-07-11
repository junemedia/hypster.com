using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace hypster.Controllers
{
    public class connectController : ControllerBase
    {

        //
        // GET: /connect/
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();




            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();


            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["Connect_Members"] != null)
            {
                members_list_tmp = (List<hypster_tv_DAL.Member>)i_chache["Connect_Members"];
            }
            else
            {
                members_list_tmp = memManager.GetMembersRandom(8000);
                i_chache.Add("Connect_Members", members_list_tmp, DateTime.Now.AddSeconds(1500)); //15 mins
            }
            




            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if(i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }


            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ((members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;


            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);




            return View(model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 60)] 
        public ActionResult page(int id)
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();
            model.Current_Page = id;




            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();


            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["Connect_Members"] != null)
            {
                members_list_tmp = (List<hypster_tv_DAL.Member>)i_chache["Connect_Members"];
            }
            else
            {
                members_list_tmp = memManager.GetMembersRandom(8000);
                i_chache.Add("Connect_Members", members_list_tmp, DateTime.Now.AddSeconds(1500)); //15 mins
            }





            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }




            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ( (members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;



            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);

            return View("Index", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++








        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 100)]
        public ActionResult GetGenresBar()
        {
            List<hypster_tv_DAL.MusicGenre> genres_list = new List<hypster_tv_DAL.MusicGenre>();

            // get music genres
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            genres_list = genreManager.GetMusicGenresList();


            return View(genres_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult search()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [HttpPost]
        public ActionResult search(string SearchFor, string serUserPar)
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();
            model.Current_Page = 1;

            ViewBag.SearchFor = SearchFor;
            ViewBag.SearchString = serUserPar;



            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();



            switch (SearchFor)
            {
                case "serUserName":
                    members_list_tmp.Add(memManager.getMemberByUserName(serUserPar));
                    break;
                case "serUserEmail":
                    members_list_tmp.Add(memManager.getMemberByEmail(serUserPar));
                    break;
                case "serUserID":
                    int iUserID = -1;
                    Int32.TryParse(serUserPar, out iUserID);
                    if (iUserID != -1)
                        members_list_tmp.Add(memManager.getMemberByID(iUserID));
                    break;
                default:
                    break;
            }





            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }




            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ((members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;


            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);

            return View("search", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++












        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 60)]
        public ActionResult searchGenre(int id)
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();
            model.Current_Page = 1;
            ViewBag.Genre_ID = id;



            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();
            members_list_tmp = memManager.GetMembersWithGenre(id);





            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }





            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ((members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;


            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);

            return View("searchGenre", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++






        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 60)]
        public ActionResult GenrePage(int id)
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();
            model.Current_Page = id;



            int Genre_id = 0;
            if (Request.QueryString["Genre_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["Genre_ID"].ToString(), out  Genre_id);

                ViewBag.Genre_id = id;
            }



            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();

            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();
            members_list_tmp = memManager.GetMembersWithGenre(Genre_id);





            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }





            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ((members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;

            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);



            return View("searchGenre", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++













    }
}
