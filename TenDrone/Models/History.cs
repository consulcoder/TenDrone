using System.ComponentModel.DataAnnotations;
using TenDrone.Resources;

namespace TenDrone.Models 
{
  
  public class History
  {
      [Key]
      public Guid Id { get; set; }
      public long Time { get; set; }
      public string SerialNumber { get; set; }
      public int BatteryCapacity { get; set; }
  }
}