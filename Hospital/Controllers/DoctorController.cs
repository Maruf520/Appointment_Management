using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HospitalDbContext _context;
        public DoctorController(IUnitOfWork unitOfWork, HospitalDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
           
        }
        
        public IActionResult Index()
        {
/*            var model = new DoctorDetailsViewModel();
            model.doctor = allDoctor.ToList();*/
            return View();
        }
        [HttpGet]
        public IActionResult display()
        {
            var allDoctor = _unitOfWork.doctorRepository.GetDoctorList();
            var adoc = allDoctor.ToList();
            return Json(new {  data = adoc});
        }
        //only for admin 
           public async Task<IActionResult> Edit(int id)
        {
            var user = _unitOfWork.doctorRepository.GetDoctorById(id);
            var specializations = _unitOfWork.specializationRepository.GetSpecializations();
            var model = new DoctorFormViewModel
            {
                doctor = user
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
        public async Task<IActionResult> DoctorRegistration()
        {
            var specializations = _unitOfWork.specializationRepository.GetSpecializations();
            var model = new DoctorFormViewModel();
            foreach (var Specialization in specializations)
            {
                model.SpecializationList.Add(new SelectListItem()
                {
                    Value = Specialization.SpecializationId.ToString(),

                    Text = Specialization.Name

                }

                    );
            }


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRegistration(DoctorFormViewModel model)
        {
           
            Doctor doctorr = new Doctor()
            {
                Address = model.doctor.Address,
                Phone = model.doctor.Phone,
                Name = model.doctor.Name,
                IsAvailable = true,


            };
            var iss = model.doctor.Id;

            _context.Doctors.Add(doctorr);

     
            await _context.SaveChangesAsync();
            var doctorId = doctorr.Id;

            foreach (var id in model.SelectedeSepcializationId)
            {
                _context.DoctorSpecializations.Add(new DoctorSpecialization
                {
                    SpecializationId = id,
                    DoctorId = doctorId
                });
                
            }
            await _context.SaveChangesAsync();



            return RedirectToAction("Index", "Home");

        }

/*        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var specializations = _unitOfWork.specializationRepository.GetSpecializations();
            var doctorObj = _unitOfWork.doctorRepository.GetDoctorById(id);
            var model = new DoctorFormViewModel
            {
                doctor = doctorObj
            };

            foreach (var specialization in specializations)
            {
                model.SpecializationList.Add(new SelectListItem()
                {
                    Value = specialization.SpecializationId.ToString(),

                    Text = specialization.Name
                }); ;
            }
            return View(model);
        }
*/
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorFormViewModel model)
        {
            var user = _unitOfWork.doctorRepository.GetDoctorById(model.doctor.Id);
            user.Name = model.doctor.Name;
            user.Phone = model.doctor.Phone;
            user.Address = model.doctor.Address;
            user.SpecializationId = model.doctor.SpecializationId;

            _unitOfWork.Complete();

            return RedirectToAction("Index", "Doctor");
        }

        public async Task<IActionResult> Details(int id)
        {
            var detail =await _context.Doctors.Where(c => c.Id == id).Include(c => c.doctorSpecializations).FirstOrDefaultAsync();
                var doc = new doctorDetails
                {
                    Doctor = detail,
                };
            ViewBag.specialization = _context.DoctorSpecializations.Where(s => s.DoctorId == id).Select(s => new doctorDetails { 

                specializationName = s.Specialization.Name
            
            });

            
            return View(doc);
        }

        public IActionResult All()
        {
            var al = _context.Doctors.ToList();
            return new JsonResult(al);
        }
        [HttpGet]
        public IActionResult DoctorAvailability(int id)
        {
            var doctor = _unitOfWork.doctorRepository.GetDoctorById(id);
            if(doctor.IsAvailable == true)
            {
                doctor.IsAvailable = false;
            }
            else
            {
                doctor.IsAvailable = true;
            }
            
            _unitOfWork.Complete();
            return RedirectToAction("Details", "Doctor", new { id });
        }

    }
}
