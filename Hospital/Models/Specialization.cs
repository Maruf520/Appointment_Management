using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Specialization
    {
        public int SpecializationId { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> doctors { get; set; }

    }
}
