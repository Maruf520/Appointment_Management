using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public Doctor Doctor { get; set; }
        public List<SelectListItem> Doctors { set; get; }
    }
}
