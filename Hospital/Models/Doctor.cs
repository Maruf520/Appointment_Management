using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"(^([+]{1}[8]{2}|0088)?(01){1}[3-9]{1}\d{8})$")]
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }
        public int SpecializationId { get; set; }
        public ICollection<DoctorSpecialization> doctorSpecializations { get; set; }
        
    }
}
