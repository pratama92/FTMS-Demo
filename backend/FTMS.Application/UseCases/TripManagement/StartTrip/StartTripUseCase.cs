using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.TripManagement.StartTrip
{
    public class StartTripUseCase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ITripRepository _tripRepository;
        private readonly IVehicleValidator _vehicleValidator;


        public StartTripUseCase(ITripRepository tripRepository, ICurrentUser currentUser, IOrganizationValidator organizationValidator, IVehicleValidator vehicleValidator)
        {
            _tripRepository = tripRepository;
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
            _vehicleValidator = vehicleValidator;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid tripId, DateTimeOffset startTripRequest)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var trip = await _tripRepository.GetByIdAsync(tripId);
            if (trip == null)
                throw new NotFoundException("Trip is not found.", ErrorCodes.TripNotFound);
            if (trip.OrganizationId != organizationId)
                throw new NotFoundException("Trip is not part of Organization.", ErrorCodes.TripNotOwned);

            await _vehicleValidator.ValidateVehicleAvailabilityAsync(trip.VehicleId);
            await _vehicleValidator.ValidateTripAvailabilityAsync(trip.VehicleId, trip.TripId);
            await _vehicleValidator.ValidateTripStartSequenceAsync(trip.VehicleId, trip.TripId);

            var actualStartedAt = startTripRequest == default ? DateTimeOffset.UtcNow : startTripRequest;

            trip.Start(actualStartedAt);
            await _tripRepository.UpdateAsync(trip);

            return new BaseResponse<bool>()
            {
                Success = true,
                Message = "Trip started successfully.",
                Data = true
            };
        }
    }
}
