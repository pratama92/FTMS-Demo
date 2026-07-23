namespace FTMS.Application.UseCases.BookingManagement.AddGuestPassenger
{
    public class AddGuestPassengerCommand
    {
        public Guid BookingId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string GuestPhone { get; set; } = string.Empty;
        public string PickupLocation { get; set; } = string.Empty;
    }
}
