namespace FTMS.Application.UseCases.BookingManagement.ChangePickupLocation
{
    public class ChangePickupLocationCommand
    {
        public Guid BookingId { get; set; }
        public Guid BookingPassengerId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
    }
}
