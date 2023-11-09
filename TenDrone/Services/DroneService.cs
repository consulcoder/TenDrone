using TenDrone.Models;
using TenDrone.Repository;
using TenDrone.Resources;
using Mapster;

namespace TenDrone.Services
{
    public interface IDroneService
    {
        IEnumerable<Drone> GetDrones();
        RegisterDroneRequest GetDroneBySerialNumber(string serialNumber);
        int GetDroneBatteryBySerialNumber(string serialNumber);
        void RegisterDrone(RegisterDroneRequest registerDroneRequest);
        void AddItem(AddMedicationRequest medication);
        IEnumerable<Medication> GetItems(string serialNumber);
        IEnumerable<Drone> GetAvailableDrones();
        void UpdateDrone(string serialNumber, UpdateDroneRequest request);
        void DeleteDrone(string serialNumber);
    }

    public class DroneService : IDroneService
    {
        private readonly EntityManager em;
        private readonly IDroneRepository repo;

        public DroneService(EntityManager entityManager, IDroneRepository droneRepository) {
            em = entityManager;
            repo = droneRepository;
        }

        public IEnumerable<Drone> GetDrones()
        {
            return em.Drones.ToList();
            //return repo.GetAll();
        }

        public RegisterDroneRequest GetDroneBySerialNumber(string serialNumber)
        {
            return repo.GetDroneBySerialNumber(serialNumber)
                .Adapt(new RegisterDroneRequest());
        }

        public void RegisterDrone(RegisterDroneRequest rq)
        {
            var existingDrone = repo.GetDroneBySerialNumber(rq.SerialNumber);

            if (existingDrone != null)
            {
                throw new InvalidOperationException($"Drone with serial number '{rq.SerialNumber}' already exists.");
            }

            if (rq.Weight > 500) {
                throw new InvalidOperationException($"Cannot register the drone with serial number '{rq.SerialNumber}' when weight is high 500.");
            }

            if (rq.BatteryLevel > 100) {
                throw new InvalidOperationException($"Cannot register the drone with serial number '{rq.SerialNumber}' when battery is high 100.");
            }

            var newDrone = new Drone
            {
                SerialNumber = rq.SerialNumber,
                Model = rq.DroneModel,
                WeightLimit = rq.Weight,
                BatteryCapacity = rq.BatteryLevel,
                State = DroneState.IDLE
            };

            // var med = new Medication() {
            //     Name = "Test00",
            //     Code = "00000",
            //     Weight = 10,
            //     Image = "foto.jpg",
            //     Drone = newDrone
            // };
            // newDrone.Medications.Add(med);
            
            em.Drones.Add(newDrone);
            //em.Medications.Add(med);
            em.SaveChanges();
        }

        public void AddItem(AddMedicationRequest rq)
        {
            var drone = repo.GetDroneBySerialNumber(rq.SerialNumber);

            if (drone == null)
            {
                throw new InvalidOperationException($"Drone with serial number '{rq.SerialNumber}' no exists.");
            }
            
            try {
                var list = GetItems(rq.SerialNumber).ToList(); //In memory does'nt create RelationShip (!) Remove this line with other dsn
                // GET new Items
                list.AddRange(rq.Medications.Select(m=>m.Adapt(new Medication(){ Drone = drone})).ToList());
                // Add new Items
                var w = GetMedicationsWeight(list);
                if(w > drone.WeightLimit)
                    throw new InvalidOperationException($"Cannot add Items the drone with serial number '{drone.SerialNumber}' when weight is high {drone.WeightLimit}.");
                drone.CurrentWeight = w;
                //drone.AddMedications(list);
                // Save
                em.Medications.AddRange(list);
                em.SaveChanges();
            }
            catch(Exception ex) {
                throw ex;
            }
            
        }
        protected double GetMedicationsWeight(List<Medication> medications) {
            double s = 0;
            foreach(var item in medications)
                s+=item.Weight;
            return s;
        }

        public int GetDroneBatteryBySerialNumber(string serialNumber)
        {
            return repo.GetDroneBySerialNumber(serialNumber).BatteryCapacity;
        }

        public IEnumerable<Medication> GetItems(string serialNumber)
        {
            return em.Medications.Where(m=>m.DroneId == serialNumber).ToList();
            
            // var drone = repo.GetDroneBySerialNumber(serialNumber);
            // return drone.Medications;
        }

        public IEnumerable<Drone> GetAvailableDrones()
        {
             return repo.GetAvialableDrones();
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