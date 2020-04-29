using HealthPairDomain.InnerModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    interface IAppointmentRepository
    {
        Task<InnerAppointment> GetAppointmentAsync(string search = null);

        Task<InnerAppointment> GetAppointmentByIdAsync(int id);


    }
}
