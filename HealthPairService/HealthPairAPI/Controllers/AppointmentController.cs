using HealthPairAPI.Logic;
using HealthPairAPI.TransferModels;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthPairAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _repo;
        private readonly IProviderRepository _providerRepo;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(IAppointmentRepository repo, ILogger<AppointmentController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed AppointmentController");
        }

        // GET: api/appointment
        /// <summary> Fetches all appointments in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to appointment. </param>
        /// <returns> A content result.
        /// 200 with A list of appointments, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Appointment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Appointment> AppointmentAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all appointments");
                AppointmentAll = (await _repo.GetAppointmentAsync()).Select(Mapper.MapAppointments).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving appointments with parameters {search}.");
                AppointmentAll = (await _repo.GetAppointmentAsync(search)).Select(Mapper.MapAppointments).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {AppointmentAll}");
                string json = JsonSerializer.Serialize(AppointmentAll);
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Error! {e.Message}.");
                return StatusCode(500);
            }
        }

        // GET: api/appointment/5
        /// <summary> Fetches one appointment from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to appointment. </param>
        /// <returns> A content result.
        /// 200 with A appointment, depending on input id
        /// 404 if no appointment with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Appointment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Appointment>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving appointments with id {id}.");
            if (await _repo.GetAppointmentByIdAsync(id) is Inner_Appointment appointment)
            {
                string json = JsonSerializer.Serialize(Mapper.MapAppointments(appointment));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }

        // POST: api/appointment
        /// <summary> Adds a appointment to the database.
        /// <param name="appointment"> Transfer_Appointment Object - This object represents all the input fields of a appointment. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Appointment), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Appointment appointment)
        {
            _logger.LogInformation($"Adding new appointment.");
            Inner_Appointment transformedAppointment = new Inner_Appointment
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,

            };
            _repo.AddAppointmentAsync(transformedAppointment);
            return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentId }, appointment);
        }

        // PUT: api/appointment/5
        /// <summary> Edits one appointment based on input appointment object.
        /// <param name="appointment"> Transfer_Appointment Object - This object represents all the input fields of a appointment. </param>
        /// <returns> A content result.
        /// 204 upon a successful edit
        /// 404 if input object's id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Appointment appointment)
        {
            _logger.LogInformation($"Editing appointment with id {id}.");
            var entity = await _repo.GetAppointmentByIdAsync(id);
            if (entity is Inner_Appointment)
            {
                entity.AppointmentId = appointment.AppointmentId;
                entity.AppointmentDate = appointment.AppointmentDate;

                return NoContent();
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }

        // DELETE: api/appointment/5
        /// <summary> Edits one appointment based on input appointment object.
        /// <param name="id"> int - This int is searched for in the id related to appointment. </param>
        /// <returns> A content result.
        /// 204 upon a successful delete
        /// 404 if input id was not found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting appointment with id {id}.");
            if (await _repo.GetAppointmentByIdAsync(id) is Inner_Appointment appointment)
            {
                await _repo.RemoveAppointmentAsync(appointment.AppointmentId);
                return NoContent();
            }
            _logger.LogInformation($"No appointments found with id {id}.");
            return NotFound();
        }
    }
}