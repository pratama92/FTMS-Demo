using FTMS.Domain.Entities;
using FTMS.Domain.Enums;

namespace FTMS.Application.UseCases.BookingManagement.GetTripByBookingId
{
    public class GetTripByBookingIdResponse
    {
        public Guid TripId { get;  set; }
        public Guid BookingId { get;  set; }
        public Guid VehicleId { get;  set; }
        public Guid? DriverPersonId { get;  set; }
        public TripStatusEnum Status { get;  set; }
        public DateTimeOffset? StartedAt { get;  set; }
        public DateTimeOffset? CompletedAt { get;  set; }
        public string? CancellationReason { get;  set; }
        public Guid OrganizationId { get;  set; }
        public DateTimeOffset CreatedAt { get;  set; }
    }
}
