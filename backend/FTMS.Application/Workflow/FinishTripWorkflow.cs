using FTMS.Application.Common;
using FTMS.Application.UseCases.BookingManagement.CompleteBooking;
using FTMS.Application.UseCases.TripManagement.FinishTrip;

namespace FTMS.Application.Workflow
{
    public class FinishTripWorkflow
    {
        private readonly FinishTripUseCase _finishTripUseCase;
        private readonly CompleteBookingUseCase _completeBookingUseCase;

        public FinishTripWorkflow(FinishTripUseCase finishTripUseCase, CompleteBookingUseCase completeBookingUseCase)
        {
            _finishTripUseCase = finishTripUseCase;
            _completeBookingUseCase = completeBookingUseCase;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid tripId, DateTimeOffset finishTimeRequest)
        {
            var finishTrip = await _finishTripUseCase.ExecuteAsync(tripId, finishTimeRequest);
          
            var completeBooking = await _completeBookingUseCase.ExecuteAsync(finishTrip.Data);
            
            return new BaseResponse<bool>()
            {
                Success = true,
                Message = "Trip is finished and booking is completed successfully.",
                Data = true
            };
        }
    }
}
