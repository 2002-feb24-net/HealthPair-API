﻿using System;
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
        public DbSet<Data_InsuranceProvider> InsuranceProviders { get; set; }

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
                    InsuranceName = "Cigna"
                },
                new Data_Insurance()
                {
                    InsuranceId = 2,
                    InsuranceName = "Humana"
                },
                new Data_Insurance()
                {
                    InsuranceId = 3,
                    InsuranceName = "UnitedHealth"
                },
                new Data_Insurance()
                {
                    InsuranceId = 4,
                    InsuranceName = "Blue Cross Blue Shield Association"
                },
                new Data_Insurance()
                {
                    InsuranceId = 5,
                    InsuranceName = "Aetna"
                },
                new Data_Insurance()
                {
                    InsuranceId = 6,
                    InsuranceName = "Anthem Inc."
                },
                new Data_Insurance()
                {
                    InsuranceId = 7,
                    InsuranceName = "Kaiser Foundation"
                },
                new Data_Insurance()
                {
                    InsuranceId = 8,
                    InsuranceName = "HCSC"
                },
                new Data_Insurance()
                {
                    InsuranceId = 9,
                    InsuranceName = "Centene"
                },
                new Data_Insurance()
                {
                    InsuranceId = 10,
                    InsuranceName = "EmblemHealth"
                },
                new Data_Insurance()
                {
                    InsuranceId = 11,
                    InsuranceName = "Wellcare"
                },
                new Data_Insurance()
                {
                    InsuranceId = 12,
                    InsuranceName = "CVS"
                },
                new Data_Insurance()
                {
                    InsuranceId = 13,
                    InsuranceName = "Molina Healthcare Inc."
                },
                new Data_Insurance()
                {
                    InsuranceId = 14,
                    InsuranceName = "Independence Health Group Inc."
                },
                new Data_Insurance()
                {
                    InsuranceId = 15,
                    InsuranceName = "Highmark Group"
                }
            );

            modelBuilder.Entity<Data_InsuranceProvider>(entity =>
            {
                entity.Property(e => e.IPId)
                    .IsRequired();
                entity.HasIndex(e => e.InsuranceId);
                entity.HasIndex(e => e.ProviderId);
                entity.HasOne(bc => bc.Insurance)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.InsuranceId);
                entity.HasOne(bc => bc.Provider)
                    .WithMany(b => b.InsuranceProviders)
                    .HasForeignKey(bc => bc.ProviderId);
            });
            modelBuilder.Entity<Data_InsuranceProvider>().HasData(
                // new Data_InsuranceProvider()
                // {
                //     IPId = 1,
                //     InsuranceId = 1,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 2,
                    InsuranceId = 1,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 3,
                //     InsuranceId = 1,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 4,
                    InsuranceId = 2,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 5,
                    InsuranceId = 2,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 6,
                    InsuranceId = 2,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 7,
                    InsuranceId = 3,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 8,
                //     InsuranceId = 3,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 9,
                    InsuranceId = 3,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 10,
                    InsuranceId = 4,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 11,
                //     InsuranceId = 4,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 12,
                    InsuranceId = 4,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 13,
                    InsuranceId = 5,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 14,
                //     InsuranceId = 5,
                //     ProviderId = 2
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 15,
                //     InsuranceId = 5,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 16,
                    InsuranceId = 6,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 17,
                //     InsuranceId = 6,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 18,
                    InsuranceId = 6,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 19,
                    InsuranceId = 7,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 20,
                //     InsuranceId = 7,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 21,
                    InsuranceId = 7,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 22,
                    InsuranceId = 8,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 23,
                //     InsuranceId = 8,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 24,
                    InsuranceId = 8,
                    ProviderId = 3
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 25,
                //     InsuranceId = 9,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 26,
                    InsuranceId = 9,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 27,
                    InsuranceId = 9,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 28,
                    InsuranceId = 10,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 29,
                //     InsuranceId = 10,
                //     ProviderId = 2
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 30,
                //     InsuranceId = 10,
                //     ProviderId = 3
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 31,
                    InsuranceId = 11,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 32,
                    InsuranceId = 11,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 33,
                    InsuranceId = 11,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 34,
                    InsuranceId = 12,
                    ProviderId = 1
                },
                new Data_InsuranceProvider()
                {
                    IPId = 35,
                    InsuranceId = 12,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 36,
                //     InsuranceId = 12,
                //     ProviderId = 3
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 37,
                //     InsuranceId = 13,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 38,
                    InsuranceId = 13,
                    ProviderId = 2
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 39,
                //     InsuranceId = 13,
                //     ProviderId = 3
                // },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 40,
                //     InsuranceId = 14,
                //     ProviderId = 1
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 41,
                    InsuranceId = 14,
                    ProviderId = 2
                },
                new Data_InsuranceProvider()
                {
                    IPId = 42,
                    InsuranceId = 14,
                    ProviderId = 3
                },
                new Data_InsuranceProvider()
                {
                    IPId = 43,
                    InsuranceId = 15,
                    ProviderId = 1
                },
                // new Data_InsuranceProvider()
                // {
                //     IPId = 44,
                //     InsuranceId = 15,
                //     ProviderId = 2
                // },
                new Data_InsuranceProvider()
                {
                    IPId = 45,
                    InsuranceId = 15,
                    ProviderId = 3
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
                    Specialty = "Addiction Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 2,
                    Specialty = "Addiction Psychiatry"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 3,
                    Specialty = "Allergy & Immunology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 4,
                    Specialty = "Alternative Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 5,
                    Specialty = "Anesthesiology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 6,
                    Specialty = "Audiology - Hearing Health"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 7,
                    Specialty = "Bariatric Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 8,
                    Specialty = "Bariatric Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 9,
                    Specialty = "Cancer Care"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 10,
                    Specialty = "Cardiac Electrophysiology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 11,
                    Specialty = "Cardiology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 12,
                    Specialty = "Cardiothoracic Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 13,
                    Specialty = "Child & Adolescent Psychiatry"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 14,
                    Specialty = "Chiropractic Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 15,
                    Specialty = "Colon & Rectal Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 16,
                    Specialty = "Critical Care Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 17,
                    Specialty = "Dentistry"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 18,
                    Specialty = "Dermatology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 19,
                    Specialty = "Emergency Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 20,
                    Specialty = "Endocrinology, Diabetes & Metabolism"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 21,
                    Specialty = "Family Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 22,
                    Specialty = "Gastroenterology - Digestive Health"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 23,
                    Specialty = "General Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 24,
                    Specialty = "Genetics"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 25,
                    Specialty = "Geriatric Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 26,
                    Specialty = "Gynecologic Oncology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 27,
                    Specialty = "Gynecology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 28,
                    Specialty = "Head & Neck Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 29,
                    Specialty = "Hematology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 30,
                    Specialty = "Hospice/Palliative Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 31,
                    Specialty = "Hospital Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 32,
                    Specialty = "Infectious Disease"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 33,
                    Specialty = "Internal Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 34,
                    Specialty = "Maternal & Fetal Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 35,
                    Specialty = "Neonatal - Perinatal Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 36,
                    Specialty = "Nephrology - Kidney Health"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 37,
                    Specialty = "Neurology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 38,
                    Specialty = "Neuropsychology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 39,
                    Specialty = "Neuroradiology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 40,
                    Specialty = "Neurosurgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 41,
                    Specialty = "Obstetrics"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 42,
                    Specialty = "Ophthalmology - Eye Disease"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 43,
                    Specialty = "Optometry"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 44,
                    Specialty = "Oral & Maxillofacial Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 45,
                    Specialty = "Orthodontics"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 46,
                    Specialty = "Orthopedics"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 47,
                    Specialty = "Orthopedic Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 48,
                    Specialty = "Otolaryngology - Ear, Nose & Throat"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 49,
                    Specialty = "Pathology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 50,
                    Specialty = "Pediatrics"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 51,
                    Specialty = "Physical Medicine & Rehabilitation"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 52,
                    Specialty = "Plastic/Cosmetic Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 53,
                    Specialty = "Podiatry - Foot Health"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 54,
                    Specialty = "Primary Care"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 55,
                    Specialty = "Psychiatry"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 56,
                    Specialty = "Psychology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 57,
                    Specialty = "Public Health and General Preventive Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 58,
                    Specialty = "Pulmonary Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 59,
                    Specialty = "Radiation Oncology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 60,
                    Specialty = "Radiology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 61,
                    Specialty = "Reproductive Endocrinology/Infertility"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 62,
                    Specialty = "Rheumatology - Arthritis"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 63,
                    Specialty = "Sleep Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 64,
                    Specialty = "Sports Medicine"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 65,
                    Specialty = "Surgical Critical Care - Trauma"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 66,
                    Specialty = "Surgical Oncology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 67,
                    Specialty = "Thoracic Surgery"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 68,
                    Specialty = "Urgent Care"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 69,
                    Specialty = "Urology"
                },
                new Data_Specialty()
                {
                    SpecialtyId = 70,
                    Specialty = "Vascular Surgery"
                }
            );
        }
    }
}
