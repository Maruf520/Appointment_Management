using Hospital.Models;
using Hospital.Repositories;
using Hospital.Repositories.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital
{
    public interface IUnitOfWork
    {
        IPatientRepository Patient { get; }
        IGenderRepository Gender { get; }
        IDoctorRepository doctorRepository { get; }
        ISpecializationRepository specializationRepository { get; }
        IAppoinmentRepository appoinmentRepository { get; }




        void Complete();
    }
}
