using System.Web.Mvc;
using Planner.Models;

namespace Planner.Controllers
{
    public class NodesController : Controller
    {
        // GET: Nodes
        public ActionResult Index()
        {
            ViewBag.Nodes = Nodes.GetAllNodes((long) Session["userId"]);
            return View();
        }
    }
}