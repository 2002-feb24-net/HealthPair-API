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

        public async Task<Inner_Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(a => a.PatientId == id);
            return Mapper.MapPatient(patient);
        }

        public async Task<List<Inner_Patient>> GetPatientsAsync(string search = null)
        {
            var patient = await _context.Patients.ToListAsync();

            return patient.Select(Mapper.MapPatient).ToList();
        }

        public async Task<bool> PatientExistAsync(int id)
        {
            return await _context.Patients.AnyAsync(a => a.PatientId == id);
        }

        public async Task<Inner_Patient> AddPatientAsync(Inner_Patient patient)
        {
            var newAPatient = Mapper.UnMapPatient(patient);
            _context.Patients.Add(newAPatient);
            await Save();

            return Mapper.MapPatient(newAPatient);
        }

        public async Task UpdatePatientAsync(Inner_Patient patient)
        {
            Data_Patient currentEntity = await _context.Patients.FindAsync(patient.PatientId);
            Data_Patient newEntity = Mapper.UnMapPatient(patient);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        public async Task RemovePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
            {
                return;
            }

            _context.Patients.Remove(patient);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
