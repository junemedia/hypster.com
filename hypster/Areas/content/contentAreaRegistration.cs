using System.Web.Mvc;

namespace hypster.Areas.content
{
    public class contentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "content";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "content_default",
                "content/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
