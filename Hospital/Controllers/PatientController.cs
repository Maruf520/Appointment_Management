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
    public class PatientController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public PatientController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPatient()
        {
            var ViewModel = new PatientRegisterViewModel();
            var genders = _unitOfWork.Gender.GetGenders();
            foreach(var gender in genders)
            {
                ViewModel.GenderList.Add(new SelectListItem()
                {
                    Value = gender.GenderId.ToString(),
                    Text = gender.GenderName
                }
                    
                    );
            }
            return View(ViewModel);
        }

        [HttpPost]

        public IActionResult RegisterPatient(PatientRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                string sMonth = DateTime.Now.ToString("MM");
                string sDay = DateTime.Now.ToString("dd");
                DateTime todaysDate = DateTime.Now.Date;
                int year = todaysDate.Year;



                var patient = new Patient
                {
                    Name = model.patient.Name,
                    Address = model.patient.Address,
                    Phone = model.patient.Phone,
                    BirthDate = model.patient.BirthDate,
                    Height = model.patient.Height,
                    Weight = model.patient.Weight,
                    GenderId = model.patient.Gender.GenderId,
                    Token = (year + sMonth + sDay + _unitOfWork.Patient.GetPatients().Count()).ToString(),

                };
                _unitOfWork.Patient.Add(patient);
                _unitOfWork.Complete();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}