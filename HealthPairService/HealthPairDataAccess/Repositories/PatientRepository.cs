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
    public class PatientRepository : IPatientRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public PatientRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<InnerPatient> AddPatientAsync(InnerPatient patient)
        {
            var newAPatient = Mapper.MapPatient(patient);

            _context.Patients.Add(newAPatient);
            await _context.SaveChangesAsync();

            return Mapper.MapPatient(newAPatient);
        }

        public EntityState Changed(InnerPatient patient)
        {
            var newAPatient = Mapper.MapPatient(patient);

            return _context.Entry(newAPatient).State = EntityState.Modified;
        }

        //public Task<IEnumerable<InnerPatient>> GetPatientByAppointmentId(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<InnerPatient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(a => a.PatientId == id);
            return Mapper.MapPatient(patient);
        }

        public async Task<IEnumerable<InnerPatient>> GetPatientsAsync(string search = null)
        {
            var patient = await _context.Patients.ToListAsync();

            return patient.Select(Mapper.MapPatient);
        }

        public async Task<bool> PatientExistAsync(int id)
        {
            return await _context.Patients.AnyAsync(a => a.PatientId == id);
        }

        public async Task<bool> RemovePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }
    }
}
