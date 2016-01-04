using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.black.Controllers
{
    [AuthorizeBlack]
    public class srRadioController : Controller
    {
        //
        // GET: /black/srRadio/

        public PartialViewResult Index()
        {
            List<hypster_tv_DAL.MusicGenre> model = new List<hypster_tv_DAL.MusicGenre>();


            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model = genreManager.GetMusicGenresList();


            return PartialView(model);
        }


    }
}
