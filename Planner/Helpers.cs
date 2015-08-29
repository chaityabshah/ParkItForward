using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner
{
    public static class Helpers
    {
        public static string CalendarData(this HtmlHelper htmlHelper, IEnumerable<Task> tasks)
        {
            var taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                taskDtos.Add(new TaskDto
                {
                    Title = task.Name,
                    Start =
                        task.Startdate != null
                            ? task.Startdate.Value.ToString("u").Replace("Z", "").Replace(" ", "T")
                            : null,
                    End =
                        task.Enddate != null
                            ? task.Enddate.Value.ToString("u").Replace("Z", "").Replace(" ", "T")
                            : null,
                    Id = "../../Tasks/Details/" + task.Id
                });
            }
            var s = JsonConvert.SerializeObject(taskDtos);
            return s.TrimStart('[').TrimEnd(']');
        }
    }
}