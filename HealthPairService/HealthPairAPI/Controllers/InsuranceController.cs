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
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceRepository _repo;
        private readonly ILogger<InsuranceController> _logger;

        public InsuranceController(IInsuranceRepository repo, ILogger<InsuranceController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed InsuranceController");
        }

        // GET: api/insurance
        /// <summary> Fetches all insurances in the database. Can add a search parameter to narrow search. Null returns all.
        /// <param name="search"> string - This string is searched for in the body of multiple fields related to insurance. </param>
        /// <returns> A content result.
        /// 200 with A list of insurances, depending on input search
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Insurance>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Insurance> InsuranceAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all insurances");
                InsuranceAll = (await _repo.GetInsuranceAsync()).Select(Mapper.MapInsurance).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving insurances with parameters {search}.");
                InsuranceAll = (await _repo.GetInsuranceAsync(search)).Select(Mapper.MapInsurance).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {InsuranceAll}");
                string json = JsonSerializer.Serialize(InsuranceAll);
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

        // GET: api/insurance/5
        /// <summary> Fetches one insurance from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to insurance. </param>
        /// <returns> A content result.
        /// 200 with A insurance, depending on input id
        /// 404 if no insurance with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Insurance), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Insurance>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving insurances with id {id}.");
            if (await _repo.GetInsuranceByIdAsync(id) is Inner_Insurance insurance)
            {
                string json = JsonSerializer.Serialize(Mapper.MapInsurance(insurance));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No insurances found with id {id}.");
            return NotFound();
        }

        // POST: api/insurance
        /// <summary> Adds a insurance to the database.
        /// <param name="insurance"> Transfer_Insurance Object - This object represents all the input fields of a insurance. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Insurance), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Insurance insurance)
        {
            _logger.LogInformation($"Adding new insurance.");
            Inner_Insurance transformedInsurance = new Inner_Insurance
            {
                InsuranceId = insurance.InsuranceId,
                InsuranceName = insurance.InsuranceName,
                //Patients
                //Providers

            };
            _repo.AddInsuranceAsync(transformedInsurance);
            return CreatedAtAction(nameof(GetById), new { id = insurance.InsuranceId }, insurance);
        }

        // PUT: api/insurance/5
        /// <summary> Edits one insurance based on input insurance object.
        /// <param name="insurance"> Transfer_Insurance Object - This object represents all the input fields of a insurance. </param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Insurance insurance)
        {
            _logger.LogInformation($"Editing insurance with id {id}.");
            var entity = await _repo.GetInsuranceByIdAsync(id);
            if (entity is Inner_Insurance)
            {
                entity.InsuranceId = insurance.InsuranceId;
                entity.InsuranceName = insurance.InsuranceName;
                //Patients
                //Providers

                return NoContent();
            }
            _logger.LogInformation($"No insurances found with id {id}.");
            return NotFound();
        }

        // DELETE: api/insurance/5
        /// <summary> Edits one insurance based on input insurance object.
        /// <param name="id"> int - This int is searched for in the id related to insurance. </param>
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
            _logger.LogInformation($"Deleting insurance with id {id}.");
            if (await _repo.GetInsuranceByIdAsync(id) is Inner_Insurance insurance)
            {
                await _repo.RemoveInsuranceAsync(insurance.InsuranceId);
                return NoContent();
            }
            _logger.LogInformation($"No insurances found with id {id}.");
            return NotFound();
        }
    }
}
