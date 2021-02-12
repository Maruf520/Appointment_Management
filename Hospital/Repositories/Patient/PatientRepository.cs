using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class PatientRepository : IPatientRepository
        
    {
        private readonly HospitalDbContext _hospitalDbContext;

        public PatientRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public void Add(Patient patient)
        {
             _hospitalDbContext.Patients.Add(patient);
        }

        public Patient GetPatientById(string id)
        {
            return _hospitalDbContext.Patients.SingleOrDefault(c => c.PatientId == id);
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _hospitalDbContext.Patients;
        }
        public IEnumerable<Patient> GetPatientList ()
        {
            return _hospitalDbContext.Patients;
        }

/*        public IEnumerable<Patient> GetRecentPatients()
        {
            return _hospitalDbContext.Patients.Where(a => DbFunctions.DiffDays(a.DateTime, DateTime.Now) == 0);
        }*/

        public void Remove(Patient patient)
        {
            _hospitalDbContext.Patients.Remove(patient);
        }
        public void Update(Patient patient)
        {
            _hospitalDbContext.Patients.Update(patient);
        }
    }
}
