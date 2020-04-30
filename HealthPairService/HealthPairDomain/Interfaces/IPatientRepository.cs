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
        Task<List<Inner_Patient>> GetPatientsAsync(string search = null);
        Task<Inner_Patient> GetPatientByIdAsync(int id);
        Task<bool> PatientExistAsync(int id);
        Task<Inner_Patient> AddPatientAsync(Inner_Patient patient);
        Task UpdatePatientAsync(Inner_Patient patient);
        Task RemovePatientAsync(int id);
    }
}
