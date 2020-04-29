using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Patient
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

        public Data_Insurance Insurance { get; set; }
        public ICollection<Data_Appointment> Appointments { get; set; } = new List<Data_Appointment>();
    }
}
