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
    public class Classes
    {
        public static Dictionary<string, int> DaysOfWeek()
        {
            var daysOfWeek = new Dictionary<string, int>();
            for (var i = 0; i < 7; i++)
            {
                daysOfWeek.Add(Enum.GetName(typeof (DayOfWeek), i), i);
            }

            return daysOfWeek;
        }

        internal static void Create(long userId, ClassesForm @class)
        {
            var sections = JsonConvert.DeserializeObject<List<Section>>(@class.Sections);

            foreach (var section in sections)
            {
                section.Start =
                    new DateTimeOffset(section.Start, new TimeSpan(@class.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
                section.End =
                    new DateTimeOffset(section.End, new TimeSpan(@class.Offset*TimeSpan.TicksPerMinute))
                        .UtcDateTime;
            }

            var dbSections = JsonConvert.SerializeObject(sections);

            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Classes.Add(new Class
                {
                    UserId = userId,
                    Name = @class.Name,
                    Description = @class.Description,
                    Location = @class.Location,
                    Status = "ACTIVE",
                    Sections = dbSections,
                    Startdate = @class.StartDate,
                    Enddate = @class.EndDate,
                    Created = DateTime.UtcNow
                });
                dbContext.SaveChanges();
            }
        }

        public static IEnumerable<@Class> GetAll(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<@Class>().Where(_ => _.UserId == userId && _.Status == "ACTIVE").ToList();
            }
        }

        public static @Class Get(long userId, long classId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<@Class>().Where(_ => _.Id == classId && _.UserId == userId).FirstOrDefault();
            }
        }

        internal static void Update(long userId, ClassesForm @class)
        {
            var sections = JsonConvert.DeserializeObject<List<Section>>(@class.Sections);

            foreach (var section in sections)
            {
                section.Start = new DateTimeOffset(section.Start, new TimeSpan(@class.Offset*TimeSpan.TicksPerMinute))
                    .UtcDateTime;
                section.End = new DateTimeOffset(section.End, new TimeSpan(@class.Offset*TimeSpan.TicksPerMinute))
                    .UtcDateTime;
            }

            var dbSections = JsonConvert.SerializeObject(sections);


            using (var dbContext = new PlannerDbContext())
            {
                var updatedClass = new @Class
                {
                    Id = @class.Id, // Primary Key
                    UserId = userId,
                    Name = @class.Name,
                    Description = @class.Description,
                    Location = @class.Location,
                    Status = "ACTIVE",
                    Sections = dbSections,
                    Startdate = @class.StartDate,
                    Enddate = @class.EndDate,
                    Updated = DateTime.UtcNow
                };
                dbContext.Classes.Attach(updatedClass);
                dbContext.Entry(updatedClass).State = EntityState.Modified; // this is important
                dbContext.SaveChanges(); // save changes to the db
            }
        }

        public static string GetEvents(long userId)
        {
            var classes = GetAll(userId);
            var events = new List<CalEvent>();
            var days = DaysOfWeek();
            var i = 0;
            foreach (var @class in classes)
            {
                var url = "../../Classes/Details/" + @class.Id;
                var sections = JsonConvert.DeserializeObject<List<Section>>(@class.Sections);
                var startDate = @class.Startdate;
                var endDate = @class.Enddate;
                foreach (var section in sections)
                {
                    //var day = days[section.Day].ToString();
                    var dayOfWeek = section.Day;
                    var title = @class.Name + " -- " + section.Type;
                    var startTime = section.Start.ToString("HH:mm:ss");
                    var endTime = section.End.ToString("HH:mm:ss");

                    var present = startDate;

                    while (present <= endDate)
                    {
                        if (present.DayOfWeek.ToString() == dayOfWeek)
                        {
                            var sectionEvent = new CalEvent
                            {
                                Start = (present.ToString("yyyy-MM-dd") + "T" + startTime + "Z").AsDateTime(),
                                End = (present.ToString("yyyy-MM-dd") + "T" + endTime + "Z").AsDateTime(),
                                Url = url,
                                Title = title,
                                Id = i
                            };
                            events.Add(sectionEvent);
                            i++;
                        }
                        present = present.AddDays(1);
                    }
                    //var sectionEvent = new Section { Day =  day, }
                }
                //var sectionEvent = new Section {};
            }

            return JsonConvert.SerializeObject(events, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        internal static void Delete(long userId, ClassesForm @class)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var currentClass = Get(userId, @class.Id);
                var updatedClass = new @Class
                {
                    UserId = userId,
                    Id = @class.Id,
                    Name = currentClass.Name,
                    Location = currentClass.Location,
                    Description = currentClass.Description,
                    Startdate = currentClass.Startdate,
                    Enddate = currentClass.Enddate,
                    Created = currentClass.Created,
                    Sections = currentClass.Sections,
                    Status = "DELETED",
                    Updated = DateTime.UtcNow
                };
                dbContext.Classes.Attach(updatedClass); // ensure you specify task.Id
                dbContext.Entry(updatedClass).State = EntityState.Modified; // this is important

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

        public class Section
        {
            [JsonProperty(PropertyName = "Day")]
            public string Day { get; set; }

            [JsonProperty(PropertyName = "Type")]
            public string Type { get; set; }

            [JsonProperty(PropertyName = "Start")]
            public DateTime Start { get; set; }

            [JsonProperty(PropertyName = "End")]
            public DateTime End { get; set; }

            [JsonProperty(PropertyName = "Location")]
            public string Location { get; set; }
        }
    }
}