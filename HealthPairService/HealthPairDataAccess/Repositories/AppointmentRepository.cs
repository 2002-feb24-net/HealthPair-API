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
    public class AppointmentRepository : IAppointmentRepository, ISave
    {
        //Private variable of context
        private readonly HealthPairContext _context;

        //Constructor
        public AppointmentRepository(HealthPairContext context)
        {
            _context = context;
        }

        public async Task<InnerAppointment> AddAppointmentAsync(InnerAppointment appointment)
        {
            var newAppointment = Mapper.MapAppointments(appointment);

            _context.Appointments.Add(newAppointment);
            await _context.SaveChangesAsync();

            return Mapper.MapAppointments(newAppointment);
        }

        public async Task<bool> AppointmentExistAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == id);
        }

        public EntityState Changed(InnerAppointment appointment)
        {
            var newAppointment = Mapper.MapAppointments(appointment);

            return _context.Entry(newAppointment).State = EntityState.Modified;
        }

        public async Task<IEnumerable<InnerAppointment>> GetAppointmentAsync(string search = null)
        {
            var appointments = await _context.Appointments.ToListAsync();

            return appointments.Select(Mapper.MapAppointments);
        }

        public async Task<InnerAppointment> GetAppointmentByIdAsync(int id)
        {
            //var dataAppointment = Mapper
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            return Mapper.MapAppointments(appointment);
        }

        public async Task<bool> RemoveAppointmentAsync(int id)
        {
            var appointmnent = await _context.Appointments.FindAsync(id);

            if(appointmnent is null)
            {
                return false;
            }

            _context.Appointments.Remove(appointmnent);
            int written = await _context.SaveChangesAsync();

            return written > 0;
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
