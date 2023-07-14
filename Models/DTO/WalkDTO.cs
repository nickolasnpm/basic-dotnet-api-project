using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class WalkDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
    }
}
