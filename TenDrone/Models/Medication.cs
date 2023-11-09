using System.ComponentModel.DataAnnotations;

namespace TenDrone.Models 
{
    public class Medication
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Image { get; set; }
}
}