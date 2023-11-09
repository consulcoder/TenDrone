using TenDrone.Models;
using TenDrone.Resources;

namespace TenDrone.Services
{
    public interface IDroneService
    {
        IEnumerable<Drone> GetDrones();
        Drone GetDroneBySerialNumber(string serialNumber);
        void RegisterDrone(string serialNumber, double weight, int batteryLevel);
        IEnumerable<Medication> GetItems();
        IEnumerable<Drone> GetAvailableDrones();
        void UpdateDrone(string serialNumber, UpdateDroneRequest request);
        void DeleteDrone(string serialNumber);
    }

    public class DroneService : IDroneService
    {
        private readonly EntityManager em;

        public DroneService(EntityManager entityManager) {
            em = entityManager;
        }

        public IEnumerable<Drone> GetDrones()
        {
            return em.Drones.ToList();
        }

        public Drone GetDroneBySerialNumber(string serialNumber)
        {
            return em.Drones.FirstOrDefault(d => d.SerialNumber == serialNumber);
        }

        public void RegisterDrone(string serialNumber, double weight, int batteryLevel)
        {
            var existingDrone = em.Drones.FirstOrDefault(d => d.SerialNumber == serialNumber);

            if (existingDrone != null)
            {
                throw new InvalidOperationException($"Drone with serial number '{serialNumber}' already exists.");
            }

            // if (batteryLevel < 25)
            // {
            //     throw new InvalidOperationException($"Cannot load the drone with serial number '{serialNumber}' when battery level is below 25%.");
            // }

            var newDrone = new Drone
            {
                SerialNumber = serialNumber,
                Model = DroneModel.Lightweight,
                WeightLimit = 500,
                BatteryCapacity = batteryLevel,
                State = DroneState.IDLE
            };

            em.Drones.Add(newDrone);
            em.SaveChanges();
        }

        public IEnumerable<Medication> GetItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> GetAvailableDrones()
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(string serialNumber, UpdateDroneRequest request)
        {
            var drone = em.Drones.FirstOrDefault(d => d.SerialNumber == serialNumber);

            if (drone == null)
            {
                throw new InvalidOperationException($"Drone with serial number '{serialNumber}' not found.");
            }

            if (request.WeightLimit > 500)
            {
                throw new InvalidOperationException($"Weight limit cannot exceed 500 grams.");
            }

            if (request.BatteryCapacity < 25 && drone.State == DroneState.LOADING)
            {
                throw new InvalidOperationException($"Cannot be in LOADING state if battery level is below 25%.");
            }

            drone.Model = request.Model;
            drone.WeightLimit = request.WeightLimit;
            drone.BatteryCapacity = request.BatteryCapacity;
            em.SaveChanges();
        }

        public void DeleteDrone(string serialNumber)
        {
            var drone = em.Drones.FirstOrDefault(d => d.SerialNumber == serialNumber);

            if (drone == null)
            {
                throw new InvalidOperationException($"Drone with serial number '{serialNumber}' not found.");
            }

            em.Drones.Remove(drone);
            em.SaveChanges();
        }
        
    }

}