using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.VehicleManagement.CreateVehicle
{
    public class CreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public CreateVehicleUseCase(IVehicleRepository vehicleRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _vehicleRepository = vehicleRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<Guid>> ExecuteAsync(CreateVehicleRequest request)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            if (!Enum.IsDefined(typeof(DrivetrainEnum), request.Drivetrain))
            {
                throw new BusinessException("Invalid drivetrain specified.", ErrorCodes.VehicleDrivetrainInvalid);
            }

            if (!Enum.IsDefined(typeof(FuelTypeEnum), request.FuelType))
            {
                throw new BusinessException("Invalid fuel type specified.", ErrorCodes.VehicleFuelTypeInvalid);
            }

            if (!Enum.IsDefined(typeof(TransmissionTypeEnum), request.Transmission))
            {
                throw new BusinessException("Invalid transmission type specified.", ErrorCodes.VehicleTransmissionInvalid);
            }

            if (!Enum.IsDefined(typeof(VehicleTypeEnum), request.VehicleType))
            {
                throw new BusinessException("Invalid vehicle type specified.", ErrorCodes.VehicleTypeInvalid);
            }

            if (await _vehicleRepository.ExistsByLicensePlateAsync(request.LicensePlate))
                throw new BusinessException("Vehicle license plate is duplicated.", ErrorCodes.VehicleLicenseDuplicated);

            if (await _vehicleRepository.ExistsByChassisNumberAsync(request.ChassisNumber))
                throw new BusinessException("Chassis number is duplicated.", ErrorCodes.VehicleChassisNumberDuplicated);

            if (await _vehicleRepository.ExistsByEngineNumberAsync(request.EngineNumber))
                throw new BusinessException("Engine number is duplicated.", ErrorCodes.VehicleEngineNumberDuplicated);

            var vehicle = Vehicle.Create(
                request.VehicleCode,
                request.LicensePlate,
                request.ChassisNumber,
                request.EngineNumber,
                request.Brand,
                request.Model,
                request.Year,
                request.Color,
                request.SeatCapacity,
                request.CargoCapacity,
                request.VehicleType,
                request.Transmission,
                request.Drivetrain,
                request.FuelType,
                organizationId
            );

            await _vehicleRepository.AddAsync(vehicle);
            return new BaseResponse<Guid>
            {
                Success = true,
                Message = "Vehicle created successfully.",
                Data = vehicle.VehicleId
            };
        }
    }
}
