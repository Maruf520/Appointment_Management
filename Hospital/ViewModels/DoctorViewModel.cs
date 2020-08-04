using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class DoctorViewModel
    {
        public List<Doctor> Doctors { get; set; }

        public DateTime? Date { get; set; }
        public int DoctorId { get; set; }
    }
}
