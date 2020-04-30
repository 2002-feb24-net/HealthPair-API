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

        public async Task<InnerAppointment> AddAppointment(InnerAppointment appointment)
        {
            var newAppointment = Mapper.MapAppointments(appointment);

            _context.Appointments.Add(newAppointment);
            await Save();

            return Mapper.MapAppointments(newAppointment);
        }


        public async Task<bool> AppointmentExistAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.AppointmentId == id);
        }

        public async Task Changed(InnerAppointment appointment)
        {
            var currentAppointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == appointment.AppointmentId);
            var newAppointment = Mapper.MapAppointments(currentAppointment);

            _context.Entry(currentAppointment).CurrentValues.SetValues(newAppointment);


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

        public async Task RemoveAppointmentAsync(int id)
        {
            var appointmnent = await _context.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
            if(appointmnent == null)
            {
                return;
            }
            _context.Remove(appointmnent);
            await Save(); 
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
