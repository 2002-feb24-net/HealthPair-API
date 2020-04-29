using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    class CoreAppointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        //public Data_Patient Patient { get; set; }
        //public Data_Provider Provider { get; set; }
    }
}
