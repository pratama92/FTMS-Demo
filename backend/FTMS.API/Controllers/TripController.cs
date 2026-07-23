using FTMS.API.Dto.Trip.Request;
using FTMS.Application.UseCases.TripManagement.CancelTrip;
using FTMS.Application.UseCases.TripManagement.StartTrip;
using FTMS.Application.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/trips")]
[Authorize]
public class TripController : ControllerBase
{
    private readonly StartTripUseCase _startTripUseCase;
    private readonly CancelTripUseCase _cancelTripUseCase;
    private readonly FinishTripWorkflow _finishTripWorkflow;

    public TripController(StartTripUseCase startTripUseCase, CancelTripUseCase cancelTripUseCase, FinishTripWorkflow finishTripWorkflow)
    {
        _startTripUseCase = startTripUseCase;
        _cancelTripUseCase = cancelTripUseCase;
        _finishTripWorkflow = finishTripWorkflow;
    }


    [HttpPatch("{tripId}/start")]
    public async Task<IActionResult> Start([FromRoute] Guid tripId, [FromBody] StartTripRequest request)
    {
        return Ok(await _startTripUseCase.ExecuteAsync(tripId, request.StartTripTime));
    }


    [HttpPatch("{tripId}/finish")]
    public async Task<IActionResult> Finish([FromRoute] Guid tripId, [FromBody] FinishTripRequest request)
    {
        return Ok(await _finishTripWorkflow.ExecuteAsync(tripId, request.FinishTripTime));
    }


    [HttpPatch("{tripId}/cancel")]
    public async Task<IActionResult> Cancel([FromRoute] Guid tripId, [FromBody] CancelTripRequest request)
    {
        var command = new CancelTripCommand()
        {
            TripId = tripId,
            CancelReason = request.CancelReason,
        };

        return Ok(await _cancelTripUseCase.ExecuteAsync(command));
    }
}