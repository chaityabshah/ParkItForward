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
    public class Events
    {
        public static DateTime? endDateUtc { get; private set; }
        public static DateTime startDateUtc { get; private set; }

        public static IEnumerable<Event> GetAll(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<Event>().Where(_ => _.UserId == userId && _.Status != "DELETED").ToList();
            }
        }

        public static Event Get(long userId, long eventId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<Event>().Where(_ => _.Id == eventId && _.UserId == userId).FirstOrDefault();
            }
        }

        public static string GetEvents(long userId)
        {
            var userEvents = GetAll(userId);
            var events = new List<CalEvent>();
            var i = 0;
            foreach (var userEvent in userEvents)
            {
                var url = "../../Events/Details/" + userEvent.Id;
                var startDate = userEvent.Startdate;
                var endDate = ((userEvent.Enddate.HasValue) ? userEvent.Enddate : startDate);
                var name = userEvent.Name;
                var allDay = ((userEvent.Type == "All day") ? "true" : "");
                var addEvent = new CalEvent
                {
                    Id = i, // This might be a problem...
                    Title = name + " @ " + userEvent.Location,
                    Start = startDate,
                    End = endDate,
                    AllDay = allDay,
                    Url = url
                };
                events.Add(addEvent);
            }

            return JsonConvert.SerializeObject(events, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        internal static void Create(long userId, EventForm userEvent)
        {
            startDateUtc =
                new DateTimeOffset((DateTime) userEvent.StartDate,
                    new TimeSpan(userEvent.Offset*TimeSpan.TicksPerMinute))
                    .UtcDateTime;

            if (userEvent.EndDate.HasValue)
            {
                endDateUtc =
                    new DateTimeOffset((DateTime) userEvent.EndDate,
                        new TimeSpan(userEvent.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }

            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Events.Add(new Event
                {
                    UserId = userId,
                    Type = userEvent.Type,
                    Location = userEvent.Location,
                    Name = userEvent.Name,
                    Description = userEvent.Description,
                    Status = "ACTIVE",
                    Startdate = startDateUtc,
                    Enddate = endDateUtc,
                    Created = DateTime.UtcNow
                });
                dbContext.SaveChanges();
            }
        }

        internal static void Update(long userId, EventForm userEvent)
        {
            startDateUtc =
                new DateTimeOffset((DateTime) userEvent.StartDate,
                    new TimeSpan(userEvent.Offset*TimeSpan.TicksPerMinute))
                    .UtcDateTime;

            if (userEvent.EndDate.HasValue)
            {
                endDateUtc =
                    new DateTimeOffset((DateTime) userEvent.EndDate,
                        new TimeSpan(userEvent.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }
            else
            {
                endDateUtc = null;
            }


            using (var dbContext = new PlannerDbContext())
            {
                var currentEvent = Get(userId, userEvent.Id);

                var updatedEvent = new Event
                {
                    Id = userEvent.Id,
                    UserId = userId,
                    Type = userEvent.Type,
                    Location = userEvent.Location,
                    Name = userEvent.Name,
                    Description = userEvent.Description,
                    Startdate = startDateUtc,
                    Enddate = endDateUtc,
                    Status = "ACTIVE",
                    Created = currentEvent.Created,
                    Updated = DateTime.UtcNow
                    //Created = currentEvent.Created
                };

                dbContext.Events.Attach(updatedEvent); // ensure you specify Event.Id
                dbContext.Entry(updatedEvent).State = EntityState.Modified; // this is important
                dbContext.SaveChanges(); // save changes to the db
            }
        }

        internal static void AJAX(long userId, Xedit AJAXEvent)
        {
            //Xedit Event = JsonConvert.DeserializeObject<Xedit>(AJAXEvent);

            //throw new Exception();

            var newEvent = AJAXEvent;

            var currentEvent = Get(userId, newEvent.PK);

            // ADD UTC CONVERSION

            var updatedEvent = new Event
            {
                Id = newEvent.PK,
                UserId = userId,
                Name = ((newEvent.Name == "Name") ? newEvent.Value : currentEvent.Name),
                Location = ((newEvent.Name == "Location") ? newEvent.Value : currentEvent.Location),
                Description = ((newEvent.Name == "Description") ? newEvent.Value : currentEvent.Description),
                Status = "UPDATED",
                Created = currentEvent.Created,
                Updated = DateTime.UtcNow,
                Startdate = ((newEvent.Name == "Startdate") ? newEvent.Value.AsDateTime() : currentEvent.Startdate),
                Enddate = ((newEvent.Name == "Enddate") ? newEvent.Value.AsDateTime() : currentEvent.Enddate),
                Type = ((newEvent.Name == "Type") ? newEvent.Value : currentEvent.Type)
            }; // create obj and set keys

            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Events.Attach(updatedEvent); // ensure you specify Event.Id
                dbContext.Entry(updatedEvent).State = EntityState.Modified; // this is important
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

        internal static void Delete(long userId, EventForm userEvent)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var currentEvent = Get(userId, userEvent.Id);
                var updatedEvent = new Event
                {
                    UserId = userId,
                    Id = userEvent.Id,
                    Type = currentEvent.Type,
                    Name = currentEvent.Name,
                    Startdate = currentEvent.Startdate,
                    Enddate = currentEvent.Enddate,
                    Created = currentEvent.Created,
                    Status = "DELETED",
                    Updated = DateTime.UtcNow
                };
                dbContext.Events.Attach(updatedEvent); // ensure you specify Event.Id
                dbContext.Entry(updatedEvent).State = EntityState.Modified; // this is important
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
    }
}