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
    public class SpecialityRepository : ISpecialtyRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public SpecialityRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<InnerSpeciality> AddspecialityAsync(InnerSpeciality speciality)
        {
            var newSpeciality = Mapper.MapSpecialty(speciality);

            _context.Specialties.Add(newSpeciality);
            await _context.SaveChangesAsync();

            return Mapper.MapSpecialty(newSpeciality);
        }

        public EntityState Changed(InnerSpeciality speciality)
        {
            var newSpeciality = Mapper.MapSpecialty(speciality);

            return _context.Entry(newSpeciality).State = EntityState.Modified;
        }

        public async Task<IEnumerable<InnerSpeciality>> GetspecialityAsync(string search = null)
        {
            var speciality = await _context.Specialties.ToListAsync();

            return speciality.Select(Mapper.MapSpecialty);
        }

        public async Task<InnerSpeciality> GetspecialityByIdAsync(int id)
        {
            var speciality = await _context.Specialties
                .FirstOrDefaultAsync(a => a.SpecialtyId == id);
            return Mapper.MapSpecialty(speciality);
        }

        public async Task<bool> RemoveSpecialityAsync(int id)
        {
            var speciality = await _context.Specialties.FindAsync(id);

            if (speciality is null)
            {
                return false;
            }

            _context.Specialties.Remove(speciality);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }

        public async Task<bool> SpecialityExistAsync(int id)
        {
            return await _context.Specialties.AnyAsync(a => a.SpecialtyId == id);
        }
    }
}
