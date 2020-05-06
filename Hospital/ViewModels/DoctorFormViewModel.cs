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
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsAvailable { get; set; }

        public List<SelectListItem> SpecializationList { get; set; }
        [Required]
        public int Specialization { get; set; }

        public IEnumerable<Specialization> Specializations { get; set; }
        public Doctor doctor { get; set; }


        public RegisterViewModel registerViewModel { get; set; }
    }
}
