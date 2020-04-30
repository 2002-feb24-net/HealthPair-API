using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerPatient
    {
        public int PatientId { get; set; }
        public int InsuranceId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientAddress1 { get; set; }
        public string PatientCity { get; set; }
        public string PatientState { get; set; }
        public int PatientZipcode { get; set; }
        public DateTime PatientBirthDay { get; set; }
        public long PatientPhoneNumber { get; set; }

        public InnerInsurance Insurance { get; set; }
        public ICollection<InnerAppointment> Appointments { get; set; } = new List<InnerAppointment>();
    }
}
