using FTMS.Application.UseCases.Lookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTMS.API.Controllers
{
    [ApiController]
    [Route("api/lookups")]
    [Authorize(Roles = "Dispatcher")]
    public class LookupController : ControllerBase
    {

        private readonly GetLookupPersonUseCase _getLookUpPersonUseCase;
        private readonly GetLookupVehicleUseCase _getLookupVehicleUseCase;
        private readonly GetLookupDriverUseCase _getLookupDriverUseCase;

        public LookupController(GetLookupPersonUseCase getLookUpPersonUseCase, GetLookupVehicleUseCase getLookupVehicleUseCase, GetLookupDriverUseCase getLookupDriverUseCase)
        {
            _getLookUpPersonUseCase = getLookUpPersonUseCase;
            _getLookupVehicleUseCase = getLookupVehicleUseCase;
            _getLookupDriverUseCase = getLookupDriverUseCase;
        }

        [HttpGet("persons")]
        public async Task<IActionResult> GetPersonLookup()
        {
            var result = await _getLookUpPersonUseCase.ExecuteAsync();

            return Ok(result);
        }

        [HttpGet("drivers")]
        public async Task<IActionResult> GetDriverLookup()
        {
            var result = await _getLookupDriverUseCase.ExecuteAsync();

            return Ok(result);
        }

        [HttpGet("vehicles")]
        public async Task<IActionResult> GetVehicleLookup()
        {
            var result = await _getLookupVehicleUseCase.ExecuteAsync();

            return Ok(result);
        }
    }
}
