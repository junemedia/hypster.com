using System.Web.Mvc;

namespace hypster.Areas.hypAPI
{
    public class hypAPIAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "hypAPI";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "hypAPI_default",
                "hypAPI/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
