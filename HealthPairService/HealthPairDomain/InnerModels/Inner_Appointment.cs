using System;
using System.Collections.Generic;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        public Inner_Patient Patient { get; set; }
        public Inner_Provider Provider { get; set; }
    }
}
