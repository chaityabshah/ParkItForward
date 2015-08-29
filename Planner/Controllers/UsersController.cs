using System.Web.Http;
using System.Web.Mvc;
using Planner.Models;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    public class UsersController : Controller
    {
        public bool Index(string register)
        {
            return true;
        }

        [System.Web.Mvc.HttpPost]
        public string Register(Register form)
        {
            return Users.Create(form);
        }
    }
}