using System.Web.Mvc;
using System.Web.Routing;

namespace Planner.ActionFilters // NEW NEW NEW
{
    public class AuthorizationFilter : ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session["userId"] != null)
                return;

            //Redirect him to somewhere.

            var redirectTarget = new RouteValueDictionary
            {{"action", "Index"}, {"controller", "Home"}};
            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }
    }
}