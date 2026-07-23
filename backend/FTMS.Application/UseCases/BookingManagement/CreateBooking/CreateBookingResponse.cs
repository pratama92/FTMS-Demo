using System;
using System.Collections.Generic;
using System.Text;

namespace FTMS.Application.UseCases.BookingManagement.CreateBooking
{
    public class CreateBookingResponse
    {
        public Guid BookingId { get; set; }
        public string BookingNumber { get; set; } = string.Empty;
    }
}
