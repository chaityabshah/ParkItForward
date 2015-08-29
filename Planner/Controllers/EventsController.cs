using System.Net;
using System.Web.Mvc;
using Planner.ActionFilters;
using Planner.Models;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    [AuthorizationFilter]
    [HandleError]
    public class EventsController : Controller
    {
        // GET: Tasks
        public ActionResult Index()
        {
            return View(Events.GetAll((long) Session["UserId"]));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventForm userEvent)
        {
            Events.Create((long) Session["UserId"], userEvent);
            return RedirectToAction("Index", "Events");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            return View(Events.Get((long) Session["UserId"], id));
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            return View(Events.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Update(EventForm userEvent)
        {
            Events.Update((long) Session["UserId"], userEvent);
            return RedirectToAction("Index", "Events");
        }

        public HttpStatusCodeResult Ajax(Xedit @event) //HttpStatusCodeResult
        {
            Events.AJAX((long) Session["userId"], @event);
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        [HttpGet]
        public ActionResult Remove(long id)
        {
            return View(Events.Get((long) Session["UserId"], id));
        }

        [HttpPost]
        public ActionResult Delete(EventForm userEvent)
        {
            Events.Delete((long) Session["UserId"], userEvent);
            return RedirectToAction("Index", "Events");
        }

        [HttpGet]
        public ActionResult Calendar()
        {
            var userId = (long) Session["UserId"];
            return View(Tasks.GetAll(userId));
        }
    }
}