namespace FTMS.API.Dto.Booking.Request
{
    public class CreateBookingRequest
    {
        public Guid VehicleId { get; set; }
        public string DestinationLocation { get; set; } = string.Empty;
        public DateTimeOffset EstimatedDepartureTime { get; set; }
        public DateTimeOffset EstimatedArrivalTime { get; set; }
    }
}
