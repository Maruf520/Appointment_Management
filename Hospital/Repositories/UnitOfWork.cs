using Hospital.IRepositories;
using Hospital.Models;
using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;

        public IPatientRepository Patient { get; private set; }

        public IGenderRepository Gender { get; private set; }
        public IDoctorRepository doctorRepository { get; private set; }
        public ISpecializationRepository specializationRepository { get; private set; }



        public UnitOfWork(HospitalDbContext context)
        {
            _context = context;
            Patient = new PatientRepository(context);
            Gender = new GenderRepository(context);
            doctorRepository = new DoctorRepository(context);
            specializationRepository = new SpecializationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
