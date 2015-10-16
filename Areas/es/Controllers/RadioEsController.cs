using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Areas.es.Controllers
{
    public class RadioEsController : Controller
    {
        //
        // GET: /es/RadioEs/



        public ActionResult pr_CreateRadioStation_POST(string RadioStationName, string RadioStationSearchTerm)
        {
            if (RadioStationName == " Nombre de Estación" || RadioStationSearchTerm == " Ingresa artista, canción o género" || RadioStationName == "" || RadioStationSearchTerm == "")
            {
                return RedirectPermanent("/es/crear/station?act=err");
            }



            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.RadioStation radioStation = new hypster_tv_DAL.RadioStation();
            radioStation.user_ID = curr_user.id;
            radioStation.RadioStationName = RadioStationName;
            radioStation.RadioStationQuery = RadioStationSearchTerm;

            radioStationManager.hyDB.AddToRadioStations(radioStation);
            radioStationManager.hyDB.SaveChanges();



            return RedirectPermanent("/es/Cuenta/music");
        }



    }
}
