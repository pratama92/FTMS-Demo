using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.Common
{
    public class PersonValidator : IPersonValidator
    {
        private readonly IBookingRepository _bookingRepository;

        public PersonValidator(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task ValidateDriverAvailabilityAsync(Guid driverPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            var exists = await _bookingRepository.HasDriverOverlapAsync(driverPersonId, estimatedDepartureTime, estimatedArrivalTime);
            if (exists)
                throw new BusinessException("Driver already asigned to overlap booking.", ErrorCodes.DriverAlreadyAssigned);
        }

        public async Task ValidateRegularPassengerAvailabilityAsync(Guid regularPassengerPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            var exists = await _bookingRepository.HasRegularPassengerOverlapAsync(regularPassengerPersonId, estimatedDepartureTime, estimatedArrivalTime);
            if (exists)
                throw new BusinessException("Passenger already asigned to overlap booking.", ErrorCodes.PassengerAlreadyAdded);
        }
    }
}
