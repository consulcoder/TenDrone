using System.ComponentModel.DataAnnotations;
using TenDrone.Models;

namespace TenDrone.Resources;
public class MedicationDto
{
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Weight { get; set; }
    [Required]
    public string Image { get; set; }
}