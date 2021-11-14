using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neighbors.Controllers.RequestModels;
using Neighbors.Queries;

namespace Neighbors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationQuery _locationQuery;

        public LocationController(ILogger<LocationController> logger, ILocationQuery locationQuery)
        {
            _logger = logger;
            _locationQuery = locationQuery;
        }

        [HttpPost]
        public async Task<ActionResult> GetNearestLocations(NearestLocationsRequestModel model)
        {
            model = model ?? throw new ArgumentNullException(nameof(model));
            try
            {
                var result =
                    await _locationQuery.GetNearestLocationsAsync(model.Coordinate, model.MaxDistance, model.MaxResults);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(e.Message);
            }
        }
    }
}