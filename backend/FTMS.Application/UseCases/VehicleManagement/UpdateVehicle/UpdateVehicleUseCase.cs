using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.VehicleManagement.UpdateVehicle
{
    public class UpdateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public UpdateVehicleUseCase(IVehicleRepository vehicleRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _vehicleRepository = vehicleRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(UpdateVehicleCommand request)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle not found.", ErrorCodes.VehicleNotFound);
            }

            if (vehicle.OrganizationId != organizationId)
                throw new NotFoundException("Vehicle not part of organization.", ErrorCodes.VehicleNotOwned);

            if (await _vehicleRepository.ExistsByLicensePlateAsync(request.LicensePlate, request.VehicleId))
                throw new BusinessException("Vehicle license plate is duplicated.", ErrorCodes.VehicleLicenseDuplicated);

            // Update vehicle properties
            vehicle.UpdateInformation(
                request.VehicleCode,
                request.LicensePlate,
                request.Color,
                request.SeatCapacity,
                request.CargoCapacity
            );

            await _vehicleRepository.UpdateAsync(vehicle);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Vehicle updated successfully",
                Data = true,
            };
        }
    }
}
