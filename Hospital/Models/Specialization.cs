using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Specialization
    {
        
        public int SpecializationId { get; set; }
        public string Name { get; set; }
        public ICollection<DoctorSpecialization> doctorSpecializations { get; set; }
    }
}
