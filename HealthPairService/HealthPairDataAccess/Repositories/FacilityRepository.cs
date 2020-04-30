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
    public class FacilityRepository : IFacilityRepository, ISave
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public FacilityRepository(HealthPairContext context)
        {
            _context = context;
        }


        public async Task<InnerFacility> AddFacilityAsync(InnerFacility facility)
        {
            var newFacility = Mapper.MapFacility(facility);

            _context.Facilities.Add(newFacility);
            await _context.SaveChangesAsync();

            return Mapper.MapFacility(newFacility);
        }

        public EntityState ChangedAsync(InnerFacility facility)
        {
            var newFacility = Mapper.MapFacility(facility);

            return _context.Entry(newFacility).State = EntityState.Modified;
        }

        public async Task<bool> FacilityExistAsync(int id)
        {
            return await _context.Facilities.AnyAsync(a => a.FacilityId == id);
        }

        public async Task<IEnumerable<InnerFacility>> GetFacilityAsync(string search = null)
        {
            var facility = await _context.Facilities.ToListAsync();

            return facility.Select(Mapper.MapFacility);
        }

        public async Task<InnerFacility> GetFacilityByIdAsync(int id)
        {
            var facility = await _context.Facilities
                .FirstOrDefaultAsync(a => a.FacilityId == id);
            return Mapper.MapFacility(facility);
        }

        public async Task RemoveFacilityAsync(int id)
        {
            var facility = await _context.Facilities
                .FirstOrDefaultAsync(a => a.FacilityId == id);
            if (facility == null)
            {
                return;
            }
            _context.Remove(facility);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
