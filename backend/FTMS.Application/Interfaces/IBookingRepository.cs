using FTMS.Application.UseCases.BookingManagement.Dto;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;

namespace FTMS.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task<bool> ExistsAsync(Guid bookingId);
        Task<List<Booking>> GetAllAsync(Guid? organizationId = null, BookingStatusEnum? status = null);
        Task<List<BookingDetailDto>> GetAllBookingAsync(Guid? organizationId = null, BookingStatusEnum? status = null, DateTimeOffset? dateTime = null);
        Task<Booking?> GetByBookingNumberAsync(string bookingNumber);
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<BookingDetailDto?> GetDetailByIdAsync(Guid bookingId);
        Task<bool> HasVehicleOverlapAsync(Guid vehicleId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
        Task<bool> HasDriverOverlapAsync(Guid driverPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
        Task<bool> HasRegularPassengerOverlapAsync(Guid personId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime);
        Task UpdateAsync(Booking booking);
        Task SaveChangesAsync();
    }
}