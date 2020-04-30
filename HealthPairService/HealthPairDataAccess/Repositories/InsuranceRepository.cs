using HealthPairDataAccess.DataModels;
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
    public class InsuranceRepository : IInsuranceRepository, ISave
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public InsuranceRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<InnerInsurance> AddInsuranceAsync(InnerInsurance insurance)
        {
            var newInsurance = Mapper.MapInsurance(insurance);

            _context.Insurances.Add(newInsurance);
            await Save();

            return Mapper.MapInsurance(newInsurance);
        }

        public async Task Changed(InnerInsurance insurance)
        {
            var currentInsurance = await _context.Insurances
                .FirstOrDefaultAsync(a => a.InsuranceId == insurance.InsuranceId);
            var newInsurance = Mapper.MapInsurance(currentInsurance);

            _context.Entry(currentInsurance).CurrentValues.SetValues(newInsurance);
        }

        public async Task<IEnumerable<InnerInsurance>> GetInsuranceAsync(string search = null)
        {
            var insurance = await _context.Insurances.ToListAsync();

            return insurance.Select(Mapper.MapInsurance);
        }

        public async Task<InnerInsurance> GetInsuranceByIdAsync(int id)
        {
            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(a => a.InsuranceId == id);
            return Mapper.MapInsurance(insurance);
        }

        public async Task<bool> InsuranceExistAsync(int id)
        {
            return await _context.Insurances.AnyAsync(a => a.InsuranceId == id);
        }

        public async Task RemoveInsuranceAsync(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);

            if (insurance == null)
            {
                return;
            }

            _context.Insurances.Remove(insurance);
            _context.Remove(insurance);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
