using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HospitalDbContext _hospitalDbContext;
        public DoctorController(IUnitOfWork unitOfWork, HospitalDbContext hospitalDbContext)
        {
            _unitOfWork = unitOfWork;
            _hospitalDbContext = hospitalDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        //only for admin 
        [HttpGet]
        public IActionResult RegisterDoctor()
        {
            var Specializations = _hospitalDbContext.Specializations;
            var doctorRegisterViewModel = new DoctorRegisterViewModel();

                foreach (var specializations in Specializations)
                {
                doctorRegisterViewModel.SpecializationList.Add(new SelectListItem()
                {
                    Value = specializations.Id.ToString(),
                    Text = specializations.Name,
                }
                    );
                }

                    return View(doctorRegisterViewModel);
            }
        }
    }
