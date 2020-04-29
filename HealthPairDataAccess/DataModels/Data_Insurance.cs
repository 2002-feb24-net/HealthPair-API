using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Insurance
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }

        public ICollection<Data_Patient> Patients { get; set; } = new List<Data_Patient>();
        public ICollection<Data_InsuranceProvider> InsuranceProviders { get; set; } = new List<Data_InsuranceProvider>();
    }
}
