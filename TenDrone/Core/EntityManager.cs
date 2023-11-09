using Microsoft.EntityFrameworkCore;
using TenDrone.Models;
public class EntityManager:DbContext
{
    public EntityManager(DbContextOptions<EntityManager> options)
   : base(options) { }

    public DbSet<Drone> Drones => Set<Drone>();
    public DbSet<Medication> Medications => Set<Medication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drone>()
            .HasMany(d => d.Medications)
            .WithOne(m => m.Drone)
            .HasForeignKey(m => m.DroneId);

        modelBuilder.Entity<Medication>()
            .HasOne(m => m.Drone)
            .WithMany(d => d.Medications);
    
    }
}