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
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityRepository _repo;
        private readonly IProviderRepository _providerRepo;
        private readonly ILogger<FacilityController> _logger;

        public FacilityController(IFacilityRepository repo, IProviderRepository providerRepo, ILogger<FacilityController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _providerRepo = providerRepo ?? throw new ArgumentException(nameof(providerRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed FacilityController");
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Facility>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Facility> FacilityAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all facilitys");
                FacilityAll = (await _repo.GetFacilityAsync()).Select(Mapper.MapFacility).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving facilitys with parameters {search}.");
                FacilityAll = (await _repo.GetFacilityAsync(search)).Select(Mapper.MapFacility).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {FacilityAll}");
                string json = JsonSerializer.Serialize(FacilityAll);
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

        // GET: api/facility/5
        /// <summary> Fetches one facility from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to facility. </param>
        /// <returns> A content result.
        /// 200 with A facility, depending on input id
        /// 404 if no facility with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Facility), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Facility>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving facilitys with id {id}.");
            if (await _repo.GetFacilityByIdAsync(id) is Inner_Facility facility)
            {
                string json = JsonSerializer.Serialize(Mapper.MapFacility(facility));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No facilitys found with id {id}.");
            return NotFound();
        }

        // POST: api/facility
        /// <summary> Adds a facility to the database.
        /// <param name="facility"> Transfer_Facility Object - This object represents all the input fields of a facility. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Facility), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Facility facility)
        {
            _logger.LogInformation($"Adding new facility.");
            Inner_Facility transformedFacility = new Inner_Facility
            {
                FacilityId = facility.FacilityId,
                FacilityAddress1 = facility.FacilityAddress1,
                FacilityCity = facility.FacilityCity,
                FacilityName = facility.FacilityName,
                FacilityState = facility.FacilityState,
                FacilityZipcode = facility.FacilityZipcode,
                FacilityPhoneNumber = facility.FacilityPhoneNumber,
                //Providers = (_providerRepo.GetProviderByIdAsync(facility.Pr))
                
            };
            _repo.AddFacilityAsync(transformedFacility);
            return CreatedAtAction(nameof(GetById), new { id = facility.FacilityId }, facility);
        }

        // PUT: api/facility/5
        /// <summary> Edits one facility based on input facility object.
        /// <param name="facility"> Transfer_Facility Object - This object represents all the input fields of a facility. </param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Facility facility)
        {
            _logger.LogInformation($"Editing facility with id {id}.");
            var entity = await _repo.GetFacilityByIdAsync(id);
            if (entity is Inner_Facility)
            {
                entity.FacilityId = facility.FacilityId;
                entity.FacilityAddress1 = facility.FacilityAddress1;
                entity.FacilityCity = facility.FacilityCity;
                entity.FacilityName = facility.FacilityName;
                entity.FacilityState = facility.FacilityState;
                entity.FacilityZipcode = facility.FacilityZipcode;
                entity.FacilityPhoneNumber = facility.FacilityPhoneNumber;

                return NoContent();
            }
            _logger.LogInformation($"No facilitys found with id {id}.");
            return NotFound();
        }

        // DELETE: api/facility/5
        /// <summary> Edits one facility based on input facility object.
        /// <param name="id"> int - This int is searched for in the id related to facility. </param>
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
            _logger.LogInformation($"Deleting facility with id {id}.");
            if (await _repo.GetFacilityByIdAsync(id) is Inner_Facility facility)
            {
                await _repo.RemoveFacilityAsync(facility.FacilityId);
                return NoContent();
            }
            _logger.LogInformation($"No facilitys found with id {id}.");
            return NotFound();
        }
    }
}