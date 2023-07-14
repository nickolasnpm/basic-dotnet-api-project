using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class LoginRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}