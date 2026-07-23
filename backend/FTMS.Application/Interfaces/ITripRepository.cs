using FTMS.Domain.Entities;

namespace FTMS.Application.Interfaces
{
    public interface ITripRepository
    {
        Task AddAsync(Trip trip);
        Task<Trip?> GetByIdAsync(Guid tripId);
        Task<Trip?> GetByBookingIdAsync(Guid bookingId);
        Task UpdateAsync(Trip trip);
        Task<bool> ExistsAsync(Guid tripId);
        Task<bool> HasActiveTripForVehicleAsync(Guid vehicleId, Guid tripId);
        Task<bool> HasEarlierReadyTripAsync(Guid vehicleId, Guid tripId);
    }
}
