using System.Web.Mvc;

namespace hypster.Areas.ru
{
    public class ruAreaRegistration : AreaRegistration
    {

        public override string AreaName
        {
            get
            {
                return "ru";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ru_default",
                "ru/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }


    }
}
