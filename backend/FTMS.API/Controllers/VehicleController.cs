using FTMS.API.Dto.Vehicle.Request;
using FTMS.Application.UseCases.VehicleManagement.CreateVehicle;
using FTMS.Application.UseCases.VehicleManagement.DeleteVehicle;
using FTMS.Application.UseCases.VehicleManagement.GetVehicleById;
using FTMS.Application.UseCases.VehicleManagement.GetVehicles;
using FTMS.Application.UseCases.VehicleManagement.UpdateStatusVehicle;
using FTMS.Application.UseCases.VehicleManagement.UpdateVehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTMS.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    [Authorize(Roles = "Dispatcher")]
    public class VehicleController : ControllerBase
    {
        private readonly CreateVehicleUseCase _createVehicleUseCase;
        private readonly GetVehicleByIdUseCase _getVehicleByIdUseCase;
        private readonly GetVehiclesUseCase _getVehiclesUseCase;
        private readonly UpdateVehicleUseCase _updateVehicleUseCase;
        private readonly DeleteVehicleUseCase _deleteVehicleUseCase;
        private readonly UpdateStatusVehicleUseCase _updateStatusVehicleUseCase;

        public VehicleController(CreateVehicleUseCase createVehicleUseCase, GetVehicleByIdUseCase getVehicleByIdUseCase, GetVehiclesUseCase getVehiclesUseCase, UpdateVehicleUseCase updateVehicleUseCase, DeleteVehicleUseCase deleteVehicleUseCase, UpdateStatusVehicleUseCase updateStatusVehicleUseCase)
        {
            _createVehicleUseCase = createVehicleUseCase;
            _getVehicleByIdUseCase = getVehicleByIdUseCase;
            _getVehiclesUseCase = getVehiclesUseCase;
            _updateVehicleUseCase = updateVehicleUseCase;
            _deleteVehicleUseCase = deleteVehicleUseCase;
            _updateStatusVehicleUseCase = updateStatusVehicleUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest request)
        {
            var result = await _createVehicleUseCase.ExecuteAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? isDeleted = null)
        {
            var command = await _getVehiclesUseCase.ExecuteAsync(isDeleted);

            return Ok(command);
        }

        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid vehicleId)
        {
            var command = await _getVehicleByIdUseCase.ExecuteAsync(vehicleId);

            return Ok(command);
        }

        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> Update([FromRoute] Guid vehicleId, [FromBody] UpdateVehicleRequest request)
        {
            var command = new UpdateVehicleCommand()
            {
                VehicleCode = request.VehicleCode,
                VehicleId = vehicleId,
                LicensePlate = request.LicensePlate,
                Color = request.Color,
                SeatCapacity = request.SeatCapacity,
                CargoCapacity = request.CargoCapacity,
            };

            var result = await _updateVehicleUseCase.ExecuteAsync(command);
            return Ok(result);
        }

        [HttpPatch("{vehicleId}/maintenance")]
        public async Task<IActionResult> SetToMaintenance([FromRoute] Guid vehicleId)
        {
            var result = await _updateStatusVehicleUseCase.ExecuteAsync(vehicleId, Domain.Enums.VehicleStatusEnum.Maintenance);

            return Ok(result);
        }

        [HttpPatch("{vehicleId}/available")]
        public async Task<IActionResult> SetToAvailable([FromRoute] Guid vehicleId)
        {
            var result = await _updateStatusVehicleUseCase.ExecuteAsync(vehicleId, Domain.Enums.VehicleStatusEnum.Available);

            return Ok(result);
        }

        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid vehicleId)
        {
            var result = await _deleteVehicleUseCase.ExecuteAsync(vehicleId);
            return Ok(result);
        }

    }
}
