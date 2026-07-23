using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.VehicleManagement.DeleteVehicle
{
    public class DeleteVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public DeleteVehicleUseCase(IVehicleRepository vehicleRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _vehicleRepository = vehicleRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid vehicleId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
            {
                throw new NotFoundException("Vehicle is not found.", ErrorCodes.VehicleNotFound);
            }
            vehicle.Delete();
            await _vehicleRepository.UpdateAsync(vehicle); // Ensure soft-delete is persisted

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Vehicle deleted successfully",
                Data = true,
            };
        }
    }
}
