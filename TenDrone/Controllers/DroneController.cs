using Microsoft.AspNetCore.Mvc;
using TenDrone.Services;
using TenDrone.Models;
using TenDrone.Resources;

namespace TenDrone.Controllers;

[ApiController]
[Route("api/drones")]
public class DroneController : ControllerBase
{
    private readonly ILogger<DroneController> _logger;
        private readonly IDroneService _droneService;

    public DroneController(IDroneService droneService, ILogger<DroneController> logger)
    {
        _droneService = droneService;
        _logger = logger;
    }

    [HttpPost]
    [Route("add")]
    public IActionResult RegisterDrone([FromBody] RegisterDroneRequest request)
    {
        try
        {
            _droneService.RegisterDrone(request);
            return Ok("Drone registered successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("add-medications")]
    public IActionResult AddItem([FromBody] AddMedicationRequest request)
    {
        try
        {
            _droneService.AddItem(request);
            return Ok("Items added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("medications/{serialNumber}")]
    public IEnumerable<Medication> GetItems(string serialNumber)
    {
        return _droneService.GetItems(serialNumber);
    }

    [HttpGet]
    public IEnumerable<Drone> GetDrones()
    {
        return _droneService.GetDrones();
    }

    [HttpGet]
    [Route("available")]
    public IEnumerable<Drone> GetAvailableDrones()
    {
        return _droneService.GetAvailableDrones();
    }

    [HttpGet("details/{serialNumber}")]
    public IActionResult GetDroneBySerialNumber(string serialNumber)
    {
        var drone = _droneService.GetDroneBySerialNumber(serialNumber);

        if (drone == null)
        {
            return NotFound($"Drone with serial number '{serialNumber}' not found.");
        }

        return Ok(drone);
    }

    [HttpGet("battery/{serialNumber}")]
    public IActionResult GetDroneBatteryBySerialNumber(string serialNumber)
    {
        return Ok(_droneService.GetDroneBatteryBySerialNumber(serialNumber));
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