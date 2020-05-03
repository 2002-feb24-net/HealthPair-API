using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace HealthPairDataAccess.DataModels
{
    public class HealthPairContext : DbContext
    {
        public HealthPairContext(DbContextOptions<HealthPairContext> options) : base(options)
        {
        }

        public DbSet<Data_Patient> Patients { get; set; }
        public DbSet<Data_Appointment> Appointments { get; set; }
        public DbSet<Data_Insurance> Insurances { get; set; }
        public DbSet<Data_Provider> Providers { get; set; }
        public DbSet<Data_Specialty> Specialties { get; set; }
        public DbSet<Data_Facility> Facilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data_Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .IsRequired();
                entity.HasIndex(e => e.PatientId);
                entity.HasIndex(e => e.ProviderId);
                entity.Property(e => e.AppointmentDate)
                    .IsRequired();
                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Appointments)
                    .HasForeignKey(e => e.PatientId);
                entity.HasOne(e => e.Provider)
                    .WithMany(e => e.Appointments)
                    .HasForeignKey(e => e.ProviderId);
            });
            modelBuilder.Entity<Data_Appointment>().HasData(
                new Data_Appointment()
                {
                    AppointmentId = 1,
                    PatientId = 3,
                    ProviderId = 1,
                    AppointmentDate = new DateTime(2020, 4, 29)
                },
                new Data_Appointment()
                {
                    AppointmentId = 2,
                    PatientId = 2,
                    ProviderId = 3,
                    AppointmentDate = new DateTime(2020, 6, 29)
                },
                new Data_Appointment()
                {
                    AppointmentId = 3,
                    PatientId = 1,
                    ProviderId = 2,
                    AppointmentDate = new DateTime(2020, 5, 29)
                }
            );

            modelBuilder.Entity<Data_Facility>(entity =>
            {
                entity.Property(e => e.FacilityId)
                    .IsRequired();
                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.FacilityAddress1)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.FacilityCity)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.FacilityState)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.FacilityZipcode)
                    .IsRequired();
                entity.Property(e => e.FacilityPhoneNumber)
                    .IsRequired();
            });
            modelBuilder.Entity<Data_Facility>().HasData(
                new Data_Facility()
                {
                    FacilityId = 1,
                    FacilityName = "Test Facility Name",
                    FacilityAddress1 = "123 Test Street",
                    FacilityCity = "Test City",
                    FacilityState = "Test State",
                    FacilityZipcode = 12345,
                    FacilityPhoneNumber = 1234567890
                },
                new Data_Facility()
                {
                    FacilityId = 2,
                    FacilityName = "My Facility",
                    FacilityAddress1 = "Best Place Lane",
                    FacilityCity = "The City",
                    FacilityState = "Wonder State",
                    FacilityZipcode = 14556,
                    FacilityPhoneNumber = 6715927402
                },
                new Data_Facility()
                {
                    FacilityId = 3,
                    FacilityName = "Facility Three",
                    FacilityAddress1 = "Your Current Location",
                    FacilityCity = "Your City",
                    FacilityState = "Your State",
                    FacilityZipcode = 72910,
                    FacilityPhoneNumber = 2852019634
                }
            );

            modelBuilder.Entity<Data_Insurance>(entity =>
            {
                entity.Property(e => e.InsuranceId)
                    .IsRequired();
                entity.Property(e => e.InsuranceName)
                    .IsRequired()
                    .HasMaxLength(80);
            });
            modelBuilder.Entity<Data_Insurance>().HasData(
                new Data_Insurance()
                {
                    InsuranceId = 1,
                    InsuranceName = "Test Insurance Name"
                },
                new Data_Insurance()
                {
                    InsuranceId = 2,
                    InsuranceName = "Best Insurance"
                },
                new Data_Insurance()
                {
                    InsuranceId = 3,
                    InsuranceName = "Worst Insurance"
                }
            );

            modelBuilder.Entity<Data_InsuranceProvider>(entity =>
            {
                entity.HasKey(bc => new { bc.InsuranceId, bc.ProviderId });
                entity.HasOne(bc => bc.Insurance)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.InsuranceId);
                entity.HasOne(bc => bc.Provider)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.ProviderId);
            });
            modelBuilder.Entity<Data_InsuranceProvider>().HasData(
                new Data_InsuranceProvider()
                {
                    InsuranceId = 1,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    InsuranceId = 1,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    InsuranceId = 3,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    InsuranceId = 2,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    InsuranceId = 3,
                    ProviderId = 2
                }
            );

            modelBuilder.Entity<Data_Patient>(entity =>
            {
                entity.Property(e => e.PatientId)
                    .IsRequired();
                entity.HasIndex(e => e.InsuranceId);
                entity.Property(e => e.PatientFirstName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientLastName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientPassword)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.PatientAddress1)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.PatientCity)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.PatientState)
                    .IsRequired()
                    .HasMaxLength(40);
                entity.Property(e => e.PatientZipcode)
                    .IsRequired();
                entity.Property(e => e.PatientBirthDay)
                    .IsRequired();
                entity.Property(e => e.PatientPhoneNumber)
                    .IsRequired();
                entity.Property(e => e.PatientEmail)
                    .IsRequired()
                    .HasMaxLength(120);
                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValue(false);
            });
            modelBuilder.Entity<Data_Patient>().HasData(
                new Data_Patient()
                {
                    PatientId = 1,
                    InsuranceId = 1,
                    PatientFirstName = "TestPatientFirstName",
                    PatientLastName = "TestPatientLastName",
                    PatientPassword = "TestPassword",
                    PatientAddress1 = "123 Test Street",
                    PatientCity = "Test City",
                    PatientState = "Test State",
                    PatientZipcode = 12345,
                    PatientBirthDay = new DateTime(2000, 1, 1),
                    PatientPhoneNumber = 1234567890,
                    PatientEmail = "TestEmail@test.com",
                    IsAdmin = true
                },
                new Data_Patient()
                {
                    PatientId = 2,
                    InsuranceId = 1,
                    PatientFirstName = "Bob",
                    PatientLastName = "Sagget",
                    PatientPassword = "password123",
                    PatientAddress1 = "123 Holiday Lane",
                    PatientCity = "Georgetown",
                    PatientState = "Georgia",
                    PatientZipcode = 55567,
                    PatientBirthDay = new DateTime(2000, 1, 1),
                    PatientPhoneNumber = 1234567890,
                    PatientEmail = "dontemailme@emailcompany.com",
                    IsAdmin = false
                },
                new Data_Patient()
                {
                    PatientId = 3,
                    InsuranceId = 2,
                    PatientFirstName = "Daffy",
                    PatientLastName = "Duck",
                    PatientPassword = "L00nyT00nz",
                    PatientAddress1 = "Sesame Street",
                    PatientCity = "London",
                    PatientState = "California",
                    PatientZipcode = 11111,
                    PatientBirthDay = new DateTime(2000, 1, 1),
                    PatientPhoneNumber = 5555555555,
                    PatientEmail = "rickrolled@rick.com",
                    IsAdmin = false
                }
            );

            modelBuilder.Entity<Data_Provider>(entity =>
            {
                entity.Property(e => e.ProviderId)
                    .IsRequired();
                entity.HasIndex(e => e.FacilityId);
                entity.HasIndex(e => e.SpecialtyId);
                entity.Property(e => e.ProviderFirstName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.ProviderLastName)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.ProviderPhoneNumber)
                    .IsRequired();
                entity.HasOne(p => p.Facility)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(p => p.FacilityId);
                entity.HasOne(p => p.Specialty)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(p => p.SpecialtyId);
            });
            modelBuilder.Entity<Data_Provider>().HasData(
                new Data_Provider()
                {
                    ProviderId = 1,
                    FacilityId = 1,
                    SpecialtyId = 1,
                    ProviderFirstName = "TestProviderFirstName",
                    ProviderLastName = "TestProviderLastName",
                    ProviderPhoneNumber = 1234567890
                },
                new Data_Provider()
                {
                    ProviderId = 2,
                    FacilityId = 3,
                    SpecialtyId = 2,
                    ProviderFirstName = "Deborah",
                    ProviderLastName = "White",
                    ProviderPhoneNumber = 6483932283
                },
                new Data_Provider()
                {
                    ProviderId = 3,
                    FacilityId = 2,
                    SpecialtyId = 1,
                    ProviderFirstName = "Billy",
                    ProviderLastName = "Joe",
                    ProviderPhoneNumber = 6667778888
                }
            );

            modelBuilder.Entity<Data_Specialty>(entity =>
            {
                entity.Property(e => e.SpecialtyId)
                    .IsRequired();
                entity.Property(e => e.Specialty)
                    .IsRequired()
                    .HasMaxLength(80);
            });
            modelBuilder.Entity<Data_Specialty>().HasData(
                new Data_Specialty()
                {
                    SpecialtyId = 1,
                    Specialty = "Test Specialty"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 2,
                    Specialty = "Legos"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 3,
                    Specialty = "Guns"
                }
            );
        }
    }
}
