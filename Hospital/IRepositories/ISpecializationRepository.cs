using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public interface ISpecializationRepository
    {
        public IEnumerable<Specialization> GetSpecializations();
    }
}
