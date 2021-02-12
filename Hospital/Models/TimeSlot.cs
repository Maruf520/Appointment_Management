using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class TimeSlot
    {

        [Key]
        public int TimeSlotId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public Doctor Doctor { get; set; }


    }
}
