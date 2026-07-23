using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.AssignDriver
{
    public class AssignDriverUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IPersonValidator _personValdiator;

        public AssignDriverUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser, IPersonRepository personRepository, IPersonValidator personValdiator)
        {
            _bookingRepository = bookingRepository;
            _organizationValidator = organizationValdiator;
            _currentUser = currentUser;
            _personRepository = personRepository;
            _personValdiator = personValdiator;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(AssignDriverCommand request)
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

            // Check Person
            var driver = await _personRepository.GetByIdAsync(request.PersonId);
            if (driver == null)
            {
                throw new NotFoundException("Person not found.", ErrorCodes.PersonNotFound);
            }

            if (driver.OrganizationId != organizationId)
            {
                throw new NotFoundException("Driver is not part of Organizaion.", ErrorCodes.DriverNotOwned);
            }

            driver.EnsureCanDrive();

            await _personValdiator.ValidateDriverAvailabilityAsync(driver.PersonId, booking.EstimatedDepartureTime, booking.EstimatedArrivalTime);

            booking.AssignDriver(driver.PersonId);

            await _bookingRepository.UpdateAsync(booking);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Driver assigned successfully",
                Data = true
            };
        }
    }
}
