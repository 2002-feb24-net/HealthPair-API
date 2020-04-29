using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDataAccess.DataModels
{
    public class Data_Specialty
    {
        public int SpecialtyId { get; set; }
        public string Specialty { get; set; }

        public ICollection<Data_Provider> Providers { get; set; } = new List<Data_Provider>();
    }
}
