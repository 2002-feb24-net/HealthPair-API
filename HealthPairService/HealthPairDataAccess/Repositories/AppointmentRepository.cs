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
    public class AppointmentRepository : IAppointmentRepository
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public AppointmentRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<List<Inner_Appointment>> GetAppointmentAsync(string search = null)
        {
            var appointments = await _context.Appointments.ToListAsync();

            return appointments.Select(Mapper.MapAppointment).ToList();
        }

        public async Task<Inner_Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            return Mapper.MapAppointment(appointment);
        }

        public async Task<bool> AppointmentExistAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == id);
        }

        public async Task<Inner_Appointment> AddAppointmentAsync(Inner_Appointment appointment)
        {
            var newAppointment = Mapper.UnMapAppointment(appointment);

            _context.Appointments.Add(newAppointment);
            await Save();

            return Mapper.MapAppointment(newAppointment);
        }

        public async Task UpdateAppointmentAsync(Inner_Appointment appointment)
        {
            Data_Appointment currentEntity = await _context.Appointments.FindAsync(appointment.AppointmentId);
            Data_Appointment newEntity = Mapper.UnMapAppointment(appointment);

            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
            await Save();
        }

        public async Task RemoveAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            if(appointment == null)
            {
                return;
            }
            _context.Remove(appointment);
            await Save();
        }
        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
