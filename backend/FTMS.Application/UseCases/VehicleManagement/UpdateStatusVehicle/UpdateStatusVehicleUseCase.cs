using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.VehicleManagement.UpdateStatusVehicle
{
    public class UpdateStatusVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;

        public UpdateStatusVehicleUseCase(IVehicleRepository vehicleRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator)
        {
            _vehicleRepository = vehicleRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
        }

        public async Task<BaseResponse<string>> ExecuteAsync(Guid vehicleId, VehicleStatusEnum status)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
                throw new NotFoundException("Vehicle is not found.", ErrorCodes.VehicleNotFound);

            if (vehicle.OrganizationId != organizationId)
                throw new NotFoundException("Organisation is not own this vehicle.", ErrorCodes.VehicleNotOwned);

            switch (status)
            {
                case VehicleStatusEnum.Available:
                    vehicle.Activate();
                    break;
                case VehicleStatusEnum.Maintenance:
                    vehicle.SetMaintenance();
                    break;
                case VehicleStatusEnum.Retired:
                    vehicle.Retire();
                    break;
                default:
                    throw new BusinessException("Invalid vehicle status.", ErrorCodes.InvalidStatus);
            }

            await _vehicleRepository.UpdateAsync(vehicle);

            return new BaseResponse<string>
            {
                Success = true,
                Message = "Status changed successfully.",
                Data = vehicle.Status.ToString()

            };
        }

    }
}
