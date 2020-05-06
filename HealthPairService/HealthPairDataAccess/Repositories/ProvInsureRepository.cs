using HealthPairDataAccess.DataModels;
using HealthPairDataAccess.Logic;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthPairDataAccess.Repositories
{
    public class ProvInsurRepository : IProvInsurRepository
    {
        private readonly HealthPairContext _context;

        public ProvInsurRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<int[]> GetInsuranceCoverage(int id)
        {
            List<int> myIntList = new List<int>();
            var coverage = await _context.InsurProvs.ToListAsync();
            coverage = coverage.FindAll(p => p.ProviderId == id);
            foreach (var val in coverage)
            {
                myIntList.Add(val.InsuranceId);
            }
            return myIntList.ToArray();
        }

        public async Task<int[]> GetProviderCoverage(int id)
        {
            List<int> myIntList = new List<int>();
            var coverage = await _context.InsurProvs.ToListAsync();
            coverage = coverage.FindAll(p => p.InsuranceId == id);
            foreach (var val in coverage)
            {
                myIntList.Add(val.ProviderId);
            }
            return myIntList.ToArray();
        }
    }
}