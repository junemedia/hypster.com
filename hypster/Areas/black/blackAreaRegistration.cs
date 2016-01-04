using System.Web.Mvc;

namespace hypster.Areas.black
{
    public class blackAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "black";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "black_default",
                "black/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
