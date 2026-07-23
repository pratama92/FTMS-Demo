namespace FTMS.Application.UseCases.BookingManagement.Dto
{
    public class BookingResponse
    {
        public Guid BookingId { get; set; }
        public string BookingNumber { get; set; } = string.Empty;
        public Guid VehicleId { get; set; }
        public string VehicleCode { get; set; } = string.Empty;
        public Guid? DriverPersonID { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string CreatedByPersonName { get; set; } = string.Empty;
        public string DestinationLocation { get; set; } = string.Empty;
        public DateTimeOffset EstimatedDepartureTime { get; set; }
        public DateTimeOffset EstimatedArrivalTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string StatusTrip { get; set; } = string.Empty;
        public List<BookingPassengerResponse> Passengers { get; set; } = new List<BookingPassengerResponse>();
    }

    public class BookingPassengerResponse
    {
        public Guid BookingPassengerId { get; set; }
        public Guid BookingId { get; set; }
        public string? PersonName { get; set; } = string.Empty;
        public string? PersonPhone { get; set; } = string.Empty;
        public string? GuestName { get; set; } = string.Empty;
        public string? GuestPhone { get; set; } = string.Empty;
        public string PassengerType { get; set; } = string.Empty;
        public string PickupLocation { get; set; } = string.Empty;
    }
}
