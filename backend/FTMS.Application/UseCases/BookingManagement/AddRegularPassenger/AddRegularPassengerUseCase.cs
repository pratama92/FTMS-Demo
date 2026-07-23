using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.AddRegularPassenger
{
    public class AddRegularPassengerUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;
        private readonly IVehicleValidator _vehicleValidator;
        private readonly IPersonValidator _personValidator;

        public AddRegularPassengerUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser, IVehicleValidator vehicleValidator, IPersonValidator personValidator)
        {
            _bookingRepository = bookingRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
            _vehicleValidator = vehicleValidator;
            _personValidator = personValidator;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(AddRegularPassengerCommand request)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null)
            {
                throw new NotFoundException("Booking not found.", ErrorCodes.BookingNotFound);
            }

            if (booking.OrganizationId != organizationId)
            {
                throw new NotFoundException("Booking not part of Organization.", ErrorCodes.BookingNotOwned);
            }

            await _personValidator.ValidateRegularPassengerAvailabilityAsync(request.PersonId, booking.EstimatedDepartureTime, booking.EstimatedArrivalTime);

            var currentPassengerCount = booking.Passengers.Count;
            await _vehicleValidator.ValidateSeatAsync(booking.VehicleId, currentPassengerCount);

            booking.AddRegularPassenger(request.PersonId, request.PickupLocation);

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
