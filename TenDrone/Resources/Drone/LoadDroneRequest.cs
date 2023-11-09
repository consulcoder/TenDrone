namespace TenDrone.Resources;
public class RegisterDroneRequest
{
    public string SerialNumber { get; set; }
    public double Weight { get; set; }
    public int BatteryLevel { get; set; }
}