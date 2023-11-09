using System.ComponentModel.DataAnnotations;
using TenDrone.Models;

namespace TenDrone.Resources;
public class AddMedicationRequest
{
    [Required]
    public string SerialNumber {get; set;}
    
    public List<MedicationDto> Medications {get; set;}
}