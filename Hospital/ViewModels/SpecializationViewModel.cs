using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class SpecializationViewModel
    {
        public IEnumerable<Specialization> Specializations { get; set; }
    }
}
