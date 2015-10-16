using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class tagsController : Controller
    {
        //
        // GET: /tags/

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            
            hypster_tv_DAL.TagManagement tags_manager = new hypster_tv_DAL.TagManagement();

            List<hypster_tv_DAL.Tag> tags_list = new List<hypster_tv_DAL.Tag>();

            tags_list = tags_manager.GetPopularTags();



            return View(tags_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        [OutputCache(Duration = 30)]
        public ActionResult getTagContent(string tag_name)
        {
            hypster.ViewModels.TagsLangingViewModel model = new hypster.ViewModels.TagsLangingViewModel();
            hypster_tv_DAL.TagManagement tags_manager = new hypster_tv_DAL.TagManagement();



            string search_tag_name = "";
            search_tag_name = tag_name.Replace('+', ' ');
            ViewBag.Tag_Name = search_tag_name;
            


            


            // get popular playlists
            model.tags_list = tags_manager.GetPopularTags();


            if (tag_name != null && tag_name != "")
            {

                //get tag by name
                model.tag = tags_manager.GetTagByName(search_tag_name);


                //get playlists by term
                if (model.tag.Tag_ID != 0)
                {
                    model.playlists_list = tags_manager.GetPlaylistsByTagId(model.tag.Tag_ID);

                    model.posts_list = tags_manager.GetNewsByTagId(model.tag.Tag_ID);


                    //increment popular tag
                    tags_manager.IncrementPopularTag(model.tag.Tag_ID);
                }

            }

           

            

            return View("Index", model);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





    }
}
