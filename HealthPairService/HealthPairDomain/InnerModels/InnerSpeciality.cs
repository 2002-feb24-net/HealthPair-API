using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerSpeciality
    {
        public int SpecialtyId { get; set; }
        public string Specialty { get; set; }

        public ICollection<InnerProvider> Providers { get; set; } = new List<InnerProvider>();
    }
}
