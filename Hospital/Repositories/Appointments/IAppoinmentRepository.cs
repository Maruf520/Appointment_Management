using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Repositories.Appointments
{
    public interface IAppoinmentRepository
    {
        IEnumerable<Appointment> GetAppoinments();
        IEnumerable<Appointment> GetAppointmentWithPatients(int id);
        IEnumerable<Appointment> AppointmentByDoctor(int id);
        Appointment GetAppointmentById(int id);
        void Update(Appointment appointment);
        void Add(Appointment appointment);
        bool ValidateAppointment(DateTime datetime, int id );
    }
}
