using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.BookingManagement.Dto;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Booking.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid bookingId)
        {
            return await _context.Booking.AnyAsync(b => b.BookingId == bookingId);
        }

        public async Task<List<Booking>> GetAllAsync(Guid? organizationId = null, BookingStatusEnum? status = null)
        {
            IQueryable<Booking> query = _context.Booking.Include(b => b.Passengers).AsNoTracking();

            if (organizationId.HasValue)
            {
                var nonNullableStatus = organizationId.Value;
                query = query.Where(b => b.OrganizationId == nonNullableStatus);
            }

            if (status.HasValue)
            {
                var nonNullableStatus = status.Value;
                query = query.Where(b => b.Status == nonNullableStatus);
            }

            return await query.ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId)
        {
            return await _context.Booking.Include(b => b.Passengers).SingleOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<Booking?> GetByBookingNumberAsync(string bookingNumber)
        {
            return await _context.Booking.Where(b => b.BookingNumber == bookingNumber).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Booking.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasVehicleOverlapAsync(Guid vehicleId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            return await _context.Booking
                .AnyAsync(x =>
                    x.VehicleId == vehicleId &&
                    x.Status != BookingStatusEnum.Cancelled &&
                    x.Status != BookingStatusEnum.Completed &&
                    x.EstimatedDepartureTime < estimatedArrivalTime &&
                    x.EstimatedArrivalTime > estimatedDepartureTime);
        }

        public async Task<bool> HasDriverOverlapAsync(Guid driverPersonId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            return await _context.Booking
               .AnyAsync(x =>
                   x.DriverPersonId == driverPersonId &&
                   x.Status != BookingStatusEnum.Cancelled &&
                   x.Status != BookingStatusEnum.Completed &&
                   x.EstimatedDepartureTime < estimatedArrivalTime &&
                   x.EstimatedArrivalTime > estimatedDepartureTime);
        }

        public async Task<bool> HasRegularPassengerOverlapAsync(Guid personId, DateTimeOffset estimatedDepartureTime, DateTimeOffset estimatedArrivalTime)
        {
            return await _context.Booking
               .AnyAsync(x =>
                   x.Status != BookingStatusEnum.Cancelled &&
                   x.Status != BookingStatusEnum.Completed &&
                   x.EstimatedDepartureTime < estimatedArrivalTime &&
                   x.EstimatedArrivalTime > estimatedDepartureTime &&
                   x.Passengers.Any(p => p.PersonId == personId));
        }

        public async Task<BookingDetailDto?> GetDetailByIdAsync(Guid bookingId)
        {
            var bookingDetail = await (
                from b in _context.Booking

                join v in _context.Vehicles
                    on b.VehicleId equals v.VehicleId

                join d in _context.Persons
                    on b.DriverPersonId equals d.PersonId into drivers
                from driver in drivers.DefaultIfEmpty()

                join t in _context.Trips
                    on b.BookingId equals t.BookingId into trips
                from trip in trips.DefaultIfEmpty()

                join c in _context.Persons
                    on b.CreatedByPersonId equals c.PersonId

                where b.BookingId == bookingId

                select new BookingDetailDto
                {
                    OrganizatinID = b.OrganizationId,
                    BookingId = b.BookingId,
                    BookingNumber = b.BookingNumber,

                    VehicleId = v.VehicleId,
                    VehicleCode = v.VehicleCode,

                    DriverPersonId = driver != null ? driver.PersonId : null,
                    DriverName = driver != null ? driver.Name : string.Empty,

                    CreatedByPersonId = c.PersonId,
                    CreatedByPersonName = c.Name,

                    DestinationLocation = b.DestinationLocation,
                    EstimatedDepartureTime = b.EstimatedDepartureTime,
                    EstimatedArrivalTime = b.EstimatedArrivalTime,

                    Status = b.Status.ToString(),
                })
                .SingleOrDefaultAsync();

            if (bookingDetail is null)
            {
                return null;
            }

            var booking = await _context.Booking
                .Include(x => x.Passengers)
                .SingleAsync(x => x.BookingId == bookingId);

            var personIds = booking.Passengers
                .Where(x => x.PersonId.HasValue)
                .Select(x => x.PersonId!.Value)
                .ToList();

            var persons = await _context.Persons
                .Where(x => personIds.Contains(x.PersonId))
                .ToDictionaryAsync(x => x.PersonId);

            bookingDetail.Passengers = booking.Passengers
                .Select(x => new BookingPassengerDto
                {
                    BookingPassengerId = x.BookingPassengerId,
                    BookingId = x.BookingId,

                    PersonId = x.PersonId,
                    PersonName = x.PersonId.HasValue
                        ? persons[x.PersonId.Value].Name
                        : null,
                    PersonPhone = x.PersonId.HasValue
                        ? persons[x.PersonId.Value].Phone
                        : null,

                    GuestName = x.GuestName,
                    GuestPhone = x.GuestPhone,

                    PassengerType = x.PassengerType.ToString(),
                    PickupLocation = x.PickupLocation
                })
                .ToList();

            return bookingDetail;
        }

        public async Task<List<BookingDetailDto>> GetAllBookingAsync(Guid? organizationId = null, BookingStatusEnum? status = null, DateTimeOffset? dateTime = null)
        {
            var query =
                from b in _context.Booking.AsNoTracking()

                join v in _context.Vehicles
                    on b.VehicleId equals v.VehicleId

                join d in _context.Persons
                    on b.DriverPersonId equals d.PersonId into drivers
                from driver in drivers.DefaultIfEmpty()

                join c in _context.Persons
                    on b.CreatedByPersonId equals c.PersonId

                join t in _context.Trips
                    on b.BookingId equals t.BookingId into trips
                from trip in trips.DefaultIfEmpty()

                select new
                {
                    Booking = b,
                    Vehicle = v,
                    Driver = driver,
                    CreatedBy = c,
                    Trip = trip
                };


            if (organizationId.HasValue)
            {
                query = query.Where(x =>
                    x.Booking.OrganizationId == organizationId.Value);
            }


            if (status.HasValue)
            {
                query = query.Where(x =>
                    x.Booking.Status == status.Value);
            }


            if (dateTime.HasValue)
            {
                var localDate = dateTime.Value.Date;

                var startLocal = new DateTimeOffset(localDate, TimeZoneInfo.Local.GetUtcOffset(localDate));
                var endLocal = startLocal.AddDays(1);

                var startUtc = startLocal.ToUniversalTime();
                var endUtc = endLocal.ToUniversalTime();

                query = query.Where(x =>
                x.Booking.EstimatedDepartureTime >= startUtc &&
                x.Booking.EstimatedDepartureTime < endUtc);
            }


            return await query
                .OrderByDescending(x => x.Booking.CreatedAt)
                .Select(x => new BookingDetailDto
                {
                    BookingId = x.Booking.BookingId,
                    BookingNumber = x.Booking.BookingNumber,

                    VehicleId = x.Vehicle.VehicleId,
                    VehicleCode = x.Vehicle.VehicleCode,


                    DriverPersonId = x.Driver != null
                        ? x.Driver.PersonId
                        : null,

                    DriverName = x.Driver != null
                        ? x.Driver.Name
                        : string.Empty,


                    CreatedByPersonId = x.CreatedBy.PersonId,
                    CreatedByPersonName = x.CreatedBy.Name,


                    DestinationLocation = x.Booking.DestinationLocation,

                    EstimatedDepartureTime = x.Booking.EstimatedDepartureTime,
                    EstimatedArrivalTime = x.Booking.EstimatedArrivalTime,


                    Status = x.Booking.Status.ToString(),

                    StatusTrip = x.Trip != null ? x.Trip.Status.ToString() : string.Empty

                })
                .ToListAsync();
        }
    }
}
