using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Planner.Controllers
{
    /// <summary>
    /// Dummy controller that results in authorized exception being thrown
    /// </summary>
    public class ElmahController : Controller
    {
        // GET: Elmah
        public ActionResult Index()
        {
            throw new UnauthorizedAccessException();
        }
    }
}