using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public sealed partial class Booking
    {
        public void Cancel()
        {
            EnsureBookingNotCompleted();

            EnsureBookingNotConfirmed();

            Status = BookingStatusEnum.Cancelled;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Confirm()
        {
            EnsureBookingNotCancelled();

            EnsureBookingNotCompleted();

            EnsureBookingNotConfirmed();

            if (DriverPersonId == null)
                throw new BusinessException("Booking requires a driver before confirmation.", ErrorCodes.DriverRequired);

            if (!_passengers.Any())
                throw new BusinessException("Booking requires passengers before confirmation.", ErrorCodes.PassengerRequired);

            Status = BookingStatusEnum.Confirmed;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Complete()
        {
            if (Status != BookingStatusEnum.Confirmed)
                throw new BusinessException("Only confirmed bookings can be completed.", ErrorCodes.InvalidStatus);

            Status = BookingStatusEnum.Completed;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

    }
}
