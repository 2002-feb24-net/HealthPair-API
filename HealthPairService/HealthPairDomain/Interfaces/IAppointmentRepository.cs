using HealthPairDomain.InnerModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    interface IAppointmentRepository
    {
        Task<CoreAppointment> GetAppointmentAsync(string search = null);

        Task<CoreAppointment> GetAppointmentByIdAsync(int id);


    }
}
