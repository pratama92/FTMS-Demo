using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.CancelBooking
{
    public class CancelBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public CancelBookingUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _bookingRepository = bookingRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Booking not found.", ErrorCodes.BookingNotFound);

            if (booking.OrganizationId != organizationId)
                throw new NotFoundException("Booking is not part of organization.", ErrorCodes.BookingNotOwned);

            booking.Cancel();

            await _bookingRepository.UpdateAsync(booking);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Booking cancelled successfully",
                Data = true
            };
        }
    }
}
