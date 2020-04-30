using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Provider
    {
        public int ProviderId { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public long ProviderPhoneNumber { get; set; }

        public Inner_Facility Facility { get; set; }
        public Inner_Specialty Specialty { get; set; }
        public List<Inner_Appointment> Appointments { get; set; } = new List<Inner_Appointment>();
        public List<Inner_Insurance> Insurances { get; set; } = new List<Inner_Insurance>();
    }
}
