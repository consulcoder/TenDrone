using TenDrone.Models;
using TenDrone.Services;

public class BatteryCheckHostedService : IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<BatteryCheckHostedService> _logger;
    private Timer _timer;
    private readonly IDroneService _droneService;

    public BatteryCheckHostedService(ILogger<BatteryCheckHostedService> logger, IDroneService droneService)
    {
        _logger = logger;
        _droneService = droneService;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Checking Bettery Levels Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, 
            TimeSpan.FromSeconds(60));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        var count = Interlocked.Increment(ref executionCount);

        var audits = _droneService.GetDrones().Select(item=>new History() {
            Id = Guid.NewGuid(),
            Time = DateTime.UtcNow.ToFileTime(),
            SerialNumber = item.SerialNumber,
            BatteryCapacity = item.BatteryCapacity
        });

        _droneService.AddAudit(audits);

        _logger.LogInformation(
            $"Bettery Levels Service Check: #{count}, total inserted = {audits.Count()}, time({DateTime.UtcNow.ToFileTime()})");
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Bettery Levels Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}

