using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class PatientRegisterViewModel
    {
        public PatientRegisterViewModel()
        {
            GenderList = new List<SelectListItem>();
        }
        public Patient patient { get; set; }

        public List<SelectListItem> GenderList { get; set; }


    }
}
