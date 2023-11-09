namespace TenDrone.Resources {
    public class UpdateDroneRequest
    {
        public DroneModel Model { get; set; }
        public double WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
    }
}