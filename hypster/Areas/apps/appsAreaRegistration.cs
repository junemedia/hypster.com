using System.Web.Mvc;

namespace hypster.Areas.apps
{
    public class appsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "apps";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "apps_default",
                "apps/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
