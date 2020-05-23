using Hospital.IRepositories;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels
{
    public class DoctorFormViewModel
    {
        public DoctorFormViewModel()
        {
            SpecializationList = new List<SelectListItem>();
        }

        public List<SelectListItem> SpecializationList { get; set; }
 
        public Doctor doctor { get; set; }

        public List<int> SelectedeSepcializationId { get; set; }
        public RegisterViewModel registerViewModel { get; set; }
    }
}
