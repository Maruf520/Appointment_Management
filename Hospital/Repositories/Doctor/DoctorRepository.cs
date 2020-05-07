using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _hospitalDbContext;

        public DoctorRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _hospitalDbContext.Doctors.Include(s => s.Specialization);
        }

        public IEnumerable<Doctor> GetAvailableDoctors()
        {
            return _hospitalDbContext.Doctors.Include(c => c.Specialization).Where(a => a.IsAvailable == true);
        }

        public Doctor GetDoctorById(int id)
        {
            return _hospitalDbContext.Doctors.Include(s => s.Specialization).SingleOrDefault(d => d.Id == id);
        }

        public void Add(Doctor doctor)
        {
            _hospitalDbContext.Doctors.Add(doctor);
        }
    }
}
