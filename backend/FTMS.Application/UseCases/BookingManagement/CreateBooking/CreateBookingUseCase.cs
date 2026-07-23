using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.CreateBooking
{
    public class CreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBookingNumberGenerator _bookingNumberGenerator;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;
        private readonly IVehicleValidator _vehicleValidator;

        public CreateBookingUseCase(IBookingRepository bookingRepository, IVehicleRepository vehicleRepository, IPersonRepository personRepository, IBookingNumberGenerator bookingNumberGenerator, IOrganizationValidator organizationValidator, ICurrentUser currentUser, IVehicleValidator vehicleValidator)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
            _personRepository = personRepository;
            _bookingNumberGenerator = bookingNumberGenerator;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
            _vehicleValidator = vehicleValidator;
        }

        public async Task<BaseResponse<CreateBookingResponse>> ExecuteAsync(CreateBookingCommand request)
        {
            // Organization
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            // Vehicle
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle is null)
            {
                throw new BusinessException("Vehicle not found.", ErrorCodes.VehicleNotFound);
            }

            if (vehicle.OrganizationId != organizationId)
            {
                throw new BusinessException("Vehicle not part of Organization.", ErrorCodes.VehicleNotOwned);
            }

            vehicle.EnsureAvailable();

            // vehicel availability in range time
            await _vehicleValidator.ValidateBookingAvailabilityAsync(vehicle.VehicleId, request.EstimatedDepartureTime, request.EstimatedArrivalTime);

            // Booking Number Generate
            var bookingNumber = await _bookingNumberGenerator.GenerateAsync();

            // Create Aggregate
            var booking = Booking.Create(
                organizationId,
                bookingNumber,
                request.VehicleId,
                _currentUser.PersonId, // who is the person login
                request.DestinationLocation,
                request.EstimatedDepartureTime,
                request.EstimatedArrivalTime);

            // Save
            await _bookingRepository.AddAsync(booking);

            // Response
            var response = new CreateBookingResponse()
            {
                BookingId = booking.BookingId,
                BookingNumber = booking.BookingNumber
            };

            return new BaseResponse<CreateBookingResponse>
            {
                Success = true,
                Message = "Booking created successfully.",
                Data = response
            };
        }
    }
}
