using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Logic;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDataAccess.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public ProviderRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<Inner_Provider> GetProviderByIdAsync(int id)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(a => a.ProviderId == id);
            return Mapper.MapProvider(provider);
        }

        public async Task<List<Inner_Provider>> GetProvidersAsync(string search = null)
        {
            var provider = await _context.Providers.ToListAsync();

            return provider.Select(Mapper.MapProvider).ToList();
        }

        public async Task<bool> ProviderExistAsync(int id)
        {
            return await _context.Providers.AnyAsync(a => a.ProviderId == id);
        }

        public async Task<Inner_Provider> AddProviderAsync(Inner_Provider provider)
        {
            var newProvider = Mapper.UnMapProvider(provider);

            _context.Providers.Add(newProvider);
            await Save();

            return Mapper.MapProvider(newProvider);
        }

        public async Task UpdateProviderAsync(Inner_Provider provider)
        {
            Data_Provider currentEntity = await _context.Providers.FindAsync(provider.ProviderId);
            Data_Provider newEntity = Mapper.UnMapProvider(provider);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        public async Task RemoveProviderAsync(int id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider is null)
            {
                return;
            }

            _context.Providers.Remove(provider);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
