using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class GenderRepository: IGenderRepository
    {
        private readonly HospitalDbContext _hospitalDbContext;
        public GenderRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public  IEnumerable<Gender> GetGenders()
        {
            return  _hospitalDbContext.Genders;
        }
    }
}
