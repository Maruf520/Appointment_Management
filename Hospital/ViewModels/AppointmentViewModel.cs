using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class AppointmentViewModel
    {
        public List<Appointment> Appointments  { get; set; }
        public IEnumerable< Appointment > Appointment { get; set; }
        public string StringName { get; set; }
        public Patient Patient  { get; set; }
        public string PatientId { get; set; }
        public DateTime  Date { get; set; }
    }
}
