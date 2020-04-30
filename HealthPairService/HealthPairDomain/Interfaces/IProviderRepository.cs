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
        Task<IEnumerable<InnerProvider>> GetProvidersAsync(string search = null);

        Task<InnerProvider> GetProviderByIdAsync(int id);

        Task<IEnumerable<InnerProvider>> GetProviderByFacilityeNameAsync(string search = null);

        Task<IEnumerable<InnerProvider>> GetProviderBySpeialitytId(int id);

        Task<bool> ProviderExistAsync(int id);

        Task<InnerProvider> AddProviderAsync(InnerProvider provider);

        Task<bool> RemoveProviderAsync(int id);

        EntityState Changed(InnerProvider provider);
    }
}
