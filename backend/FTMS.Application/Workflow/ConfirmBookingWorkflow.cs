using FTMS.Application.Common;
using FTMS.Application.UseCases.BookingManagement.ConfirmBooking;
using FTMS.Application.UseCases.TripManagement.CreateTrip;

namespace FTMS.Application.Workflow
{
    public class ConfirmBookingWorkflow
    {
        private readonly ConfirmBookingUseCase _confirmBookingUseCase;
        private readonly CreateTripUseCase _createTripUseCase;

        public ConfirmBookingWorkflow(
            ConfirmBookingUseCase confirmBookingUseCase,
            CreateTripUseCase createTripUseCase)
        {
            _confirmBookingUseCase = confirmBookingUseCase;
            _createTripUseCase = createTripUseCase;
        }

        public async Task<BaseResponse<ConfirmBookingResponse>> ExecuteAsync(Guid bookingId)
        {
            var confirmResult = await _confirmBookingUseCase.ExecuteAsync(bookingId);

            var tripResult = await _createTripUseCase.ExecuteAsync(bookingId);

            return new BaseResponse<ConfirmBookingResponse>
            {
                Success = true,
                Message = "Booking confirmed and Trip created successfully.",
                Data = new ConfirmBookingResponse
                {
                    BookingId = bookingId,
                    TripId = tripResult.Data
                }
            };
        }
    }
}
