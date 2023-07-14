using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Models.DTO
{
    public class RegionDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Area { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public long Population { get; set; }
    }
}
