using TenDrone.Models;

namespace TenDrone.Repository 
{
    public interface IMedicationRepository {
        IQueryable<Medication> GetMedicationsByDrone(Drone drone);
    }

    class MedicationRepository : IMedicationRepository
    {
        public IQueryable<Medication> GetMedicationsByDrone(Drone drone)
        {
            throw new NotImplementedException();
        }
    }

}