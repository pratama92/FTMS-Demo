namespace FTMS.API.Dto.Booking.Request
{
    public class ChangePickupLocationRequest
    {
        public Guid BookingPassengerId{ get; set; }
        public string PickupLocation { get; set; } = string.Empty;
    }
}
