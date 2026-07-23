namespace FTMS.API.Dto.Booking.Request
{
    public class AddGuestPassengerRequest
    {
        public string GuestName { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;
        public string PickupLocation { get; set; } = string.Empty;
    }
}
