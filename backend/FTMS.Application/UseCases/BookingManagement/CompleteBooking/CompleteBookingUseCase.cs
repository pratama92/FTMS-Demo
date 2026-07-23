using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.BookingManagement.CompleteBooking
{
    public class CompleteBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public CompleteBookingUseCase(IBookingRepository bookingRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser)
        {
            _bookingRepository = bookingRepository; 
            _organizationValidator = organizationValdiator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid bookingId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var booking = await _bookingRepository.GetByIdAsync(bookingId);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found.", ErrorCodes.BookingNotFound);
            }

            if (booking.OrganizationId != organizationId)
            {
                throw new NotFoundException("Booking not part of Organization.", ErrorCodes.BookingNotOwned);
            }

            booking.Complete();

            await _bookingRepository.UpdateAsync(booking);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Booking completed successfully",
                Data = true
            };
        }
    }
}
