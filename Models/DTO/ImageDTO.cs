using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class ImageDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }
        [Required]
        [StringLength(200)]
        public string FileDescription { get; set; }
    }
}