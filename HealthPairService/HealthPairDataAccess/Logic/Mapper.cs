using HealthPairDataAccess.DataModels;
using HealthPairDomain.InnerModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDataAccess.Logic
{
    public class Mapper
    {
        //Appointments

        public static InnerAppointment MapAppointments(Data_Appointment appointment)
        {
            return new InnerAppointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                PatientId = appointment.PatientId,
                ProviderId = appointment.ProviderId,
                //Patient = 
                //Provider =
            };
        }

        public static Data_Appointment MapAppointments(InnerAppointment appointment)
        {
            return new Data_Appointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                PatientId = appointment.PatientId,
                ProviderId = appointment.ProviderId,
                //Patient = 
                //Provider =
            };
        }

        //Facility
        public static InnerFacility MapFacility(Data_Facility facility)
        {
            return new InnerFacility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode,
                //Providers
            };
        }

        public static Data_Facility MapFacility(InnerFacility facility)
        {
            return new Data_Facility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode,
                //Providers
            };
        }

        //Insurance
        public static InnerInsurance MapInsurance(Data_Insurance insurance)
        {
            return new InnerInsurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
                //InsuranceProviders
                //Patients
            };
        }

        public static Data_Insurance MapInsurance(InnerInsurance insurance)
        {
            return new Data_Insurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
                //InsuranceProviders
                //Patients
            };
        }

        //Patient
        public static InnerPatient MapPatient(Data_Patient patient)
        {
            return new InnerPatient
            {
                PatientId = patient.PatientId,
                PatientAddress1 = patient.PatientAddress1,
                PatientBirthDay = patient.PatientBirthDay,
                PatientCity = patient.PatientCity,
                PatientFirstName = patient.PatientFirstName,
                PatientZipcode = patient.PatientZipcode,
                PatientLastName = patient.PatientLastName,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientState = patient.PatientState,
                InsuranceId = patient.InsuranceId,
                //Appointments = MapAppointments(patient.Appointments)
                //Insurance
            };
        }

        public static Data_Patient MapPatient(InnerPatient patient)
        {
            return new Data_Patient
            {
                PatientId = patient.PatientId,
                PatientAddress1 = patient.PatientAddress1,
                PatientBirthDay = patient.PatientBirthDay,
                PatientCity = patient.PatientCity,
                PatientFirstName = patient.PatientFirstName,
                PatientZipcode = patient.PatientZipcode,
                PatientLastName = patient.PatientLastName,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientState = patient.PatientState,
                InsuranceId = patient.InsuranceId,
                //Appointments = MapAppointments(patient.Appointments)
                //Insurance
            };
        }

        //Provider
        public static Data_Provider MapProvier(InnerProvider provider)
        {
            return new Data_Provider
            {
                ProviderId = provider.ProviderId,
                ProviderFirstName = provider.ProviderFirstName,
                ProviderLastName = provider.ProviderLastName,
                ProviderPhoneNumber = provider.ProviderPhoneNumber,
                
                //Facility
                FacilityId = provider.FacilityId,
                //Facility =
                //Appointment
                //Specialty
                //Specialty =
                SpecialtyId = provider.SpecialtyId,


            };
        }

        public static InnerProvider MapProvier(Data_Provider provider)
        {
            return new InnerProvider
            {
                ProviderId = provider.ProviderId,
                ProviderFirstName = provider.ProviderFirstName,
                ProviderLastName = provider.ProviderLastName,
                ProviderPhoneNumber = provider.ProviderPhoneNumber,

                //Facility
                FacilityId = provider.FacilityId,
                //Facility =
                //Appointment
                //Specialty
                //Specialty =
                SpecialtyId = provider.SpecialtyId,
            };
        }

        public static InnerSpeciality MapSpecialty(Data_Specialty specialty)
        {
            return new InnerSpeciality
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty,
                //Providers
            };
        }

        public static Data_Specialty MapSpecialty(InnerSpeciality specialty)
        {
            return new Data_Specialty
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty,
                //Providers
            };
        }

        //Insurance Provider
        public static InnerInsuranceProvider MapInsuranceProvider(Data_InsuranceProvider insuranceProvider)
        {
            return new InnerInsuranceProvider
            {
                InsuranceId = insuranceProvider.InsuranceId,
                ProviderId = insuranceProvider.ProviderId,
                //Insurance
                //Provider
            };
        }

        public static Data_InsuranceProvider MapInsuranceProvider(InnerInsuranceProvider insuranceProvider)
        {
            return new Data_InsuranceProvider
            {
                InsuranceId = insuranceProvider.InsuranceId,
                ProviderId = insuranceProvider.ProviderId,
                //Insurance
                //Provider
            };
        }
    }
}
