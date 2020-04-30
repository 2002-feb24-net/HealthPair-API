using System.Collections.Generic;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Facility
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress1 { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public int FacilityZipcode { get; set; }
        public long FacilityPhoneNumber { get; set; }

        public List<Inner_Provider> Providers { get; set; } = new List<Inner_Provider>();
    }
}
