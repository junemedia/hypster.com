using System.Web.Mvc;

namespace hypster.Areas.es
{
    public class esAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "es";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "es_default",
                "es/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
