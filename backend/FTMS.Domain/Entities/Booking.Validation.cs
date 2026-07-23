using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public sealed partial class Booking
    {
        public void EnsureBookingConfirmed()
        {
            if (Status != BookingStatusEnum.Confirmed)
                throw new BusinessException("Booking is not Confirmed.", ErrorCodes.BookingNotConfirmed);
        }

        private void EnsureBookingNotCancelled()
        {
            if (Status == BookingStatusEnum.Cancelled)
            {
                throw new BusinessException("Booking is Already Cancelled.", ErrorCodes.BookingCancelled);
            }
        }

        private void EnsureBookingNotCompleted()
        {
            if (Status == BookingStatusEnum.Completed)
            {
                throw new BusinessException("Booking is Already Completed.", ErrorCodes.BookingCompleted);
            }
        }

        private void EnsureBookingNotConfirmed()
        {
            if (Status == BookingStatusEnum.Confirmed)
            {
                throw new BusinessException("Booking is Already Confirmed.", ErrorCodes.BookingConfirmed);
            }
        }

        private static void Validate(
            Guid organizationId,
            string bookingNumber,
            Guid vehicleId,
            Guid createdByPersonId,
            string destinationLocation,
            DateTimeOffset estimatedDepartureTime,
            DateTimeOffset estimatedArrivalTime)
        {
            if (organizationId == Guid.Empty)
                throw new BusinessException("Organization is required.", ErrorCodes.OrganizationRequired);

            if (vehicleId == Guid.Empty)
                throw new BusinessException("Vehicle is required.", ErrorCodes.VehicleRequired);

            if (createdByPersonId == Guid.Empty)
                throw new BusinessException("Created by person is required.", ErrorCodes.BookingCreatedByPersonRequired);

            if (string.IsNullOrWhiteSpace(bookingNumber))
                throw new BusinessException("Booking number is required.", ErrorCodes.BookingNumberRequired);

            if (string.IsNullOrWhiteSpace(destinationLocation))
                throw new BusinessException("Destination is required.", ErrorCodes.BookingDestinationRequired);

            if (estimatedDepartureTime >= estimatedArrivalTime)
                throw new BusinessException("Estimated departure time must be before estimated arrival time.", ErrorCodes.InvalidTimeRange);
        }
    }
}
