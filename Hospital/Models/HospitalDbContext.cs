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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Gender)
                .WithMany(p => p.patients)
                .HasForeignKey(p => p.GenderId); ;

            modelBuilder.Entity<Doctor>()
                .HasOne(p => p.Specialization)
                .WithMany(p => p.doctors)
                .HasForeignKey(p => p.SpecializationId);

            base.OnModelCreating(modelBuilder);

        }

    }
}
