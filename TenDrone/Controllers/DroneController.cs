using Microsoft.AspNetCore.Mvc;
using TenDrone.Services;
using TenDrone.Models;
using TenDrone.Resources;

namespace TenDrone.Controllers;

[ApiController]
[Route("api/drones")]
public class DroneController : ControllerBase
{
    private readonly IDroneService _droneService;

    public DroneController(IDroneService droneService)
    {
        _droneService = droneService;
    }

    [HttpGet]
    public IEnumerable<Drone> GetDrones()
    {
        return _droneService.GetDrones();
    }

    [HttpGet("{serialNumber}")]
    public IActionResult GetDroneBySerialNumber(string serialNumber)
    {
        var drone = _droneService.GetDroneBySerialNumber(serialNumber);

        if (drone == null)
        {
            return NotFound($"Drone with serial number '{serialNumber}' not found.");
        }

        return Ok(drone);
    }

    [HttpPost]
    public IActionResult LoadDrone([FromBody] LoadDroneRequest request)
    {
        try
        {
            _droneService.LoadDrone(request.SerialNumber, request.Weight, request.BatteryLevel);
            return Ok("Drone loaded successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{serialNumber}")]
    public IActionResult UpdateDrone(string serialNumber, [FromBody] UpdateDroneRequest request)
    {
        try
        {
            _droneService.UpdateDrone(serialNumber, request);
            return Ok($"Drone with serial number '{serialNumber}' updated successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{serialNumber}")]
    public IActionResult DeleteDrone(string serialNumber)
    {
        try
        {
            _droneService.DeleteDrone(serialNumber);
            return Ok($"Drone with serial number '{serialNumber}' deleted successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}