using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    [RoutePrefix("api/park-it-forward")]
    public class ParkItForwardController : ApiController
    {
        [Route("register")]
        public string Register(Register register)
        {
            return "good";
        }
    }
}
