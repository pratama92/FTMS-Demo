namespace FTMS.Application.UseCases.TripManagement.CancelTrip
{
    public class CancelTripCommand
    {
        public Guid TripId { get; set; }
        public string CancelReason { get; set; } = string.Empty;
    }
}
