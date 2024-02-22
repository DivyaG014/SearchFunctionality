using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchFunctionality.BusinessLogic.RequestModels;
using SearchFunctionality.BusinessLogic.Services;

namespace SearchFunctionality.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Route("GetFlights")]
        public async Task<IActionResult> GetFlights(SearchFlights searchFlights)
        {
            return Ok(await _flightService.GetAllFlights(searchFlights));
        }
    }
}
