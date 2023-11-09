using System.ComponentModel.DataAnnotations;
using TenDrone.Resources;

namespace TenDrone.Models 
{
  
  public class Drone
  {
      [Key]
      public string SerialNumber { get; set; }
      public DroneModel Model { get; set; }
      public double WeightLimit { get; set; }
      public int BatteryCapacity { get; set; }
      public DroneState State { get; set; }
  }
}