using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<InnerPatient>> GetPatientsAsync(string search = null);

        Task<InnerPatient> GetPatientByIdAsync(int id);

        Task<IEnumerable<InnerPatient>> GetPatientByInsuranceNameAsync(string search = null);

        Task<IEnumerable<InnerPatient>> GetPatientByAppointmentId(int id);

        Task<bool> PatientExistAsync(int id);

        Task<InnerPatient> AddPatientAsync(InnerPatient Patient);

        Task<bool> RemovePatientAsync(int id);

        EntityState Changed(InnerPatient Patient);
    }
}
