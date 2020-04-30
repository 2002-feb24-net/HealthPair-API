using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerInsuranceProvider
    {
        public int InsuranceId { get; set; }
        public InnerInsurance Insurance { get; set; }
        public int ProviderId { get; set; }
        public InnerProvider Provider { get; set; }
    }
}
