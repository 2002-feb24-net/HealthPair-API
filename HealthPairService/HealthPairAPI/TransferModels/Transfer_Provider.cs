using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Provider
    {
        public int ProviderId { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public long ProviderPhoneNumber { get; set; }


        public string FacilityName { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public long FacilityPhoneNumber { get; set; }


        public string Specialty { get; set; }
    }
}
