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

        public async Task<Inner_Insurance> AddInsuranceAsync(Inner_Insurance insurance)
        {
            var newInsurance = Mapper.UnMapInsurance(insurance);
            _context.Insurances.Add(newInsurance);
            await _context.SaveChangesAsync();

            return Mapper.MapInsurance(newInsurance);
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

        public async Task<bool> RemoveInsuranceAsync(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);

            if (insurance is null)
            {
                return false;
            }

            _context.Insurances.Remove(insurance);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }
    }
}
