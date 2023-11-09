using TenDrone.Models;
using TenDrone.Resources;

namespace TenDrone.Repository 
{
    public interface IDroneRepository {
        Drone? GetDroneBySerialNumber(string serialNumber);
        IQueryable<Drone> GetAvialableDrones();
        IQueryable<Drone> GetAll();
    }

    class DroneRepository : IDroneRepository
    {
        private readonly EntityManager em;
        public DroneRepository(EntityManager entityManager) {
            em = entityManager;
        }

        public IQueryable<Drone> GetAll()
        {
            return em.Drones.AsQueryable();
        }

        public IQueryable<Drone> GetAvialableDrones()
        {
            return em.Drones.Where(d => 
                d.BatteryCapacity >= 25 
                && (d.State == DroneState.IDLE || d.State == DroneState.LOADING)
                && d.CurrentWeight < d.WeightLimit 
            );
        }

        public Drone? GetDroneBySerialNumber(string serialNumber)
        {
            return em.Drones.FirstOrDefault(d => d.SerialNumber == serialNumber);
        }
    }

}