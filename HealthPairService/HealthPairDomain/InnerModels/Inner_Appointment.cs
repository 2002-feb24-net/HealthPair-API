using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Appointment
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Must Choose a Date")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        public Inner_Patient Patient { get; set; }
        public Inner_Provider Provider { get; set; }
    }
}
