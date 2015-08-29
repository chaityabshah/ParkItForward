using System.Web.Mvc;
using Planner.Models;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["EmailAddress"] != null)
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.EmailAddress = Session["EmailAddress"] as string;
                return View();
                //return RedirectToAction("Secured");
            }
            if (Session["LoginFailed"] != null)
            {
                ViewBag.LoginFailed = true;
            }
            ViewBag.IsLoggedIn = false;
            return View();
        }

        public ActionResult Secured()
        {
            var tasks = Tasks.GetAll((long) Session["userId"]);
            return View(tasks);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Features()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}