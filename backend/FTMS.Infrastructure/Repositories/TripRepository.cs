using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid tripId)
        {
            return await _context.Trips.AnyAsync(x => x.TripId == tripId);
        }

        public async Task<Trip?> GetByBookingIdAsync(Guid bookingId)
        {
            return await _context.Trips.FirstOrDefaultAsync(x => x.BookingId == bookingId);
        }

        public async Task<Trip?> GetByIdAsync(Guid tripId)
        {
            return await _context.Trips.FirstOrDefaultAsync(x => x.TripId == tripId);
        }

        public async Task UpdateAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasActiveTripForVehicleAsync(Guid vehicleId, Guid tripId)
        {
            return await _context.Trips
                .AnyAsync(x =>
                    x.VehicleId == vehicleId &&
                    x.TripId != tripId &&
                    x.Status == TripStatusEnum.EnRoute);
        }

        public async Task<bool> HasEarlierReadyTripAsync(Guid vehicleId, Guid tripId)
        {
            var currentDepartureTime = await _context.Trips
                .Where(t => t.TripId == tripId)
                .Join(
                    _context.Booking,
                    t => t.BookingId,
                    b => b.BookingId,
                    (_, booking) => booking.EstimatedDepartureTime)
                .SingleAsync();

            return await _context.Trips
                .Join(
                    _context.Booking,
                    t => t.BookingId,
                    b => b.BookingId,
                    (trip, booking) => new
                    {
                        trip.VehicleId,
                        trip.TripId,
                        trip.Status,
                        booking.EstimatedDepartureTime
                    })
                .AnyAsync(x =>
                    x.VehicleId == vehicleId &&
                    x.TripId != tripId &&
                    x.Status == TripStatusEnum.Ready &&
                    x.EstimatedDepartureTime < currentDepartureTime);
        }
    }
}
