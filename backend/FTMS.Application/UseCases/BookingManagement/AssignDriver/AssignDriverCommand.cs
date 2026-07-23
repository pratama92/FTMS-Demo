namespace FTMS.Application.UseCases.BookingManagement.AssignDriver
{
    public class AssignDriverCommand
    {
        public Guid BookingId { get; set; }
        public Guid PersonId { get; set; }
    }
}
