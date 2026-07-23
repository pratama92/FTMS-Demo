using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public class Trip
    {
        public Guid TripId { get; private set; }

        // Source booking
        public Guid BookingId { get; private set; }
        public Booking Booking { get; private set; } = null!;

        // Execution vehicle
        public Guid VehicleId { get; private set; }
        public Vehicle Vehicle { get; private set; } = null!;

        public Guid? DriverPersonId { get; private set; }
        public Person Driver { get; private set; } = null!;
        public TripStatusEnum Status { get; private set; }

        public DateTimeOffset? StartedAt { get; private set; }
        public DateTimeOffset? CompletedAt { get; private set; }

        public string? CancellationReason { get; private set; }

        public Guid OrganizationId { get; private set; }
        public Organization Organization { get; private set; } = null!;

        // audit table
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        private Trip()
        {
        }

        public static Trip Create(
            Guid organizationId,
            Guid bookingId,
            Guid vehicleId,
            Guid? driverPersonId
            )
        {
            if (organizationId == Guid.Empty)
                throw new BusinessException("Organization is required.", ErrorCodes.OrganizationRequired);

            if (bookingId == Guid.Empty)
                throw new BusinessException("Booking is required.", ErrorCodes.BookingRequired);

            if (vehicleId == Guid.Empty)
                throw new BusinessException("Vehicle is required.", ErrorCodes.VehicleRequired);

            if (driverPersonId == Guid.Empty)
                throw new BusinessException("Driver is required.", ErrorCodes.DriverRequired);

            return new Trip
            {
                TripId = Guid.NewGuid(),
                BookingId = bookingId,
                VehicleId = vehicleId,
                DriverPersonId = driverPersonId,
                OrganizationId = organizationId,
                Status = TripStatusEnum.Ready,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }

        public void Start(DateTimeOffset startedAt)
        {
            if (Status != TripStatusEnum.Ready)
                throw new BusinessException("Trip cannot be started.", ErrorCodes.InvalidStatus);

            if (startedAt == default)
                throw new BusinessException("Start time is required.", ErrorCodes.ValidationFailed);

            Status = TripStatusEnum.EnRoute;
            StartedAt = startedAt;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Finish(DateTimeOffset finishedAt)
        {
            if (Status != TripStatusEnum.EnRoute)
                throw new BusinessException("Trip cannot be completed.", ErrorCodes.InvalidStatus);

            if (finishedAt == default)
                throw new BusinessException("Finish time is required.", ErrorCodes.ValidationFailed);

            if (StartedAt.HasValue && finishedAt < StartedAt.Value)
                throw new BusinessException("Finish time cannot be earlier than start time.", ErrorCodes.ValidationFailed);

            Status = TripStatusEnum.Completed;
            CompletedAt = finishedAt;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Cancel(string reason)
        {
            if (Status == TripStatusEnum.Completed)
                throw new BusinessException("Completed trip cannot be cancelled.", ErrorCodes.InvalidStatus);

            if (string.IsNullOrWhiteSpace(reason))
                throw new BusinessException("Cancellation reason is required.", ErrorCodes.RequiredField);

            Status = TripStatusEnum.Cancelled;
            CancellationReason = reason.Trim();
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}