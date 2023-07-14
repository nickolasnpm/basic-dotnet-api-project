using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class DifficultyDTO
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
    }
}
