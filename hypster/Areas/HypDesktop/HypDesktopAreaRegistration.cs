using System.Web.Mvc;

namespace hypster.Areas.HypDesktop
{
    public class HypDesktopAreaRegistration : AreaRegistration
    {

        public override string AreaName
        {
            get
            {
                return "HypDesktop";
            }
        }


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HypDesktop_default",
                "HypDesktop/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}
