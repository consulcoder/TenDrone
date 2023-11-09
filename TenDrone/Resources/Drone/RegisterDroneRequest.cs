using System.ComponentModel.DataAnnotations;

namespace TenDrone.Resources;
public class RegisterDroneRequest
{
    [Required]
    public string SerialNumber { get; set; }
    public double Weight { get; set; }
    public int BatteryLevel { get; set; }
    [Required]
    public DroneModel DroneModel { get; set; }
}