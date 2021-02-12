using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class TimeSlotViewModel
    {
        public List<TimeSlot> TimeSlots { get; set; }
        public Doctor Doctor { get; set; }
        public string Name { get; set; }

        public string PatientId { get; set; }

        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateOfAppointment { get; set; }
        public string Details { get; set; }

        public Patient patient { get; set; }

        public List<int> Days { get; set; }
        public List<TimeSlot> AvailableTime { get; set; }
    }
}
