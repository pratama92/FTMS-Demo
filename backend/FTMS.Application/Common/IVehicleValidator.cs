namespace FTMS.Application.Common
{
    public interface IVehicleValidator
    {
        Task ValidateSeatAsync(Guid vehicleId, int currentCountPassenger);
        Task ValidateBookingAvailabilityAsync(Guid vehicleId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
        Task ValidateVehicleAvailabilityAsync(Guid vehicleId);
        Task ValidateTripAvailabilityAsync(Guid vehicleId, Guid tripId);
        Task ValidateTripStartSequenceAsync(Guid vehicleId, Guid tripId);
    }
}
