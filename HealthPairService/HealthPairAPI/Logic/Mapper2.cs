using HealthPairAPI.TransferModels;
using HealthPairDomain.InnerModels;


namespace HealthPairAPI.Logic
{
    public class Mapper
    {

// ! ***********************************
// ! ********* Appointments ************
// ! ***********************************
        public static Transfer_Appointment MapAppointments(Inner_Appointment appointment)
        {
            return new Transfer_Appointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                PatientFirstName = appointment.Patient.PatientFirstName,
                PatientLastName = appointment.Patient.PatientLastName,
                PatientPhoneNumber = appointment.Patient.PatientPhoneNumber,
                ProviderFirstName = appointment.Provider.ProviderFirstName,
                ProviderLastName = appointment.Provider.ProviderLastName,
                ProviderPhoneNumber = appointment.Provider.ProviderPhoneNumber
            };
        }

// ! ***********************************
// ! *********** Facility **************
// ! ***********************************
        public static Transfer_Facility MapFacility(Inner_Facility facility)
        {
            return new Transfer_Facility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode
            };
        }

// ! ***********************************
// ! *********** Insurance **************
// ! ***********************************
        public static Transfer_Insurance MapInsurance(Inner_Insurance insurance)
        {
            return new Transfer_Insurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
            };
        }

// ! ***********************************
// ! *********** Patient **************
// ! ***********************************
        public static Transfer_Patient MapPatient(Inner_Patient patient)
        {
            return new Transfer_Patient
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
                InsuranceName = patient.Insurance.InsuranceName
            };
        }

// ! ***********************************
// ! *********** Provider **************
// ! ***********************************
        public static Transfer_Provider MapProvider(Inner_Provider provider)
        {
            return new Transfer_Provider
            {
                ProviderId = provider.ProviderId,
                ProviderFirstName = provider.ProviderFirstName,
                ProviderLastName = provider.ProviderLastName,
                ProviderPhoneNumber = provider.ProviderPhoneNumber,
                FacilityName = provider.Facility.FacilityName,
                FacilityCity = provider.Facility.FacilityCity,
                FacilityPhoneNumber = provider.Facility.FacilityPhoneNumber,
                FacilityState = provider.Facility.FacilityState,
                Specialty = provider.Specialty.Specialty
            };
        }

// ! ***********************************
// ! ********** Specialty **************
// ! ***********************************
        public static Transfer_Specialty MapSpecialty(Inner_Specialty specialty)
        {
            return new Transfer_Specialty
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty
            };
        }
    }
}