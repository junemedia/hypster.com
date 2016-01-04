using System.Web.Mvc;

namespace hypster.Areas.senses
{
    public class sensesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "senses";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "senses_default",
                "senses/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
