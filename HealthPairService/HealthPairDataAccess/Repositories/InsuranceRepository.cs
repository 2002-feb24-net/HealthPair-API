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
    public class InsuranceRepository : IInsuranceRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public InsuranceRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<List<Inner_Insurance>> GetInsuranceAsync(string search = null)
        {
            var insurance = await _context.Insurances.ToListAsync();

            return insurance.Select(Mapper.MapInsurance).ToList();
        }

        public async Task<Inner_Insurance> GetInsuranceByIdAsync(int id)
        {
            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(a => a.InsuranceId == id);
            return Mapper.MapInsurance(insurance);
        }

        public async Task<bool> InsuranceExistAsync(int id)
        {
            return await _context.Insurances.AnyAsync(a => a.InsuranceId == id);
        }

        public async Task<Inner_Insurance> AddInsuranceAsync(Inner_Insurance insurance)
        {
            var newInsurance = Mapper.UnMapInsurance(insurance);
            _context.Insurances.Add(newInsurance);
            await _context.SaveChangesAsync();

            return Mapper.MapInsurance(newInsurance);
        }

        public async Task UpdateInsuranceAsync(Inner_Insurance insurance)
        {
            Data_Insurance currentEntity = await _context.Insurances.FindAsync(insurance.InsuranceId);
            Data_Insurance newEntity = Mapper.UnMapInsurance(insurance);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        public async Task RemoveInsuranceAsync(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance is null)
            {
                return;
            }

            _context.Insurances.Remove(insurance);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
