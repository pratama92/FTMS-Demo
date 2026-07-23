using FTMS.Application.Common;
using FTMS.Application.Interfaces;

namespace FTMS.Application.UseCases.Lookup
{

    public class GetLookupVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;

        public GetLookupVehicleUseCase(IVehicleRepository vehicleRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator)
        {
            _vehicleRepository = vehicleRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
        }

        public async Task<BaseResponse<List<LookupResponse>>> ExecuteAsync()
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _vehicleRepository.GetAllAsync(organizationId, false);
            var lookupResponse = vehicle.Select(v => new LookupResponse
            {
                LookupId = v.VehicleId,
                LookupName = $"{v.VehicleCode} - {v.LicensePlate}",
            }).ToList();

            return new BaseResponse<List<LookupResponse>>
            {
                Success = true,
                Message = "Vehicle lookup retrieved successfully",
                Data = lookupResponse
            };
        }
    }
}
