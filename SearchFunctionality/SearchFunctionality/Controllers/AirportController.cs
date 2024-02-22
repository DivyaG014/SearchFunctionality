using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchFunctionality.BusinessLogic.Services;

namespace SearchFunctionality.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;
        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        [Route("GetAllAirports")]
        public async Task<IActionResult> GetAllAirports()
        {
            return Ok(await _airportService.GetAllAirports());
        }
    }
}
