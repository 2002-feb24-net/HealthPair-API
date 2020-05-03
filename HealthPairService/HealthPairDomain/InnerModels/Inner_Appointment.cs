using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HealthPairDomain.InnerModels
{
    public class Inner_Appointment
    {
        private int _id;

        public int AppointmentId
        {
            get
            {
                return _id;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("There can not be a negative id!");
                }
                _id = value;
            }
        }

        public DateTime AppointmentDate { get; set; }

        public Inner_Patient Patient { get; set; }
        public Inner_Provider Provider { get; set; }
    }
}
