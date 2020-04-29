using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Facility
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress1 { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public int FacilityZipcode { get; set; }
        public long FacilityPhoneNumber { get; set; }

        public ICollection<Data_Provider> Providers { get; set; } = new List<Data_Provider>();
    }
}
