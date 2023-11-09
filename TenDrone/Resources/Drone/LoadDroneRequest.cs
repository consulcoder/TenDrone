namespace TenDrone.Resources;
public class LoadDroneRequest
{
    public string SerialNumber { get; set; }
    public double Weight { get; set; }
    public int BatteryLevel { get; set; }
}