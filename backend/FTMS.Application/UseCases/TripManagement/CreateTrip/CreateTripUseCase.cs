using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.TripManagement.CreateTrip
{
    public class CreateTripUseCase
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IBookingRepository _bookingRepository;

        public CreateTripUseCase(ITripRepository tripRepository, ICurrentUser currentUser, IBookingRepository bookingRepository)
        {
            _tripRepository = tripRepository;
            _currentUser = currentUser;
            _bookingRepository = bookingRepository;
        }

        public async Task<BaseResponse<Guid>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;

            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new BusinessException("Booking is not found.", ErrorCodes.BookingNotFound);

            booking.EnsureBookingConfirmed();

            var trip = Trip.Create(organizationId, booking.BookingId, booking.VehicleId, booking.DriverPersonId);

            await _tripRepository.AddAsync(trip);

            return new BaseResponse<Guid>
            {
                Success = true,
                Message = "Trip created successfully.",
                Data = trip.TripId
            };
        }
    }
}