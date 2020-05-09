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
        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public IActionResult Index()
        {
            var allDoctor = _unitOfWork.doctorRepository.GetDoctors();
            var model = new DoctorDetailsViewModel();
            model.doctor = allDoctor.ToList(); 
            return View(model);
        }

        //only for admin 
           public async Task<IActionResult> Edit(int id)
        {
            var user = _unitOfWork.doctorRepository.GetDoctorById(id);
            var specializations = _unitOfWork.specializationRepository.GetSpecializations();
            var model = new DoctorRegisterViewModel
            {
                Doctor = user
            };
            foreach(var item in specializations)
            {
                model.SpecializationList.Add(new SelectListItem()
                {
                    Value = item.SpecializationId.ToString(),
                    Text = item.Name
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorRegisterViewModel model)
        {
            var user = _unitOfWork.doctorRepository.GetDoctorById(model.Doctor.Id);
            var doctor = new Doctor
            {
                Address = user.Address,

                Phone = user.Phone,
                SpecializationId = user.SpecializationId,
                Name = user.Name,
            };
            _unitOfWork.Complete();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var user = _unitOfWork.doctorRepository.GetDoctorById(id);
            _unitOfWork.doctorRepository.Remove(user);
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        }
    }
