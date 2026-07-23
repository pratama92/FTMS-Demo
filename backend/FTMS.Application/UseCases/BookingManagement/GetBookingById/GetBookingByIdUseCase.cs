using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.BookingManagement.Dto;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.GetBookingById
{
    public class GetBookingByIdUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetBookingByIdUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _bookingRepository = bookingRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<BookingResponse>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetDetailByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Booking not found.", ErrorCodes.BookingNotFound);
            if (booking.OrganizatinID != organizationId)
                throw new NotFoundException("Booking not part of organization.", ErrorCodes.BookingNotOwned);

            var bookingResponse = new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingNumber = booking.BookingNumber,
                VehicleId = booking.VehicleId,
                VehicleCode = booking.VehicleCode,
                DriverPersonID = booking.DriverPersonId,
                DriverName = booking.DriverName,
                CreatedByPersonName = booking.CreatedByPersonName,
                DestinationLocation = booking.DestinationLocation,
                EstimatedDepartureTime = booking.EstimatedDepartureTime,
                EstimatedArrivalTime = booking.EstimatedArrivalTime,
                Status = booking.Status,

                Passengers = booking.Passengers
                    .Select(x => new BookingPassengerResponse
                    {
                        BookingPassengerId = x.BookingPassengerId,
                        BookingId = x.BookingId,
                        PersonName = x.PersonName,
                        PersonPhone = x.PersonPhone,
                        GuestName = x.GuestName,
                        GuestPhone = x.GuestPhone,
                        PassengerType = x.PassengerType,
                        PickupLocation = x.PickupLocation
                    }).ToList()
            };

            return new BaseResponse<BookingResponse>
            {
                Success = true,
                Message = "Booking retrieved successfully",
                Data = bookingResponse,
            };
        }
    }
}
