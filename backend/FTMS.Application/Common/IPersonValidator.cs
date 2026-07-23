namespace FTMS.Application.Common
{
    public interface IPersonValidator
    {
        Task ValidateDriverAvailabilityAsync(Guid driverPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
        Task ValidateRegularPassengerAvailabilityAsync(Guid regularPassengerPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
    }
}
