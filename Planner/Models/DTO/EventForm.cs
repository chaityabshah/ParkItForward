using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class EventForm
    {
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(128)]
        public string Type { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int Offset { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long UserId { get; internal set; }
    }
}