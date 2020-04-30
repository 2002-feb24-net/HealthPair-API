using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class InnerAppointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        public InnerPatient Patient { get; set; }
        public InnerProvider Provider { get; set; }
    }
}
