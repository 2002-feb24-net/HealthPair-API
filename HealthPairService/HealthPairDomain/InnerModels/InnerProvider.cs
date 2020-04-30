using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerProvider
    {
        public int ProviderId { get; set; }
        public int FacilityId { get; set; }
        public int SpecialtyId { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public long ProviderPhoneNumber { get; set; }

        public InnerFacility Facility { get; set; }
        //public Data_Specialty Specialty { get; set; }
        public ICollection<InnerAppointment> Appointments { get; set; } = new List<InnerAppointment>();
        public ICollection<InnerInsuranceProvider> InsuranceProviders { get; set; } = new List<InnerInsuranceProvider>();
    }
}
