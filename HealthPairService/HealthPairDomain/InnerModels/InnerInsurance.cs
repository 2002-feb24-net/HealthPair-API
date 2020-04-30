using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerInsurance
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }

        public ICollection<InnerPatient> Patients { get; set; } = new List<InnerPatient>();
        public ICollection<InnerInsuranceProvider> InsuranceProviders { get; set; } = new List<InnerInsuranceProvider>();
    }
}
