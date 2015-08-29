using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Models
{
    public class Users
    {
        public static string Create(Register form)
        {
            return Json.Encode(form);
        }
    }
}