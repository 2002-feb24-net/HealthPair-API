using HealthPairDomain.InnerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Inner_Appointment>> GetAppointmentAsync(string search = null);
        Task<Inner_Appointment> GetAppointmentByIdAsync(int id);
        Task<bool> AppointmentExistAsync(int id);
        Task<Inner_Appointment> AddAppointmentAsync(Inner_Appointment appointment);
        Task<bool> RemoveAppointmentAsync(int id);
    }
}
