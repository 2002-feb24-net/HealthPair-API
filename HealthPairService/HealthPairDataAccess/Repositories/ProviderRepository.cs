﻿using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Logic;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using HealthPairDomain.Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPairDataAccess.Repositories
{
    public class ProviderRepository : IProviderRepository, ISave
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public ProviderRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<InnerProvider> AddProviderAsync(InnerProvider provider)
        {
            var newProvider = Mapper.MapProvier(provider);

            _context.Providers.Add(newProvider);
            await Save();

            return Mapper.MapProvier(newProvider);
        }

        public EntityState Changed(InnerProvider provider)
        {
            var newProvider = Mapper.MapProvier(provider);

            return _context.Entry(newProvider).State = EntityState.Modified;
        }

        public async Task<InnerProvider> GetProviderByIdAsync(int id)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(a => a.ProviderId == id);
            return Mapper.MapProvier(provider);
        }

        public async Task<IEnumerable<InnerProvider>> GetProvidersAsync(string search = null)
        {
            var provider = await _context.Providers.ToListAsync();

            return provider.Select(Mapper.MapProvier);
        }

        public async Task<bool> ProviderExistAsync(int id)
        {
            return await _context.Providers.AnyAsync(a => a.ProviderId == id);
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

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
