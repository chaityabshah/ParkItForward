using System.Net;
using System.Web.Mvc;
using Planner.ActionFilters;
using Planner.Models;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    [AuthorizationFilter]
    [HandleError]
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult Index()
        {
            ViewBag.EmailAddress = Session["EmailAddress"] as string;
            return View(Tasks.GetAll((long) Session["UserId"]));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskForm task)
        {
            Tasks.Create((long) Session["UserId"], task);
            Session["taskAction"] = "created";
            return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(Tasks.Get((long) Session["UserId"], id));
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            return View(Tasks.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Update(TaskForm task)
        {
            Tasks.Update((long) Session["UserId"], task);
            Session["taskAction"] = "updated";
            return RedirectToAction("Index", "Tasks");
        }

        //[HttpPost]
        public HttpStatusCodeResult Ajax(Xedit task) //HttpStatusCodeResult
        {
            Tasks.AJAX((long) Session["userId"], task);
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        [HttpGet]
        public ActionResult Remove(long id)
        {
            return View(Tasks.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Delete(TaskForm task)
        {
            Tasks.Delete((long) Session["UserId"], task);
            Session["taskAction"] = "deleted";
            return RedirectToAction("Index", "Tasks");
        }
    }
}