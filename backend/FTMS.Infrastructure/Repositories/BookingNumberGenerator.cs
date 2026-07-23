using FTMS.Application.Interfaces;

namespace FTMS.Infrastructure.Services
{
    public class BookingNumberGenerator : IBookingNumberGenerator
    {
        public Task<string> GenerateAsync()
        {
            var bookingNumber = $"BK-{DateTime.UtcNow:yyyyMMddHHmmssfff}";

            return Task.FromResult(bookingNumber);
        }
    }
}