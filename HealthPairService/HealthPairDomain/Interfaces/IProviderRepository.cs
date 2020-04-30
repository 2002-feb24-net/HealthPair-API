using HealthPairDomain.InnerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDomain.Interfaces
{
    public interface IProviderRepository
    {
        Task<List<Inner_Provider>> GetProvidersAsync(string search = null);
        Task<Inner_Provider> GetProviderByIdAsync(int id);
        Task<bool> ProviderExistAsync(int id);
        Task<Inner_Provider> AddProviderAsync(Inner_Provider provider);
        Task UpdateProviderAsync(Inner_Provider provider);
        Task RemoveProviderAsync(int id);
    }
}
