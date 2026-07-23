using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.BookingManagement.Dto;

namespace FTMS.Application.UseCases.BookingManagement.GetBookings
{
    public class GetBookingsUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetBookingsUseCase(IBookingRepository bookingsRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser)
        {
            _bookingRepository = bookingsRepository;
            _organizationValidator = organizationValdiator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<List<BookingResponse>>> ExecuteAsync(DateTimeOffset? dateTimeFilter = null)
        {

            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var bookingList = await _bookingRepository.GetAllBookingAsync(organizationId: organizationId, dateTime: dateTimeFilter);

            var bookingsResponse = bookingList.Select(booking => new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingNumber = booking.BookingNumber,
                VehicleId = booking.VehicleId,
                VehicleCode = booking.VehicleCode,
                DriverPersonID = booking.DriverPersonId,
                DriverName = booking.DriverName,
                DestinationLocation = booking.DestinationLocation,
                EstimatedDepartureTime = booking.EstimatedDepartureTime,
                EstimatedArrivalTime = booking.EstimatedArrivalTime,
                CreatedByPersonName = booking.CreatedByPersonName,
                Status = booking.Status.ToString(),
                StatusTrip = booking.StatusTrip,
            }).ToList();

            return new BaseResponse<List<BookingResponse>>
            {
                Success = true,
                Message = "Bookings retrieved successfully",
                Data = bookingsResponse
            };
        }
    }
}
