using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class DoctorTimeslot
    {
        public Doctor Doctor { get; set; }
        
        public int DoctorId { get; set; }
        
        public int TimeSoltId { get; set; }
        public DateTime Date { get; set; }
        public TimeSlot TimeSlot { get; set; }


      

        
    }
}
