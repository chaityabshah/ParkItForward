using System.Web.Mvc;
using Planner.ActionFilters;
using Planner.Models;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    [AuthorizationFilter]
    [HandleError]
    public class ClassesController : Controller
    {
        public ActionResult Index()
        {
            return View(Classes.GetAll((long) Session["UserId"]));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ClassesForm @class)
        {
            Classes.Create((long) Session["UserId"], @class);
            Session["taskAction"] = "created";
            return RedirectToAction("Index", "Classes");
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            return View(Classes.Get((long) Session["UserId"], id));
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(Classes.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Update(ClassesForm @class)
        {
            Classes.Update((long) Session["UserId"], @class);
            Session["taskAction"] = "updated";
            return RedirectToAction("Index", "Classes");
        }

        [HttpGet]
        public ActionResult GetEvents()
        {
            var events = Classes.GetEvents((long) Session["UserId"]);
            ViewData["events"] = events;
            return View();
        }

        [HttpGet]
        public ActionResult Remove(long id)
        {
            return View(Classes.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Delete(ClassesForm @class)
        {
            Classes.Delete((long) Session["UserId"], @class);
            Session["taskAction"] = "deleted";
            return RedirectToAction("Index", "Classes");
        }
    }
}