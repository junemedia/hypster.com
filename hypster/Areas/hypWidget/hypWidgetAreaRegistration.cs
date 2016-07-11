using System.Web.Mvc;

namespace hypster.Areas.hypWidget
{
    public class hypWidgetAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "hypWidget";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "hypWidget_default",
                "hypWidget/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
