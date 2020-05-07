using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Gender
    {
        public int GenderId { get; set; }

        public string GenderName { get; set; }
        public ICollection<Patient> patients { get; set; }
       
    }
}
