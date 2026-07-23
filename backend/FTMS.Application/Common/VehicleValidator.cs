using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.Common
{
    public class VehicleValidator : IVehicleValidator
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITripRepository _tripRepository;

        public VehicleValidator(IVehicleRepository vehicleRepository, IBookingRepository bookingRepository, ITripRepository tripRepository)
        {
            _vehicleRepository = vehicleRepository;
            _bookingRepository = bookingRepository;
            _tripRepository = tripRepository;
        }

        public async Task ValidateSeatAsync(Guid vehicleId, int currentCountPassenger)
        {
            var seatCapacity = await _vehicleRepository.GetSeatCapacityAsync(vehicleId);

            if (currentCountPassenger + 1 > seatCapacity)
            {
                throw new BusinessException("Vehicle capacity exceeded.", ErrorCodes.BookingCapacityExceeded);
            }
        }

        public async Task ValidateBookingAvailabilityAsync(Guid vehicleId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            var booking = await _bookingRepository.HasVehicleOverlapAsync(vehicleId, estimatedDepartureTime, estimatedArrivalTime);

            if (booking)
            {
                throw new BusinessException("Vehicle is already booked during this time range.", ErrorCodes.VehicleUnAvailable);
            }
        }

        public async Task ValidateVehicleAvailabilityAsync(Guid vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle != null)
                vehicle.EnsureAvailable();

        }

        public async Task ValidateTripAvailabilityAsync(Guid vehicleId, Guid tripId)
        {
            if (await _tripRepository.HasActiveTripForVehicleAsync(vehicleId, tripId))
                throw new BusinessException("Vehicle still in another Trip.", ErrorCodes.VehicleUnAvailable);
        }

        public async Task ValidateTripStartSequenceAsync(Guid vehicleId, Guid tripId)
        {
            if (await _tripRepository.HasEarlierReadyTripAsync(vehicleId, tripId))
                throw new BusinessException("Vehicle need to strat the earliest trip first.", ErrorCodes.VehicleUnAvailable);
        }
    }
}
