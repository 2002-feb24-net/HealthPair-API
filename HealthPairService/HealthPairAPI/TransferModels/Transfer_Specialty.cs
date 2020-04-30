using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairAPI.TransferModels
{
    public class Transfer_Specialty
    {
        public int SpecialtyId { get; set; }
        public string Specialty { get; set; }

        public List<Transfer_Provider> Providers { get; set; } = new List<Transfer_Provider>();
    }
}
