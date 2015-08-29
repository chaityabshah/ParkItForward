using System.ComponentModel.DataAnnotations;

namespace Planner.Models.DTO
{
    public class Register
    {
        [Required]
        [StringLength(128)]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public string Offset { get; set; }
    }
}