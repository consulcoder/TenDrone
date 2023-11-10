using Microsoft.AspNetCore.Mvc;
using TenDrone.Services;
using TenDrone.Models;
using TenDrone.Resources;

namespace TenDrone.Controllers;

[ApiController]
[Route("history")]
public class HistoryController : ControllerBase
{
    private readonly ILogger<HistoryController> _logger;
        private readonly IDroneService _droneService;

    public HistoryController(IDroneService droneService, ILogger<HistoryController> logger)
    {
        _droneService = droneService;
        _logger = logger;
    }

    
    [HttpGet]
    [Route("audit")]
    public IEnumerable<History> GetItems()
    {
        return _droneService.GetHistory();
    }

}