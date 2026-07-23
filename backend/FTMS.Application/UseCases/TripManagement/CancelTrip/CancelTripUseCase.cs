using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.TripManagement.CancelTrip
{
    public class CancelTripUseCase
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;

        public CancelTripUseCase(ITripRepository tripRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator)
        {
            _tripRepository = tripRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(CancelTripCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.CancelReason))
                throw new BusinessException("Reason must be filled.", ErrorCodes.TripReasonRequired);

            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var trip = await _tripRepository.GetByIdAsync(request.TripId);
            if (trip == null)
                throw new NotFoundException("Trip is not found.", ErrorCodes.TripNotFound);
            if (trip.OrganizationId != organizationId)
                throw new NotFoundException("Trip is not part of Organization.", ErrorCodes.TripNotOwned);

            trip.Cancel(request.CancelReason);
            await _tripRepository.UpdateAsync(trip);

            return new BaseResponse<bool>()
            {
                Success = true,
                Message = "Trip cancelled successfully.",
                Data = true
            };
        }
    }
}
