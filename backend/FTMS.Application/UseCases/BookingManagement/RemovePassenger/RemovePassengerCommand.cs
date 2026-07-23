namespace FTMS.Application.UseCases.BookingManagement.RemovePassenger
{
    public class RemovePassengerCommand
    {
        public Guid BookingId { get; set; }
        public Guid BookingPassengerId { get; set; }
    }
}
