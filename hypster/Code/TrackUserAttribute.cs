using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Code
{
    public class TrackUserAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.HttpContext.Trace.Write(" Log Action Executing " + filterContext.ActionDescriptor.ActionName);


            //hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            //hypster_tv_DAL.HypException exp = new hypster_tv_DAL.HypException();
            //exp.ExcMethod = filterContext.ActionDescriptor.ActionName;
            //hyDB.HypExceptions.AddObject(exp);
            //hyDB.SaveChanges();


            base.OnActionExecuting(filterContext);
        }



        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.HttpContext.Trace.Write(" Log Action Executed " + filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuted(filterContext);
        }



    }
}