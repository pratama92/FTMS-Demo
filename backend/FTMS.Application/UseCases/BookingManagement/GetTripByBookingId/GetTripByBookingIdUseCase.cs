using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.GetTripByBookingId
{
    public class GetTripByBookingIdUseCase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ITripRepository _tripRepository;

        public GetTripByBookingIdUseCase(ICurrentUser currentUser, IOrganizationValidator organizationValidator, ITripRepository tripRepository)
        {
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
            _tripRepository = tripRepository;
        }

        public async Task<BaseResponse<GetTripByBookingIdResponse>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var trip = await _tripRepository.GetByBookingIdAsync(bookingId);
            if (trip == null)
                throw new NotFoundException("Trip is not found.", ErrorCodes.TripNotFound);
            if (trip.OrganizationId != organizationId)
                throw new NotFoundException("Trip is not part of Organization", ErrorCodes.TripNotOwned);

            var response = new GetTripByBookingIdResponse()
            {
                TripId = trip.TripId,
                BookingId = bookingId,
                DriverPersonId = trip.DriverPersonId,
                StartedAt = trip.StartedAt,
                CompletedAt = trip.CompletedAt,
                Status = trip.Status,
                CancellationReason = trip.CancellationReason,
                CreatedAt = trip.CreatedAt,
                OrganizationId = organizationId,
                VehicleId = trip.VehicleId,
            };

            return new BaseResponse<GetTripByBookingIdResponse>()
            {
                Success = true,
                Message = "Trip retrieved succesfully.",
                Data = response
            };
        }
    }
}
