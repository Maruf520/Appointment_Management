using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetPatients();
        /*       IEnumerable<Patient> GetRecentPatients();*/
        IEnumerable<Patient> GetPatientList();
        Patient GetPatientById(string id);
        void Add(Patient  patient);
        void Remove(Patient patient);
        void Update(Patient patient);

    }
}
