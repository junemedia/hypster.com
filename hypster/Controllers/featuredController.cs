using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class featuredController : Controller
    {
        //
        // GET: /featured/



        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult Index()
        {
            hypster.ViewModels.playlistsViewModel model = new ViewModels.playlistsViewModel();


            hypster_tv_DAL.FeaturedPlaylistManagement fplst_manager = new hypster_tv_DAL.FeaturedPlaylistManagement();
            model.featured_playlists = fplst_manager.ReturnFeaturedPlaylists();



            return View(model);
        }





        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult GetPopularFeaturedTags()
        {
            hypster_tv_DAL.TagManagement tags_manager = new hypster_tv_DAL.TagManagement();

            List<hypster_tv_DAL.Tag> tags_list = new List<hypster_tv_DAL.Tag>();

            tags_list = tags_manager.GetPopularTags();



            return View(tags_list);
        }









        [OutputCache(Duration = 100)]
        public ActionResult GetSeasonalPlaylists()
        {
            hypster_tv_DAL.featuredContentManagement fc_manager = new hypster_tv_DAL.featuredContentManagement();


            List<hypster_tv_DAL.FeaturedContent> model = new List<hypster_tv_DAL.FeaturedContent>();
            model = fc_manager.ReturnFeaturedContent((int)hypster_tv_DAL.fc_Type.Seasonal_Playlists);

            
            return View(model);
        }





        [OutputCache(Duration = 100)]
        public ActionResult GetArtistPlaylists()
        {
            hypster_tv_DAL.featuredContentManagement fc_manager = new hypster_tv_DAL.featuredContentManagement();


            List<hypster_tv_DAL.FeaturedContent> model = new List<hypster_tv_DAL.FeaturedContent>();
            model = fc_manager.ReturnFeaturedContent((int)hypster_tv_DAL.fc_Type.Artist_Playlists);



            return View(model);
        }





        [OutputCache(Duration = 100)]
        public ActionResult OtherPlaylists()
        {
            hypster_tv_DAL.featuredContentManagement fc_manager = new hypster_tv_DAL.featuredContentManagement();


            List<hypster_tv_DAL.FeaturedContent> model = new List<hypster_tv_DAL.FeaturedContent>();
            model = fc_manager.ReturnFeaturedContent((int)hypster_tv_DAL.fc_Type.Other);



            return View(model);
        }



    }
}
