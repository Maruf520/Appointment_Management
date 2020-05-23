using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class AppointmentFormViewModel
    {
        public AppointmentFormViewModel()
        {
            DoctorList = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public DateTime DateTime { get; set; }
        [Required]
        public string Detail { get; set; }

        [Required]
        public int Patient { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
        [Required]
        public int Doctor { get; set; }

        public Doctor Doctors { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }

        public List<SelectListItem> DoctorList { get; set; }
        public int MyProperty { get; set; }

        public DateTime GetStartDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}
