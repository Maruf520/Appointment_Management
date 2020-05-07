using Hospital.Models;
using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.IRepositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public SpecializationRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public IEnumerable<Specialization> GetSpecializations()
        {
            return _hospitalDbContext.Specializations;
        }
    }
}
