using Microsoft.EntityFrameworkCore;
using TenDrone.Models;
public class EntityManager:DbContext
{
    public EntityManager(DbContextOptions<EntityManager> options)
   : base(options) { }

    public DbSet<Drone> Drones => Set<Drone>();
}