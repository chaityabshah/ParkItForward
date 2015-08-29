using System.Web.Http;
using System.Web.Mvc;
using Planner.Models;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    public class UsersController : Controller
    {
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Register register)
        {
            Users.Create(register);
            Session.Add("RegisterMessage", "You have been registered!  Please login...");
            return RedirectToAction("Index", "Home");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Information()
        {
            return View(Users.Get((long) Session["UserId"]));
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Edit()
        {
            return View(Users.Get((long) Session["UserId"]));
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult UpdatePassword(UserForm form)
        {
            /* (V2:) Verify old password
             * HttpPost of new password
             * Update function passing in only the newPassword to the Users model
             */
            var newPassword = form.newPassword;
            Users.UpdatePassword((long) Session["UserId"], newPassword);
            return null;
        }

        //  Login
        public ActionResult Login([FromBody] Login login)
        {
            var userId = Users.Login(login.EmailAddress, login.Password);
            if (userId > 0)
            {
                Session.Remove("LoginFailed");
                Session.Add("EmailAddress", login.EmailAddress);
                Session.Add("UserId", userId);
                return RedirectToAction("Index", "Home");
            }
            Session.Add("LoginFailed", true);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [System.Web.Http.HttpGet]
        public string SessionStatus()
        {
            if (Session["userId"] == null)
            {
                return "false";
            }
            return "true";
        }
    }
}