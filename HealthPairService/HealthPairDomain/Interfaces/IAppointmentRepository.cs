using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
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

        Task<InnerAppointment> GetAppointmentByPatientIdAsync(int id);

        Task<bool> AppointExistAsync(int id);

        Task<InnerAppointment> AddAppointmentAsync(InnerAppointment appointment);

        Task<bool> RemoveAppointmentAsync(int id);

        //Check if appontment changeg

        EntityState Changed(InnerAppointment appointment);

    }
}
