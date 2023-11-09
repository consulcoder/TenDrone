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
      public double CurrentWeight { get; set; }
      public int BatteryCapacity { get; set; }
      [Required]
      public DroneState State { get; set; }
      public ICollection<Medication> Medications { get; set; } = new List<Medication>();

      public double GetMedicationsWeight(List<Medication> medications = null) {
        double s = 0;
        if(medications == null)
          medications = Medications.ToList();
        foreach(var item in medications)
          s+=item.Weight;
        return s;
      }
      public void AddMedications(List<Medication> medications) {
        var w = GetMedicationsWeight(medications);
        if(w > WeightLimit)
          throw new InvalidOperationException($"Cannot add Items the drone with serial number '{SerialNumber}' when weight is high {WeightLimit}.");
        foreach (var item in medications) {
          item.Drone = this;
          Medications.Add(item);
        }
        CurrentWeight = w;
      }
  }
}