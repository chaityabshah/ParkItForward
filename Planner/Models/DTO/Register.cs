using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class Register
    {
        [Required]
        public string VIN { get; set; }
        [Required]
        public string Plate { get; set; }
    }
}