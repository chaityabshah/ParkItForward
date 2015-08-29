using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.WebPages;
using Newtonsoft.Json;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Models
{
    public class Tasks
    {
        public static DateTime? endDateUtc { get; private set; }
        public static DateTime? startDateUtc { get; private set; }

        public static IEnumerable<Task> GetAll(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<Task>().Where(_ => _.UserId == userId && _.Status != "DELETED").OrderBy(_ => _.Startdate).ToList();
            }
        }

        public static Task Get(long userId, long taskId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<Task>().Where(_ => _.Id == taskId && _.UserId == userId).FirstOrDefault();
            }
        }

        public static string GetEvents(long userId)
        {
            var tasks = GetAll(userId);
            var events = new List<CalEvent>();
            var i = 0;
            foreach (var task in tasks)
            {
                var url = "../../Tasks/Details/" + task.Id;
                if (!task.Startdate.HasValue)
                {
                    continue;
                }
                var startDate = (DateTime) task.Startdate;
                var endDate = ((task.Enddate.ToString().AsDateTime() < task.Startdate.ToString().AsDateTime())
                    ? task.Enddate
                    : startDate);
                var name = task.Name;
                var taskEvent = new CalEvent
                {
                    Id = i, // This might be a problem...
                    Title = name,
                    Start = startDate,
                    End = endDate,
                    Url = url
                };
                events.Add(taskEvent);
            }

            return JsonConvert.SerializeObject(events, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        internal static void AJAX(long userId, Xedit AJAXtask)
        {
            //Xedit task = JsonConvert.DeserializeObject<Xedit>(AJAXtask);

            //throw new Exception();

            var newTask = AJAXtask;

            var currentTask = Get(userId, newTask.PK);

            // ADD UTC CONVERSION

            var updatedTask = new Task
            {
                Id = newTask.PK,
                UserId = userId,
                Subject = ((newTask.Name == "Subject") ? newTask.Value : currentTask.Subject),
                Type = ((newTask.Name == "Type") ? newTask.Value : currentTask.Type),
                Name = ((newTask.Name == "Name") ? newTask.Value : currentTask.Name),
                Description = ((newTask.Name == "Description") ? newTask.Value : currentTask.Description),
                Status = "UPDATED",
                Created = currentTask.Created,
                Updated = DateTime.UtcNow,
                Startdate = ((newTask.Name == "Startdate") ? newTask.Value.AsDateTime() : currentTask.Startdate),
                Enddate = ((newTask.Name == "Enddate") ? newTask.Value.AsDateTime() : currentTask.Enddate)
            }; // create obj and set keys

            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Tasks.Attach(updatedTask); // ensure you specify task.Id
                dbContext.Entry(updatedTask).State = EntityState.Modified; // this is important
                try
                {
                    dbContext.SaveChanges(); // save changes to the db
                }
                catch (DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            var message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity,
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        internal static void Create(long userId, TaskForm task)
        {
            if (task.StartDate.HasValue)
            {
                startDateUtc =
                    new DateTimeOffset((DateTime) task.StartDate, new TimeSpan(task.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }
            if (task.EndDate.HasValue)
            {
                endDateUtc =
                    new DateTimeOffset((DateTime) task.EndDate, new TimeSpan(task.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }

            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Tasks.Add(new Task
                {
                    UserId = userId,
                    Subject = task.Subject,
                    Type = task.Type,
                    Name = task.Name,
                    Description = task.Description,
                    Status = "ACTIVE",
                    Startdate = startDateUtc,
                    Enddate = endDateUtc,
                    Created = DateTime.UtcNow
                });
                dbContext.SaveChanges();
            }
        }

        internal static void Update(long userId, TaskForm task)
        {
            if (task.StartDate.HasValue)
            {
                startDateUtc =
                    new DateTimeOffset((DateTime) task.StartDate, new TimeSpan(task.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }
            else
            {
                startDateUtc = null;
            }
            if (task.EndDate.HasValue)
            {
                endDateUtc =
                    new DateTimeOffset((DateTime) task.EndDate, new TimeSpan(task.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }
            else
            {
                endDateUtc = null;
            }

            var currentTask = Get(userId, task.Id);

            using (var dbContext = new PlannerDbContext())
            {
                var updatedTask = new Task
                {
                    UserId = userId,
                    Id = task.Id,
                    Subject = task.Subject,
                    Type = task.Type,
                    Name = task.Name,
                    Description = task.Description,
                    Status = "ACTIVE",
                    Startdate = startDateUtc,
                    Enddate = endDateUtc,
                    Created = currentTask.Created,
                    Updated = DateTime.UtcNow
                };
                dbContext.Tasks.Attach(updatedTask); // ensure you specify task.Id
                dbContext.Entry(updatedTask).State = EntityState.Modified; // this is important
                dbContext.SaveChanges(); // save changes to the db
            }
        }

        internal static void Delete(long userId, TaskForm task)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var updatedTask = new Task
                {
                    UserId = userId,
                    Name = task.Name,
                    Id = task.Id,
                    Status = "DELETED",
                    Updated = DateTime.UtcNow
                };
                dbContext.Tasks.Attach(updatedTask); // ensure you specify task.Id
                dbContext.Entry(updatedTask).State = EntityState.Modified; // this is important
                dbContext.SaveChanges(); // save changes to the db
            }
        }
    }
}