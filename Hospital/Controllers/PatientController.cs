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
            var  Patientts = new PatientViewModel();
            var allPatient = _unitOfWork.Patient.GetPatients();
            Patientts.Patients = allPatient.ToList();
            /*            foreach (var item in allPatient)
                        {
                            Patientts.Patients.Add(new Patient() { 
                            Phone = item.Phone,
                            Name = item.Name,
                            Token = item.Token,
                            Gender = item.Gender,
                            DateTime = item.DateTime,
                            Id = item.Id,
                            Height = item.Height,
                            Weight = item.Weight,
                            GenderId = item.GenderId,
                            }); 
                        }*/

            /*                            foreach (var item in allPatient)
                                        {
                                            Patientts.Patients.Add(item);
                                        }*/
        /*    Patientts.Patients = allPatient.ToList();*/
            return View(Patientts);
        }

        public IActionResult displayPatient()
        {
           var  allPatient = _unitOfWork.Patient.GetPatientList();
            return Json(new { data = allPatient.ToList() });
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
                    GenderId = model.patient.GenderId,
                    Token = (year + sMonth + sDay + _unitOfWork.Patient.GetPatients().Count()).ToString(),

                };
                _unitOfWork.Patient.Add(patient);
                _unitOfWork.Complete();
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            
            var patientdtails = new PatientDetailsViewModel
            {
                Patient = _unitOfWork.Patient.GetPatientById(id),
            };
            return View(patientdtails);
        }

        public IActionResult Edit (int id)
        {
            var patientDetails = _unitOfWork.Patient.GetPatientById(id);

            var model = new PatientRegisterViewModel
            {
                patient = patientDetails,

            };
            
            var genders = _unitOfWork.Gender.GetGenders();
            foreach (var gender in genders)
            {
                model.GenderList.Add(new SelectListItem()
                {
                    Value = gender.GenderId.ToString(),
                    Text = gender.GenderName
                }

                    );
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PatientRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {


                var patientObj = _unitOfWork.Patient.GetPatientById(model.patient.Id);
                patientObj.Name = model.patient.Name;
                patientObj.Address = model.patient.Address;
                patientObj.Phone = model.patient.Phone;
                patientObj.GenderId = model.patient.GenderId;
                patientObj.Weight = model.patient.Weight;
                patientObj.Height = model.patient.Height;
                patientObj.BirthDate = model.patient.BirthDate;

                /*                _unitOfWork.Patient.Update(model.patient);*/
                _unitOfWork.Complete();

            }
            return RedirectToAction("Details", "Patient", new { @id = model.patient.Id });
            /* return RedirectToAction(nameof(Index));*/

        }
       [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = _unitOfWork.Patient.GetPatientById(id);
            _unitOfWork.Patient.Remove(patient);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}