using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class DoctorSpecialization
    {
           
        [Key]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [Key]
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
