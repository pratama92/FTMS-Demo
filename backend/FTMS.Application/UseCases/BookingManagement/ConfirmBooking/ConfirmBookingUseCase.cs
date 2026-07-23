using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.ConfirmBooking
{
    public class ConfirmBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValdiator;
        private readonly ICurrentUser _currentUser;

        public ConfirmBookingUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser)
        {
            _bookingRepository = bookingRepository;
            _organizationValdiator = organizationValdiator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValdiator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                throw new BusinessException("Booking not found.", ErrorCodes.BookingNotFound);
            }

            if (booking.OrganizationId != organizationId)
            {
                throw new BusinessException("Booking not part of Organization.", ErrorCodes.BookingNotOwned);
            }

            booking.Confirm();

            await _bookingRepository.UpdateAsync(booking);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Booking confirmed successfully",
                Data = true
            };
        }
    }
}
