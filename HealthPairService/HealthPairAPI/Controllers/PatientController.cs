using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HealthPairDomain.InnerModels;
using HealthPairDomain.Interfaces;
using HealthPairAPI.TransferModels;
using HealthPairAPI.Logic;

namespace HealthPairAPI.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientRepository patientRepository, IInsuranceRepository insuranceRepository, ILogger<PatientController> logger)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _insuranceRepository = insuranceRepository ?? throw new ArgumentNullException(nameof(insuranceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed PatientController");
        }

        // GET: api/patient
        /// <summary> Fetches all patients to the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to patient. </param>
        /// <returns> A content result.
        /// 200 with A list of patients, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Patient> patientAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all patients");
                patientAll = (await _patientRepository.GetPatientsAsync()).Select(Mapper.MapPatient).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving patients with parameters {search}.");
                patientAll = (await _patientRepository.GetPatientsAsync(search)).Select(Mapper.MapPatient).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {patientAll}");
                string json = JsonSerializer.Serialize(patientAll);
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

        // GET: api/patient/5
        /// <summary> Fetches one patient from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to patient. </param>
        /// <returns> A content result.
        /// 200 with A patient, depending on input id
        /// 404 if no patient with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Patient>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving patients with id {id}.");
            if (await _patientRepository.GetPatientByIdAsync(id) is Inner_Patient patient)
            {
                string json = JsonSerializer.Serialize(Mapper.MapPatient(patient));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // POST: api/patient
        /// <summary> Adds a patient to the database.
        /// <param name="patient"> Transfer_Patient Object - This object represents all the input fields of a patient. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Patient), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Patient patient)
        {
            _logger.LogInformation($"Adding new patient.");
            Inner_Patient transformedPatient = new Inner_Patient
            {
                PatientId = patient.PatientId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName  = patient.PatientLastName,
                PatientCity = patient.PatientCity,
                PatientState = patient.PatientState,
                PatientZipcode = patient.PatientZipcode,
                PatientBirthDay = patient.PatientBirthDay,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                Insurance = (_insuranceRepository.GetInsuranceByIdAsync(patient.InsuranceId)).Result
            };
            _patientRepository.AddPatientAsync(transformedPatient);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }

        // PUT: api/patient/5
        /// <summary> Edits one patient based on input patient object.
        /// <param name="patient"> Transfer_Patient Object - This object represents all the input fields of a patient. </param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Patient patient)
        {
            _logger.LogInformation($"Editing patient with id {id}.");
            var entity = await _patientRepository.GetPatientByIdAsync(id);
            if (entity is Inner_Patient)
            {
                entity.PatientAddress1 = patient.PatientAddress1;
                entity.PatientBirthDay = patient.PatientBirthDay;
                entity.PatientCity = patient.PatientCity;
                entity.PatientFirstName = patient.PatientFirstName;
                entity.PatientLastName = patient.PatientLastName;
                entity.PatientPhoneNumber = patient.PatientPhoneNumber;
                entity.PatientState = patient.PatientState;
                entity.PatientZipcode = patient.PatientZipcode;
                await _patientRepository.UpdatePatientAsync(entity);
                return NoContent();
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }

        // DELETE: api/patient/5
        /// <summary> Edits one patient based on input patient object.
        /// <param name="id"> int - This int is searched for in the id related to patient. </param>
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
            _logger.LogInformation($"Deleting patient with id {id}.");
            if (await _patientRepository.GetPatientByIdAsync(id) is Inner_Patient patient)
            {
                await _patientRepository.RemovePatientAsync(patient.PatientId);
                return NoContent();
            }
            _logger.LogInformation($"No patients found with id {id}.");
            return NotFound();
        }
    }
}