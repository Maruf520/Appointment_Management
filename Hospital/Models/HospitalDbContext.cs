using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class HospitalDbContext : IdentityDbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations {get;set;}
        public DbSet<Appointment> Appoinments { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Gender)
                .WithMany(p => p.patients)
                .HasForeignKey(p => p.GenderId); ;


            modelBuilder.Entity<DoctorSpecialization>()
                .HasKey(x => new { x.SpecializationId, x.DoctorId });


            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(p => p.Doctor)
                .WithMany(p => p.doctorSpecializations)
                .HasForeignKey(p => p.DoctorId);


            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(p => p.Specialization)
                .WithMany(p => p.doctorSpecializations)
                .HasForeignKey(p => p.SpecializationId); ;



            base.OnModelCreating(modelBuilder);

        }
         
    }
}
