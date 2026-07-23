using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.VehicleManagement.Dtos;

namespace FTMS.Application.UseCases.VehicleManagement.GetVehicles
{
    public class GetVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetVehiclesUseCase(IVehicleRepository vehicleRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator)
        {
            _vehicleRepository = vehicleRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
        }

        public async Task<BaseResponse<List<VehicleResponse>>> ExecuteAsync(bool? isDeleted = null)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetAllAsync(organizationId, isDeleted);
            var vehicleResponse = vehicle.Select(v => new VehicleResponse
            {
                VehicleId = v.VehicleId,
                VehicleCode = v.VehicleCode,
                LicensePlate = v.LicensePlate,
                ChassisNumber = v.ChassisNumber,
                EngineNumber = v.EngineNumber,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                Color = v.Color,
                VehicleType = v.VehicleType.ToString(),
                FuelType = v.FuelType.ToString(),
                Drivetrain = v.Drivetrain.ToString(),
                Transmission = v.Transmission.ToString(),
                SeatCapacity = v.SeatCapacity,
                CargoCapacity = v.CargoCapacity,
                Status = v.Status.ToString(),
                OrganizationId = v.OrganizationId
            }).ToList();

            return new BaseResponse<List<VehicleResponse>>
            {
                Success = true,
                Message = "Vehicles retrieved successfully",
                Data = vehicleResponse
            };
        }
    }
}
