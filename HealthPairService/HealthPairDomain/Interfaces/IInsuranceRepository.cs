using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IInsuranceRepository
    {
        Task<List<Inner_Insurance>> GetInsuranceAsync(string search = null);
        Task<Inner_Insurance> GetInsuranceByIdAsync(int id);
        Task<bool> InsuranceExistAsync(int id);
        Task<Inner_Insurance> AddInsuranceAsync(Inner_Insurance insurance);
        Task UpdateInsuranceAsync(Inner_Insurance insurance);
        Task RemoveInsuranceAsync(int id);
    }
}
