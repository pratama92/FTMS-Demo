using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.VehicleManagement.Dtos;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.VehicleManagement.GetVehicleById
{
    public class GetVehicleByIdUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetVehicleByIdUseCase(IVehicleRepository vehicleRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _vehicleRepository = vehicleRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<VehicleResponse>> ExecuteAsync(Guid vehicleId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle not found,", ErrorCodes.VehicleNotFound);
            }
            if (vehicle.OrganizationId != organizationId)
                throw new NotFoundException("Vehicle not part of organization", ErrorCodes.VehicleNotOwned);

            var vehicleResponse = new VehicleResponse
            {
                VehicleId = vehicle.VehicleId,
                VehicleCode = vehicle.VehicleCode,
                LicensePlate = vehicle.LicensePlate,
                ChassisNumber = vehicle.ChassisNumber,
                EngineNumber = vehicle.EngineNumber,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                VehicleType = vehicle.VehicleType.ToString(),
                FuelType = vehicle.FuelType.ToString(),
                Drivetrain = vehicle.Drivetrain.ToString(),
                Transmission = vehicle.Transmission.ToString(),
                SeatCapacity = vehicle.SeatCapacity,
                CargoCapacity = vehicle.CargoCapacity,
                Status = vehicle.Status.ToString(),
                OrganizationId = vehicle.OrganizationId
            };

            return new BaseResponse<VehicleResponse>
            {
                Success = true,
                Message = "Vehicle retrieved successfully",
                Data = vehicleResponse,
            };
        }
    }
}
