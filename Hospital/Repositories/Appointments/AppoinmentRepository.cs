using Hospital.Models;
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
    }
}
