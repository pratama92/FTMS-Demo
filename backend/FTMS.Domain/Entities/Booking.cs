using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities;

public sealed partial class Booking
{
    public Guid BookingId { get; private set; }

    public Guid OrganizationId { get; private set; }

    public string BookingNumber { get; private set; } = string.Empty;

    public Guid VehicleId { get; private set; }

    public Guid? DriverPersonId { get; private set; }

    public Guid CreatedByPersonId { get; private set; }

    public string DestinationLocation { get; private set; } = string.Empty;

    public DateTimeOffset EstimatedDepartureTime { get; private set; }

    public DateTimeOffset EstimatedArrivalTime { get; private set; }

    public BookingStatusEnum Status { get; private set; }

    public Organization Organization { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    // Navigation Properties
    public IReadOnlyCollection<BookingPassenger> Passengers => _passengers;

    private readonly List<BookingPassenger> _passengers = [];

    private Booking() { }

    private Booking(
        Guid organizationId,
        string bookingNumber,
        Guid vehicleId,
        Guid createdByPersonId,
        string destinationLocation,
        DateTimeOffset estimatedDepartureTime,
        DateTimeOffset estimatedArrivalTime)
    {
        Validate(
            organizationId,
            bookingNumber,
            vehicleId,
            createdByPersonId,
            destinationLocation,
            estimatedDepartureTime,
            estimatedArrivalTime);

        BookingId = Guid.NewGuid();
        OrganizationId = organizationId;
        BookingNumber = bookingNumber.Trim();
        VehicleId = vehicleId;
        CreatedByPersonId = createdByPersonId;
        DestinationLocation = destinationLocation.Trim();
        EstimatedDepartureTime = estimatedDepartureTime;
        EstimatedArrivalTime = estimatedArrivalTime;

        Status = BookingStatusEnum.Created;

        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static Booking Create(
        Guid organizationId,
        string bookingNumber,
        Guid vehicleId,
        Guid createdByPersonId,
        string destinationLocation,
        DateTimeOffset estimatedDepartureTime,
        DateTimeOffset estimatedArrivalTime)
    {
        return new Booking(
            organizationId,
            bookingNumber,
            vehicleId,
            createdByPersonId,
            destinationLocation,
            estimatedDepartureTime,
            estimatedArrivalTime);
    }

    public void AssignDriver(Guid driverPersonId)
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        if (driverPersonId == Guid.Empty)
            throw new BusinessException("Driver is required.", ErrorCodes.DriverRequired);

        if (_passengers.Any(x => x.PersonId == driverPersonId))
            throw new BusinessException("Assigned passenger cannot also be a driver.", ErrorCodes.PassengerIsDriver);

        if (DriverPersonId == driverPersonId)
        {
            throw new BusinessException("Driver already assigned.", ErrorCodes.DriverAlreadyAssigned);
        }

        if (Status == BookingStatusEnum.Created)
        {
            Status = BookingStatusEnum.Pending;
        }

        DriverPersonId = driverPersonId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void RemoveDriver()
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        DriverPersonId = null;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AddRegularPassenger(Guid personId, string pickupLocation)
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        if (personId == Guid.Empty)
            throw new BusinessException("Person is required.", ErrorCodes.PersonRequired);

        if (_passengers.Any(x => x.PersonId == personId))
            throw new BusinessException("Passenger already exists.", ErrorCodes.PassengerExists);

        if (DriverPersonId == personId)
            throw new BusinessException("Assigned driver cannot also be a passenger.", ErrorCodes.PassengerIsDriver);

        if (Status == BookingStatusEnum.Created)
        {
            Status = BookingStatusEnum.Pending;
        }

        _passengers.Add(BookingPassenger.CreateRegular(BookingId, personId, pickupLocation));

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void AddGuestPassenger(string guestName, string guestPhone, string pickupLocation)
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        if (_passengers.Any(x => x.GuestName == guestName && x.GuestPhone == guestPhone))
        {
            throw new BusinessException("Guest passenger already exists.", ErrorCodes.PassengerExists);
        }

        if (Status == BookingStatusEnum.Created)
        {
            Status = BookingStatusEnum.Pending;
        }

        _passengers.Add(BookingPassenger.CreateGuest(BookingId, guestName, guestPhone, pickupLocation));

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void RemovePassenger(Guid bookingPassengerId)
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        var bookingPassenger = _passengers.FirstOrDefault(x => x.BookingPassengerId == bookingPassengerId);

        if (bookingPassenger is null)
            throw new BusinessException("Passenger not found.", ErrorCodes.PassengerNotFound);

        _passengers.Remove(bookingPassenger);

        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangePickupLocation(Guid bookingPassengerId, string pickupLocation)
    {
        EnsureBookingNotCancelled();

        EnsureBookingNotCompleted();

        EnsureBookingNotConfirmed();

        var bookingPassenger = _passengers.FirstOrDefault(x => x.BookingPassengerId == bookingPassengerId);

        if (bookingPassenger is null)
            throw new BusinessException("Passenger not found.", ErrorCodes.PassengerNotFound);

        bookingPassenger.ChangePickupLocation(pickupLocation);

        UpdatedAt = DateTimeOffset.UtcNow;
    }

}