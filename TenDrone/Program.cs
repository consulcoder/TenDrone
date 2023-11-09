using Microsoft.EntityFrameworkCore;
using TenDrone.Repository;
using TenDrone.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add dbContext, here you can we are using In-memory database.
builder.Services.AddDbContext<EntityManager>(opt=>opt.UseInMemoryDatabase("TenDrone"));
// Add Services and Repositories
builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();

// Build
var app = builder.Build();

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

