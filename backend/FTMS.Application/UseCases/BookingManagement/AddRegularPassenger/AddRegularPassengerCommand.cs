namespace FTMS.Application.UseCases.BookingManagement.AddRegularPassenger
{
    public class AddRegularPassengerCommand
    {
        public Guid BookingId { get; set; }
        public Guid PersonId { get; set; }
        public string PickupLocation { get; set; } = string.Empty;
    }
}
