using HealthPairAPI.TransferModels;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HealthPairAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController: ControllerBase
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentController(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Appointments
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Transfer_Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAppointmentsAsync()
        {
            IEnumerable<Inner_Appointment> user = await _repo.GetAppointmentAsync();

            IEnumerable<Transfer_Appointment> resource = user.Select(u => new Transfer_Appointment
            {
                AppointmentId = u.AppointmentId,
                AppointmentDate = u.AppointmentDate,
                //Patient
                //Provider
            });

            return Ok(resource);
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAppointment(int id)
        {
            Inner_Appointment appointment = await _repo.GetAppointmentByIdAsync(id);
            var resource = new Transfer_Appointment
            { 
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
            };

            if (resource == null)
            {
                return NotFound();
            }

            return Ok(resource);
        }

        // PUT: api/Appointment/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Inner_Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return BadRequest();
            }
            var resource = new Inner_Appointment
            {
               AppointmentId = appointment.AppointmentId,
               AppointmentDate = appointment.AppointmentDate
            };
            await _repo.UpdateAppointmentAsync(resource);

            try
            {
                await _repo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await AppointmentsExists(id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostAppointment(Inner_Appointment user)
        {
            Inner_Appointment add = await _repo.AddAppointmentAsync(user);
            var resource = new Transfer_Appointment
            {
                AppointmentId = add.AppointmentId,
                AppointmentDate = add.AppointmentDate,
                //Add patient and provider
            };

            return Ok(resource);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAppointments(int id)
        {
            var resource = _repo.RemoveAppointmentAsync(id);

            return Ok(resource);
        }
        [HttpGet("{id}")]
        private Task<bool> AppointmentsExists(int id)
        {
            return _repo.AppointmentExistAsync(id);
        }
    }
}