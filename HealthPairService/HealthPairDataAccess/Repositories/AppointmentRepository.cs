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

        public async Task<Inner_Appointment> AddAppointmentAsync(Inner_Appointment appointment)
        {
            var newAppointment = Mapper.UnMapAppointments(appointment);

            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            return Mapper.MapAppointments(newAppointment);
        }

        public async Task<bool> AppointmentExistAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == id);
        }

        public async Task<List<Inner_Appointment>> GetAppointmentAsync(string search = null)
        {
            var appointments = await _context.Appointments.ToListAsync();

            return appointments.Select(Mapper.MapAppointments).ToList();
        }

        public async Task<Inner_Appointment> GetAppointmentByIdAsync(int id)
        {
            //var dataAppointment = Mapper
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            return Mapper.MapAppointments(appointment);
        }

        public async Task<bool> RemoveAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if(appointment is null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }
    }
}
