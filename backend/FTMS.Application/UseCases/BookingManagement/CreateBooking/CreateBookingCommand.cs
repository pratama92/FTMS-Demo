
namespace FTMS.Application.UseCases.BookingManagement.CreateBooking
{
    public class CreateBookingCommand
    {
        public Guid VehicleId { get; set; }
        public string DestinationLocation { get; set; } = string.Empty;
        public DateTimeOffset EstimatedDepartureTime { get; set; }
        public DateTimeOffset EstimatedArrivalTime { get; set; }
    }
}
