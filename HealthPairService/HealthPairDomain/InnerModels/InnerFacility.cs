using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    class InnerFacility
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress1 { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public int FacilityZipcode { get; set; }
        public long FacilityPhoneNumber { get; set; }

        //public List<> MyProperty { get; set; }
    }
}
