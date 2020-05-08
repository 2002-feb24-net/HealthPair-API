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
                    FacilityName = "Houston Methodist San Jacinto Hospital Alexander Campus",
                    FacilityAddress1 = "1700 James Bowie Drive",
                    FacilityCity = "Baytown",
                    FacilityState = "TX",
                    FacilityZipcode = 77520,
                    FacilityPhoneNumber = 2814208765
                },
                new Data_Facility()
                {
                FacilityId = 2,
                FacilityName = "Providence Hospital of North Houston LLC",
                FacilityAddress1 = "16750 Red Oak Drive",
                FacilityCity = "Houston",
                FacilityState = "TX",
                FacilityZipcode = 77090,
                FacilityPhoneNumber = 2814537110
                },
                new Data_Facility()
                {
                FacilityId = 3,
                FacilityName = "McCallen Medical Center",
                FacilityAddress1 = "301 West Expressway 83",
                FacilityCity = "McAllen",
                FacilityState = "TX",
                FacilityZipcode = 78590,
                FacilityPhoneNumber = 9566324000
                },
                new Data_Facility()
                {
                FacilityId = 4,
                FacilityName = "Faith Community Hospital",
                FacilityAddress1 = "215 Chisholm Trail",
                FacilityCity = "Jacksboro",
                FacilityState = "TX",
                FacilityZipcode = 76458,
                FacilityPhoneNumber = 9405676633
                },
                new Data_Facility()
                {
                FacilityId = 5,
                FacilityName = "Covenant Medical Center – Lakeside",
                FacilityAddress1 = "4000 24th Street",
                FacilityCity = "Lubbock",
                FacilityState = "TX",
                FacilityZipcode = 79410,
                FacilityPhoneNumber = 8067250536
                },
                new Data_Facility()
                {
                FacilityId = 6,
                FacilityName = "Woodlands Specialty Hospita",
                FacilityAddress1 = "25440 I 45 North",
                FacilityCity = "The Woodlands",
                FacilityState = "TX",
                FacilityZipcode = 77386,
                FacilityPhoneNumber = 2816028160
                },
                new Data_Facility()
                {
                FacilityId = 7,
                FacilityName = "Christus Spohn Hospital Corpus Christi Shoreline",
                FacilityAddress1 = "600 Elizabeth Street",
                FacilityCity = "Corpus Christi",
                FacilityState = "TX",
                FacilityZipcode = 78404,
                FacilityPhoneNumber = 3619024690
                },
                new Data_Facility()
                {
                FacilityId = 8,
                FacilityName = "The Corpus Christi Medical Center – Northwest",
                FacilityAddress1 = "13725 Northwest Blvd",
                FacilityCity = "Corpus Christi",
                FacilityState = "TX",
                FacilityZipcode = 78410,
                FacilityPhoneNumber = 3617674300
                },
                new Data_Facility()
                {
                FacilityId = 9,
                FacilityName = "Reagan Memorial Hospital",
                FacilityAddress1 = "1300 N Main Ave",
                FacilityCity = "Big Lake",
                FacilityState = "TX",
                FacilityZipcode = 76932,
                FacilityPhoneNumber = 3258842561
                },
                new Data_Facility()
                {
                FacilityId = 10,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "1975 Alpha, Suite 100",
                FacilityCity = "Rockwall",
                FacilityState = "TX",
                FacilityZipcode = 75087,
                FacilityPhoneNumber = 2142946200
                },
                new Data_Facility()
                {
                FacilityId = 11,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "620 South Main, Suite 100",
                FacilityCity = "Keller",
                FacilityState = "TX",
                FacilityZipcode = 76248,
                FacilityPhoneNumber = 2142946100
                },
                new Data_Facility()
                {
                FacilityId = 12,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "12500 South Freeway, Suite 100",
                FacilityCity = "Burleson",
                FacilityState = "TX",
                FacilityZipcode = 76028,
                FacilityPhoneNumber = 2142946250
                },
                new Data_Facility()
                {
                FacilityId = 13,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "1776 North Us 287, Suite 100",
                FacilityCity = "Mansfield",
                FacilityState = "TX",
                FacilityZipcode = 76063,
                FacilityPhoneNumber = 2142946300
                },
                new Data_Facility()
                {
                FacilityId = 14,
                FacilityName = "Baylor Emergency Medical Center",
                FacilityAddress1 = "5500 Colleyville Boulevard",
                FacilityCity = "Colleyville",
                FacilityState = "TX",
                FacilityZipcode = 76034,
                FacilityPhoneNumber = 2142946350
                },
                new Data_Facility()
                {
                FacilityId = 15,
                FacilityName = "Medical Center of Alliance",
                FacilityAddress1 = "3101 North Tarrant Parkway",
                FacilityCity = "Fort Worth",
                FacilityState = "TX",
                FacilityZipcode = 76177,
                FacilityPhoneNumber = 8176391100
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
                    SpecialtyId = 70,
                    Specialty = "Vascular Surgery"
                }
            );
        }
    }
}
