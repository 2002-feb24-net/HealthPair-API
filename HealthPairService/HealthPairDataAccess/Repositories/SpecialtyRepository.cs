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
    public class SpecialtyRepository : ISpecialtyRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public SpecialtyRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<Inner_Specialty> AddSpecialtyAsync(Inner_Specialty specialty)
        {
            var newSpecialty = Mapper.UnMapSpecialty(specialty);
            _context.Specialties.Add(newSpecialty);
            await _context.SaveChangesAsync();

            return Mapper.MapSpecialty(newSpecialty);
        }

        public async Task<List<Inner_Specialty>> GetSpecialtyAsync(string search = null)
        {
            var specialty = await _context.Specialties.ToListAsync();

            return specialty.Select(Mapper.MapSpecialty).ToList();
        }

        public async Task<Inner_Specialty> GetSpecialtyByIdAsync(int id)
        {
            var specialty = await _context.Specialties
                .FirstOrDefaultAsync(a => a.SpecialtyId == id);
            return Mapper.MapSpecialty(specialty);
        }

        public async Task<bool> RemoveSpecialtyAsync(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty is null)
            {
                return false;
            }

            _context.Specialties.Remove(specialty);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }

        public async Task<bool> SpecialtyExistAsync(int id)
        {
            return await _context.Specialties.AnyAsync(a => a.SpecialtyId == id);
        }
    }
}
