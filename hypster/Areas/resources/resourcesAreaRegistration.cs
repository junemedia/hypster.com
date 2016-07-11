using System.Web.Mvc;

namespace hypster.Areas.resources
{
    public class resourcesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "resources";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "resources_default",
                "resources/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
