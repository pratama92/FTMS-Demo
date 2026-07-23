using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities;

public sealed class BookingPassenger
{
    public Guid BookingPassengerId { get; private set; }

    public Guid BookingId { get; private set; }

    public Guid? PersonId { get; private set; }

    public string? GuestName { get; private set; } = string.Empty;

    public string? GuestPhone { get; private set; } = string.Empty;

    public PassengerTypeEnum PassengerType { get; private set; }

    public string PickupLocation { get; private set; } = string.Empty;


    private BookingPassenger() { }


    private BookingPassenger(
        Guid bookingId,
        Guid? personId,
        string? guestName,
        string? guestPhone,
        PassengerTypeEnum passengerType,
        string pickupLocation)
    {
        Validate(
            bookingId,
            personId,
            guestName,
            passengerType,
            pickupLocation);


        BookingPassengerId = Guid.NewGuid();
        BookingId = bookingId;
        PersonId = personId;
        GuestName = guestName?.Trim();
        GuestPhone = guestPhone?.Trim();
        PassengerType = passengerType;
        PickupLocation = pickupLocation.Trim();
    }


    public static BookingPassenger CreateRegular(
        Guid bookingId,
        Guid personId,
        string pickupLocation)
    {
        return new BookingPassenger(
            bookingId,
            personId,
            null,
            null,
            PassengerTypeEnum.Regular,
            pickupLocation);
    }


    public static BookingPassenger CreateGuest(
        Guid bookingId,
        string guestName,
        string? guestPhone,
        string pickupLocation)
    {
        return new BookingPassenger(
            bookingId,
            null,
            guestName,
            guestPhone,
            PassengerTypeEnum.Guest,
            pickupLocation);
    }

    public void ChangePickupLocation(string pickupLocation)
    {
        if (string.IsNullOrWhiteSpace(pickupLocation))
            throw new BusinessException("Pickup location is required.", ErrorCodes.PassengerPickupRequired);

        PickupLocation = pickupLocation.Trim();
    }

    private static void Validate(
        Guid bookingId,
        Guid? personId,
        string? guestName,
        PassengerTypeEnum passengerType,
        string pickupLocation)
    {
        if (bookingId == Guid.Empty)
            throw new BusinessException("Booking is required.", "BOOKING_REQUIRED");


        if (string.IsNullOrWhiteSpace(pickupLocation))
            throw new BusinessException("Pickup location is required.", "PICKUPLOCATION_REQUIRED");


        switch (passengerType)
        {
            case PassengerTypeEnum.Regular:

                if (personId == null || personId == Guid.Empty)
                    throw new BusinessException("Regular passenger requires person.", "PASSENGER_REQUIRE_PERSON");

                break;


            case PassengerTypeEnum.Guest:

                if (string.IsNullOrWhiteSpace(guestName))
                    throw new BusinessException("Guest passenger requires name.", "PASSENGER_REQUIRE_NAME");

                break;


            default:
                throw new BusinessException("Invalid passenger type.", "PASSENGER_INVALID");
        }
    }
}