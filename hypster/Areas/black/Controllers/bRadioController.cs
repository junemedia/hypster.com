using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class bRadioController : Controller
    {
        //
        // GET: /black/bRadio/

        public PartialViewResult Index()
        {
            List<hypster_tv_DAL.MusicGenre> model = new List<hypster_tv_DAL.MusicGenre>();


            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model = genreManager.GetMusicGenresList();
            

            return PartialView(model);
        }

    }
}
