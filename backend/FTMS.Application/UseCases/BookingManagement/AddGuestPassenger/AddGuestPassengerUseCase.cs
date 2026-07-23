using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.AddGuestPassenger
{
    public class AddGuestPassengerUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValdiator;
        private readonly ICurrentUser _currentUser;
        private readonly IVehicleValidator _vehicleValidator;

        public AddGuestPassengerUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser, IVehicleValidator vehicleValidator)
        {
            _bookingRepository = bookingRepository;
            _organizationValdiator = organizationValdiator;
            _currentUser = currentUser;
            _vehicleValidator = vehicleValidator;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(AddGuestPassengerCommand request)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValdiator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found.", ErrorCodes.BookingNotFound);
            }

            var currentPassengerCount = booking.Passengers.Count;
            await _vehicleValidator.ValidateSeatAsync(booking.VehicleId, currentPassengerCount);

            booking.AddGuestPassenger(request.GuestName, request.GuestPhone, request.PickupLocation);

            await _bookingRepository.SaveChangesAsync();

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Passenger added successfully",
                Data = true,
            };
        }
    }
}
