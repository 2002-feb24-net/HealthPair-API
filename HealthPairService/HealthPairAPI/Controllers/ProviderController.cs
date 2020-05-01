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
    public class ProviderController : ControllerBase
    {
        private readonly IProviderRepository _repo;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(IProviderRepository repo, ILogger<ProviderController> logger)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation($"Accessed ProviderController");
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Transfer_Provider>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string search = null)
        {
            List<Transfer_Provider> ProviderAll;
            if (search == null)
            {
                _logger.LogInformation($"Retrieving all providers");
                ProviderAll = (await _repo.GetProvidersAsync()).Select(Mapper.MapProvider).ToList();
            }
            else
            {
                _logger.LogInformation($"Retrieving providers with parameters {search}.");
                ProviderAll = (await _repo.GetProvidersAsync(search)).Select(Mapper.MapProvider).ToList();
            }
            try
            {
                _logger.LogInformation($"Serializing {ProviderAll}");
                string json = JsonSerializer.Serialize(ProviderAll);
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

        // GET: api/provider/5
        /// <summary> Fetches one provider from the database based on input id.
        /// <param name="id"> int - This int is searched for in the id related to provider. </param>
        /// <returns> A content result.
        /// 200 with A provider, depending on input id
        /// 404 if no provider with id is found
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Transfer_Provider), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Transfer_Provider>> GetById(int id)
        {
            _logger.LogInformation($"Retrieving providers with id {id}.");
            if (await _repo.GetProviderByIdAsync(id) is Inner_Provider provider)
            {
                string json = JsonSerializer.Serialize(Mapper.MapProvider(provider));
                return new ContentResult
                {
                    StatusCode = 200,
                    ContentType = "application/json",
                    Content = json
                };
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }

        // POST: api/provider
        /// <summary> Adds a provider to the database.
        /// <param name="provider"> Transfer_Provider Object - This object represents all the input fields of a provider. </param>
        /// <returns> A content result.
        /// 201 with the input object returned if success
        /// 400 if incorrect fields, or data validation fails
        /// 500 if server error
        ///  </returns>
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Transfer_Provider), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(Transfer_Provider provider)
        {
            _logger.LogInformation($"Adding new provider.");
            Inner_Provider transformedProvider = new Inner_Provider
            {
                

            };
            _repo.AddProviderAsync(transformedProvider);
            return CreatedAtAction(nameof(GetById), new { id = provider.ProviderId }, provider);
        }

        // PUT: api/provider/5
        /// <summary> Edits one provider based on input provider object.
        /// <param name="provider"> Transfer_Provider Object - This object represents all the input fields of a provider. </param>
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
        public async Task<IActionResult> Put(int id, [FromBody] Transfer_Provider provider)
        {
            _logger.LogInformation($"Editing provider with id {id}.");
            var entity = await _repo.GetProviderByIdAsync(id);
            if (entity is Inner_Provider)
            {
                

                return NoContent();
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }

        // DELETE: api/provider/5
        /// <summary> Edits one provider based on input provider object.
        /// <param name="id"> int - This int is searched for in the id related to provider. </param>
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
            _logger.LogInformation($"Deleting provider with id {id}.");
            if (await _repo.GetProviderByIdAsync(id) is Inner_Provider provider)
            {
                await _repo.RemoveProviderAsync(provider.ProviderId);
                return NoContent();
            }
            _logger.LogInformation($"No providers found with id {id}.");
            return NotFound();
        }
    }
}