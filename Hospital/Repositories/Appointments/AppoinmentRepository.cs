using Hospital.Models;
using Hospital.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repositories.Appointments
{
    public class AppoinmentRepository: IAppoinmentRepository
    {
        private readonly HospitalDbContext _hospitalDbContext;

        public AppoinmentRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }

        public IEnumerable<Appointment> GetAppoinments()
        {
            return _hospitalDbContext.Appoinments.Include(c => c.Patient).Include( p => p.Doctor).ToList();
        }

        public IEnumerable<Appointment> GetAppointmentWithPatients(int id)
        {
            return _hospitalDbContext.Appoinments.Where(c => c.Id == id).Include(c => c.Patient).Include(s => s.Doctor).ToList();
        }

        public IEnumerable<Appointment> AppointmentByDoctor(int id)
        {
            return _hospitalDbContext.Appoinments.Where(c => c.DoctorId == id).Include(c => c.Patient).ToList();
        }

         public void Add(Appointment appointment)
        {
            _hospitalDbContext.Add(appointment);
        }

        public bool ValidateAppointment(DateTime dateTime, int id)
        {
            return _hospitalDbContext.Appoinments.Any(a => a.DateTime == dateTime && a.DoctorId == id);
        }

        public Appointment GetAppointmentById(int id)
        {
            return _hospitalDbContext.Appoinments.Include(c => c.Patient).Include(c => c.Doctor).FirstOrDefault(c => c.Id == id);
        }
        public void Update(Appointment appointment)
        {
            _hospitalDbContext.Appoinments.Update(appointment);
        }

        public IQueryable< Appointment> FilterAppointments(string appointment)
        {
            var result = _hospitalDbContext.Appoinments.Include(c => c.Doctor).Include(s => s.Patient).AsQueryable();
            if(appointment != null)
            {
                if(appointment == "approved")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Approved);
                }
                else if(appointment == "rejected")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Rejected);
                }
                else if (appointment == "pending")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Submitted);
                }
                else if (appointment == "today")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Approved && c.DateTime.Year == DateTime.Now.Year && c.DateTime.Month == DateTime.Now.Month && c.DateTime.Day == DateTime.Now.Day);
                }
            }
            return result;
        }

        public IQueryable<Appointment> CustomFilterAppointment(string  name, DateTime  date)
        { 
            var result = _hospitalDbContext.Appoinments.Include(c => c.Doctor).Include(c => c.Patient).AsQueryable();
            if(name != null && date != null)
            {
                if(name == "pending")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Submitted && c.DateTime.Date == date);
                }
                else if (name == "approved")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Approved && c.DateTime.Date == date);
                }
                else if (name == "rejected")
                {
                    result = result.Where(c => c.contactStatus == ContactStatus.Rejected && c.DateTime.Date == date);
                }
            }

            return result;
        }

    }
}
