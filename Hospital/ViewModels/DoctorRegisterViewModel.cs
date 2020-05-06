using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class DoctorRegisterViewModel
    {
        public DoctorRegisterViewModel()
        {
            SpecializationList = new List<SelectListItem>();
        }
        public  Doctor Doctor { get; set; }
        public List<SelectListItem> SpecializationList { get; set; }
    }
}
