using System;
using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class ClassesForm
    {
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int Offset { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime Section { get; set; } //Create validation for this
        public string Sections { get; set; }
    }
}