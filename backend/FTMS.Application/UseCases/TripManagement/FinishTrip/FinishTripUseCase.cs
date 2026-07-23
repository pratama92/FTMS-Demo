using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.TripManagement.FinishTrip
{
    public class FinishTripUseCase
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;

        public FinishTripUseCase(ITripRepository tripRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator)
        {
            _tripRepository = tripRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
        }

        public async Task<BaseResponse<Guid>> ExecuteAsync(Guid tripId, DateTimeOffset finishTimeRequest)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
                throw new NotFoundException("Trip is not found.", ErrorCodes.TripNotFound);
            if (trip.OrganizationId != organizationId)
                throw new NotFoundException("Trip is not part of Organization.", ErrorCodes.TripNotOwned);

            var actualFinishedAt = finishTimeRequest == default ? DateTimeOffset.UtcNow : finishTimeRequest;

            trip.Finish(actualFinishedAt);
            await _tripRepository.UpdateAsync(trip);

            return new BaseResponse<Guid>()
            {
                Success = true,
                Message = "Trip completed successfully.",
                Data = trip.BookingId
            };
        }
    }
}
