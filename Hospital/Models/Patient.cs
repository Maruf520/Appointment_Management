using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        public String Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }


        public int Age
        {
            get
            {
                var now = DateTime.Today;
                var age = now.Year - BirthDate.Year;
                if (BirthDate > now.AddYears(-age)) age--;
                return age;
            }

        }

    }
}
