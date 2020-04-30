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
            await _context.SaveChangesAsync();

            return Mapper.MapInsurance(newInsurance);
        }

        public EntityState Changed(InnerInsurance insurance)
        {
            var newInsurance = Mapper.MapInsurance(insurance);

            return _context.Entry(newInsurance).State = EntityState.Modified;
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
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
