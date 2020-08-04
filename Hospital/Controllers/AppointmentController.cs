using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Hospital.Models.Patient;

namespace Hospital.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IUnitOfWork unitOfWork, HospitalDbContext context, ILogger<AppointmentController> logger)
        {
            _unitOfWork = unitOfWork;

            _logger = logger;
        }


        public IActionResult Index()
        {
            var appoint = _unitOfWork.specializationRepository.GetSpecializations();
            SpecializationViewModel specializationViewModel = new SpecializationViewModel
            {
                Specializations = appoint
            };
            return View(specializationViewModel);
        }



        public IActionResult  Create(int id)
        {
            var allDoctor = _unitOfWork.doctorRepository.GetAvailableDoctors();
            _logger.LogWarning("{allDoctor} is inside this method",allDoctor);
            
            var viewmodel = new AppointmentFormViewModel
            {
                Patient = id,
                Heading = "New Appointment",
            };
            foreach (var doctor in allDoctor)
            {
                viewmodel.DoctorList.Add(new SelectListItem()
                {
                    Value = doctor.Id.ToString(),
                    Text = doctor.Name
                });
            }
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Create (AppointmentFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var allDoctor = _unitOfWork.doctorRepository.GetAvailableDoctors();
                var viewmodel = new AppointmentFormViewModel
                {
                    Patient = model.Patient,
                    Heading = "New Appointment",
                };
                foreach (var doctor in allDoctor)
                {
                    viewmodel.DoctorList.Add(new SelectListItem()
                    {
                        Value = doctor.Id.ToString(),
                        Text = doctor.Name
                    });
                }
                return View(viewmodel);
            }

            var appointment = new Appointment
            {
                DateTime = model.GetStartDateTime(),
                Details = model.Detail,
                Status = Status.Submitted,
               
                DoctorId = model.Doctor,
            };
            _unitOfWork.appoinmentRepository.Add(appointment);
            _unitOfWork.Complete();

            return RedirectToAction("Appointments", "Appointment");
        }

        public async Task<IActionResult> Appointments()
        {
            var allapointments = _unitOfWork.appoinmentRepository.GetAppoinments();
            var appoint = new AppointmentViewModel();
            appoint.Appointments = allapointments.ToList();

            return View(appoint);
        }

        public IActionResult Details(int id)
        {
            var details = _unitOfWork.appoinmentRepository.GetAppointmentWithPatients(id);
            
            _logger.LogInformation(message:"{details} all details here", details.FirstOrDefault().Patient.Name);
      
            var appo = new AppointmentDetailsViewModel
            {
            
                Appointment = details.FirstOrDefault()
            };

            return View(appo);
        }

        [HttpGet]
        public async Task<IActionResult> Approval(int id)
        {

            var appointment = _unitOfWork.appoinmentRepository.GetAppointmentById(id);
            var d = appointment.Id;
            _logger.LogInformation("{d} Appointment id for Approval",d);

            appointment.Status = Status.Approved;
            _unitOfWork.Complete();
            return RedirectToAction("Appointments","Appointment");
            
            
        }
    

    [HttpGet]
    public async Task<IActionResult> Rejection(int id)
    {

        var appointment = _unitOfWork.appoinmentRepository.GetAppointmentById(id);
            var d = appointment.Id;
            _logger.LogInformation("{d} Appointment id for Rejection",d);
            
        appointment.Status = Status.Rejected;
        _unitOfWork.Complete();
        return RedirectToAction("Appointments", "Appointment");


    }

        public ViewResult List(string appointment)
        {

            var app = _unitOfWork.appoinmentRepository.FilterAppointments(appointment);

            var viewmodel = new AppointmentViewModel
            {
                Appointment = app,
            };

            return View(viewmodel);
        }

        public async Task<IActionResult> Search()
        {
            return View();
        }
        public IActionResult SearchList()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchList(AppointmentViewModel model)
        {
            var result = _unitOfWork.appoinmentRepository.CustomFilterAppointment(model.StringName, model.Date);

            var viewmodel = new AppointmentViewModel
            {
                Appointment = result,
                StringName = model.StringName,
                Date = model.Date,
            };
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Excel(AppointmentViewModel model)
        {
            var appoint = _unitOfWork.appoinmentRepository.CustomFilterAppointment(model.StringName, model.Date);
            using (var workbook = new XLWorkbook())
            {
               
                var worksheet = workbook.Worksheets.Add("Appointments");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Token";
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Phone";
                worksheet.Cell(currentRow, 4).Value = "Date";
                worksheet.Cell(currentRow, 5).Value = "Time";
                
                foreach(var item in appoint)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Patient.Token;
                    worksheet.Cell(currentRow, 2).Value = item.Patient.Name;
                    worksheet.Cell(currentRow, 3).Value = item.Patient.Phone;
                    worksheet.Cell(currentRow, 4).Value = item.DateTime.Date.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 5).Value = item.DateTime.ToLocalTime().TimeOfDay;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Appointment List.xlsx");
                }
            }
        }

        public IActionResult SelectSpecialization(string name)
        {
          
            var doctors = _unitOfWork.doctorRepository.GetDoctors(name);

            var model = new DoctorViewModel();

            model.Doctors = doctors.ToList();

            return View(model);
            
        }

        
        public  IActionResult SelectDateForAppontment(int id)
        {
            var doc = _unitOfWork.doctorRepository.GetIndividualDoctor(id);
            var model = new TimeSlotViewModel();
            model.DoctorId = doc.Id;
            
            return View(model);
        }


        public IActionResult SelectTimeSlot(TimeSlotViewModel modelVm)
        {
            var approvedAppointments = _unitOfWork.appoinmentRepository.ApprovedAppointment(modelVm.Date.Value.Date,modelVm.DoctorId);
            var specialistTimeSlots = _unitOfWork.appoinmentRepository.GetTimeSlots(modelVm.DoctorId);

            var availableTimeSlots = new List<TimeSlot>();
            var model = new TimeSlotViewModel();
            model.Date = modelVm.Date;
            if (!(approvedAppointments == null))
            {

                foreach (var timeSlot in specialistTimeSlots)
                {
                    foreach (var appointment in approvedAppointments)
                    {
                        if (!(appointment.StartTime == timeSlot.StartTime && appointment.EndTime == timeSlot.EndTime))
                        {
                            availableTimeSlots.Add(timeSlot);
                            break;
                        }

                    }
                }
            }

            if ( approvedAppointments.Count().Equals(0))
            {


                foreach (var timeslot in specialistTimeSlots)
                {
                    availableTimeSlots.Add(timeslot);
                }

            }
        
            

            model.TimeSlots = availableTimeSlots;

            var doctor = _unitOfWork.doctorRepository.GetDoctorById(modelVm.DoctorId);

            model.Doctor = doctor;

            model.Name = doctor.Name;

            return View(model);
        }



        [HttpGet]
        public IActionResult GetAppointment (int doctorid, int timeslotid, DateTime Date)
        
        {
            var timeId = _unitOfWork.appoinmentRepository.GetTimeSlots(timeslotid).FirstOrDefault();


            var userId  = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.Patient.GetPatientById(userId);
            if(user != null)
            {
                var modelVM = new TimeSlotViewModel
                {
                    patient = user,
                    StartTime = timeId.StartTime,
                    EndTime = timeId.EndTime,
                    PatientId = userId,
                    DoctorId = doctorid,
                    Date = Date,
                };

                return View(modelVM);
            }


            var model = new TimeSlotViewModel
            {
                StartTime = timeId.StartTime,
                EndTime = timeId.EndTime,
                PatientId = userId,
                DoctorId = doctorid,
                Date = Date,
               
                
            };
            var gen = new List<SelectListItem>();

            gen.Add(new SelectListItem()
            {
                Text = "Select",
                Value = ""
            }
                );

            foreach (Gender item in Enum.GetValues(typeof(Gender)))
            {
                gen.Add(new SelectListItem { Text = Enum.GetName(typeof(Gender), item), Value = item.ToString() });
            }
            ViewBag.gender = gen;
            return View(model);
        }



        [HttpPost]
        public IActionResult ConfirmAppointment(TimeSlotViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _unitOfWork.Patient.GetPatientById(model.PatientId);
                if (user == null)
                {

              

                    Patient patient = new Patient
                    {
                        Name = model.patient.Name,
                        Phone = model.patient.Phone,
                        BirthDate = model.patient.BirthDate,
                        PatientId = model.PatientId,
                        gender = model.patient.gender,
                        Address = model.patient.Address,
                        Height = model.patient.Height,
                        Weight = model.patient.Weight,
                        Token = model.patient.Token


                    };

                    _unitOfWork.Patient.Add(patient);
                    _unitOfWork.Complete();
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Appointment appointment = new Appointment
                {
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Details = model.Details,
                    Status = Status.Submitted,
                    DoctorId = model.DoctorId,
                    PatientId = userId,
                    DateTime = model.Date.GetValueOrDefault()

                };
                _unitOfWork.appoinmentRepository.Add(appointment);
                _unitOfWork.Complete();
               
            }
            return RedirectToAction("Index", "Appointment");
        }


    }
}

