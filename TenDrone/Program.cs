using Microsoft.EntityFrameworkCore;
using TenDrone.Repository;
using TenDrone.Services;
using System.Threading;
using TenDrone.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add dbContext, here you can we are using In-memory database.
//builder.Services.AddDbContext<EntityManager>(opt=>opt.UseInMemoryDatabase("TenDrone"),ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<EntityManager>(opt=>opt.UseInMemoryDatabase("TenDrone"),ServiceLifetime.Transient);
// Add Services and Repositories
builder.Services.AddTransient<IDroneService, DroneService>();
builder.Services.AddTransient<IDroneRepository, DroneRepository>();
builder.Services.AddTransient<IMedicationRepository, MedicationRepository>();
builder.Services.AddHostedService<BatteryCheckHostedService>();
builder.Services.AddMemoryCache();
// Build
var app = builder.Build();
//var _droneService = app.Services.GetService<IDroneService>();
// Crea un temporizador que ejecutará la tarea cada 1 minuto (60000 ms).
//var timer = new Timer(DoPeriodicTask, _droneService, 0, 5000);
   
 /*  
static void DoPeriodicTask(object state)
{
    Console.WriteLine("Tarea periódica ejecutada a las " + DateTime.Now);
    var _droneService = (IDroneService)state;

    var audits = _droneService.GetDrones().Select(item => new History()
    {
        TimeSpan = (int)DateTime.Now.TimeOfDay.TotalSeconds,
        SerialNumber = item.SerialNumber,
        BatteryCapacity = item.BatteryCapacity
    });

    _droneService.AddAudit(audits);
    // Coloca aquí el código de la tarea periódica que deseas ejecutar cada minuto.
}
 */
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

