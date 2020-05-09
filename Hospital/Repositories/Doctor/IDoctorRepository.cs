using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetDoctors();

        IEnumerable<Doctor> GetAvailableDoctors();

        Doctor GetDoctorById(int id);
        /*  Doctor GetDoctorProfile();*/
        void Add(Doctor doctor);
        void Remove(Doctor doctor);
    }
}
