namespace FTMS.API.Dto.Booking.Request
{
    public class AddRegularPassengerRequest
    {
        public Guid PersonId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
    }
}
