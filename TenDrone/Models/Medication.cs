using System.ComponentModel.DataAnnotations;

namespace TenDrone.Models 
{
    public class Medication
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public string Image { get; set; }
        
        public string DroneId { get; set; }
        [Required]
        public Drone Drone { get; set; }
    }
}