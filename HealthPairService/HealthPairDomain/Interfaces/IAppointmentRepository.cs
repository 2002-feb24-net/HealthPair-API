using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<InnerAppointment>> GetAppointmentAsync(string search = null);

        Task<InnerAppointment> GetAppointmentByIdAsync(int id);

        //Task<IEnumerable<InnerAppointment>> GetAppointmentByPatientNameAsync(string search = null);

        //Task<IEnumerable<InnerAppointment>> GetAppointmentByProviderId(int id);

        Task<bool> AppointmentExistAsync(int id);

        Task<InnerAppointment> AddAppointment(InnerAppointment appointment);

        Task RemoveAppointmentAsync(int id);

        Task Changed(InnerAppointment appointment);

    }
}
