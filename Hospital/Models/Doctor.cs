using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctor
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
