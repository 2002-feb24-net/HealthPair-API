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
    public class FacilityRepository : IFacilityRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public FacilityRepository(HealthPairContext context)
        {
            _context = context;
        }


        public async Task<Inner_Facility> AddFacilityAsync(Inner_Facility facility)
        {
            var newFacility = Mapper.UnMapFacility(facility);
            _context.Facilities.Add(newFacility);
            await _context.SaveChangesAsync();

            return Mapper.MapFacility(newFacility);
        }

        public async Task<bool> FacilityExistAsync(int id)
        {
            return await _context.Facilities.AnyAsync(a => a.FacilityId == id);
        }

        public async Task<List<Inner_Facility>> GetFacilityAsync(string search = null)
        {
            var facility = await _context.Facilities.ToListAsync();

            return facility.Select(Mapper.MapFacility).ToList();
        }

        public async Task<Inner_Facility> GetFacilityByIdAsync(int id)
        {
            var facility = await _context.Facilities
                .FirstOrDefaultAsync(a => a.FacilityId == id);
            return Mapper.MapFacility(facility);
        }

        public async Task<bool> RemoveFacilityAsync(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility is null)
            {
                return false;
            }

            _context.Facilities.Remove(facility);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }
    }
}
