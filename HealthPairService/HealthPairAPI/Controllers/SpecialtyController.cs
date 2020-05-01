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
    public class SpeacilityController : ControllerBase
    {
        private readonly ISpecialtyRepository _repo;
        private readonly ILogger<SpeacilityController> _logger;

        public SpeacilityController(ISpecialtyRepository repo, ILogger<SpeacilityController> logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed SpecialtyController");
        }

        // GET: api/specialty
        /// <summary> Fetches all specialties in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to specialty. </param>
        /// <returns> A content result.
        /// 200 with A list of specialties, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Specialty>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Specialty> SpecialtyAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all specialties");
                SpecialtyAll = (await _repo.GetSpecialtyAsync()).Select(Mapper.MapSpecialty).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving specialties with parameters {search}.");
                SpecialtyAll = (await _repo.GetSpecialtyAsync(search)).Select(Mapper.MapSpecialty).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {SpecialtyAll}");
                string json = JsonSerializer.Serialize(SpecialtyAll);
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

        // GET: api/specialty/5
        /// <summary> Fetches one specialty from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to specialty. </param>
        /// <returns> A content result.
        /// 200 with A specialty, depending on input id
        /// 404 if no specialty with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Specialty), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Specialty>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving specialties with id {id}.");
            if (await _repo.GetSpecialtyByIdAsync(id) is Inner_Specialty specialty)
            {
                string json = JsonSerializer.Serialize(Mapper.MapSpecialty(specialty));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }

        // POST: api/specialty
        /// <summary> Adds a specialty to the database.
        /// <param name="specialty"> Transfer_Specialty Object - This object represents all the input fields of a specialty. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Specialty), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Specialty specialty)
        {
            _logger.LogInformation($"Adding new specialty.");
            Inner_Specialty transformedSpecialty = new Inner_Specialty
            {
                SpecialtyId = specialty.SpecialtyId,
                Specialty = specialty.Specialty
                // Add more clsses

            };
            _repo.AddSpecialtyAsync(transformedSpecialty);
            return CreatedAtAction(nameof(GetById), new { id = specialty.SpecialtyId }, specialty);
        }

        // PUT: api/specialty/5
        /// <summary> Edits one specialty based on input specialty object.
        /// <param name="specialty"> Transfer_Specialty Object - This object represents all the input fields of a specialty. </param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Specialty specialty)
        {
            _logger.LogInformation($"Editing specialty with id {id}.");
            var entity = await _repo.GetSpecialtyByIdAsync(id);
            if (entity is Inner_Specialty)
            {
                entity.SpecialtyId = specialty.SpecialtyId;
                entity.Specialty = specialty.Specialty;
                // Add more clsses

                return NoContent();
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }

        // DELETE: api/specialty/5
        /// <summary> Edits one specialty based on input specialty object.
        /// <param name="id"> int - This int is searched for in the id related to specialty. </param>
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
            _logger.LogInformation($"Deleting specialty with id {id}.");
            if (await _repo.GetSpecialtyByIdAsync(id) is Inner_Specialty specialty)
            {
                await _repo.RemoveSpecialtyAsync(specialty.SpecialtyId);
                return NoContent();
            }
            _logger.LogInformation($"No specialties found with id {id}.");
            return NotFound();
        }
    }
}