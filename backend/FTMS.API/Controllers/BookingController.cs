using FTMS.API.Dto.Booking.Request;
using FTMS.Application.UseCases.BookingManagement.AddGuestPassenger;
using FTMS.Application.UseCases.BookingManagement.AddRegularPassenger;
using FTMS.Application.UseCases.BookingManagement.AssignDriver;
using FTMS.Application.UseCases.BookingManagement.CancelBooking;
using FTMS.Application.UseCases.BookingManagement.ChangePickupLocation;
using FTMS.Application.UseCases.BookingManagement.CreateBooking;
using FTMS.Application.UseCases.BookingManagement.GetBookingById;
using FTMS.Application.UseCases.BookingManagement.GetBookings;
using FTMS.Application.UseCases.BookingManagement.GetTripByBookingId;
using FTMS.Application.UseCases.BookingManagement.RemoveDriver;
using FTMS.Application.UseCases.BookingManagement.RemovePassenger;
using FTMS.Application.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTMS.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize(Roles = "Dispatcher")]
    public class BookingController : ControllerBase
    {
        private readonly CreateBookingUseCase _createBookingUseCase;
        private readonly AssignDriverUseCase _assignDriverUseCase;
        private readonly AddGuestPassengerUseCase _addGuestPassengerUseCase;
        private readonly AddRegularPassengerUseCase _addRegularPassengerUseCase;
        private readonly RemoveDriverUseCase _removeDriverUseCase;
        private readonly RemovePassengerUseCase _removePassengerUseCase;
        private readonly CancelBookingUseCase _cancelBookingUseCase;
        private readonly ConfirmBookingWorkflow _confirmBookingWorkflow;
        private readonly GetBookingsUseCase _getBookingsUseCase;
        private readonly GetBookingByIdUseCase _getBookingByIdUseCase;
        private readonly GetTripByBookingIdUseCase _getTripByBookingIdUseCase;
        private readonly ChangePickupLocationUseCase _changePickupLocationUseCase;

        public BookingController(CreateBookingUseCase createBookingUseCase, AssignDriverUseCase assignDriverUseCase, RemoveDriverUseCase removeDriverUseCase, RemovePassengerUseCase removePassengerUseCase, CancelBookingUseCase cancelBookingUseCase, GetBookingsUseCase getBookingsUseCase, GetBookingByIdUseCase getBookingByIdUseCase, AddGuestPassengerUseCase addGuestPassengerUseCase, AddRegularPassengerUseCase addRegularPassengerUseCase, ConfirmBookingWorkflow confirmBookingWorkflow, GetTripByBookingIdUseCase getTripByBookingIdUseCase, ChangePickupLocationUseCase changePickupLocationUseCase)
        {
            _createBookingUseCase = createBookingUseCase;
            _assignDriverUseCase = assignDriverUseCase;
            _removeDriverUseCase = removeDriverUseCase;
            _removePassengerUseCase = removePassengerUseCase;
            _cancelBookingUseCase = cancelBookingUseCase;
            _getBookingsUseCase = getBookingsUseCase;
            _getBookingByIdUseCase = getBookingByIdUseCase;
            _addGuestPassengerUseCase = addGuestPassengerUseCase;
            _addRegularPassengerUseCase = addRegularPassengerUseCase;
            _confirmBookingWorkflow = confirmBookingWorkflow;
            _getTripByBookingIdUseCase = getTripByBookingIdUseCase;
            _changePickupLocationUseCase = changePickupLocationUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var command = new CreateBookingCommand()
            {
                VehicleId = request.VehicleId,
                DestinationLocation = request.DestinationLocation,
                EstimatedDepartureTime = request.EstimatedDepartureTime,
                EstimatedArrivalTime = request.EstimatedArrivalTime,
            };

            var result = await _createBookingUseCase.ExecuteAsync(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = await _getBookingsUseCase.ExecuteAsync();

            return Ok(command);
        }

        [HttpGet("day")]
        public async Task<IActionResult> GetAllDay([FromQuery] DateOnly date)
        {
            var dateTimeOffset = new DateTimeOffset(date.ToDateTime(TimeOnly.MinValue), TimeSpan.Zero);

            var command = await _getBookingsUseCase.ExecuteAsync(dateTimeOffset);

            return Ok(command);
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid bookingId)
        {
            var command = await _getBookingByIdUseCase.ExecuteAsync(bookingId);

            return Ok(command);
        }

        [HttpGet("{bookingId}/trip")]
        public async Task<IActionResult> GetTripById([FromRoute] Guid bookingId)
        {
            var command = await _getTripByBookingIdUseCase.ExecuteAsync(bookingId);

            return Ok(command);
        }

        [HttpPatch("{bookingId:guid}/changepickuplocation")]
        public async Task<IActionResult> ChangePickupLocation(Guid bookingId, [FromBody] ChangePickupLocationRequest request)
        {
            var command = new ChangePickupLocationCommand()
            {
                BookingId = bookingId,
                BookingPassengerId = request.BookingPassengerId,
                PickupLocation = request.PickupLocation
            };

            var result = await _changePickupLocationUseCase.ExecuteAsync(command);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/cancel")]
        public async Task<IActionResult> Cancel([FromRoute] Guid bookingId)
        {
            var result = await _cancelBookingUseCase.ExecuteAsync(bookingId);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/confirm")]
        public async Task<IActionResult> Confirm([FromRoute] Guid bookingId)
        {
            var result = await _confirmBookingWorkflow.ExecuteAsync(bookingId);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/assigndriver")]
        public async Task<IActionResult> AssignDriver(Guid bookingId, [FromBody] AssignDriverRequest request)
        {
            var command = new AssignDriverCommand()
            {
                BookingId = bookingId,
                PersonId = request.PersonId
            };

            var result = await _assignDriverUseCase.ExecuteAsync(command);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/removedriver")]
        public async Task<IActionResult> RemoveDriver([FromRoute] Guid bookingId)
        {
            var result = await _removeDriverUseCase.ExecuteAsync(bookingId);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/addguestpassenger")]
        public async Task<IActionResult> AddGuestPassenger(Guid bookingId, [FromBody] AddGuestPassengerRequest request)
        {
            var command = new AddGuestPassengerCommand()
            {
                BookingId = bookingId,
                GuestName = request.GuestName,
                GuestPhone = request.GuestPhone,
                PickupLocation = request.PickupLocation,
            };

            var result = await _addGuestPassengerUseCase.ExecuteAsync(command);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/addregularpassenger")]
        public async Task<IActionResult> AddRegularPassenger(Guid bookingId, [FromBody] AddRegularPassengerRequest request)
        {
            var command = new AddRegularPassengerCommand()
            {
                BookingId = bookingId,
                PersonId = request.PersonId,
                PickupLocation = request.PickupLocation
            };

            var result = await _addRegularPassengerUseCase.ExecuteAsync(command);

            return Ok(result);
        }

        [HttpPatch("{bookingId:guid}/removepassenger")]
        public async Task<IActionResult> RemovePassenger(Guid bookingId, [FromBody] RemovePassengerRequest request)
        {
            var command = new RemovePassengerCommand()
            {
                BookingId = bookingId,
                BookingPassengerId = request.BookingPassengerId
            };

            var result = await _removePassengerUseCase.ExecuteAsync(command);

            return Ok(result);
        }
    }
}
