using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class BookingTests
{
    private Booking CreateBooking()
    {
        return Booking.Create(
            Guid.NewGuid(),
            "BK-001",
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Jakarta",
            DateTimeOffset.UtcNow.AddHours(1),
            DateTimeOffset.UtcNow.AddHours(3));
    }


    [Fact]
    public void Create_ShouldSetStatusToCreated()
    {
        // Arrange
        var booking = CreateBooking();

        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Created);
    }


    [Fact]
    public void AddRegularPassenger_ShouldChangeStatusToPending()
    {
        // Arrange
        var booking = CreateBooking();

        // Act
        booking.AddRegularPassenger(
            Guid.NewGuid(),
            "Office");


        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Pending);

        booking.Passengers.Should()
            .HaveCount(1);
    }


    [Fact]
    public void AssignDriver_ShouldChangeStatusToPending()
    {
        // Arrange
        var booking = CreateBooking();

        // Act
        booking.AssignDriver(Guid.NewGuid());


        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Pending);

        booking.DriverPersonId.Should()
            .NotBeNull();
    }

    [Fact]
    public void AddRegularPassenger_ShouldRejectDuplicatePassenger()
    {
        // Arrange
        var booking = CreateBooking();

        var passengerId = Guid.NewGuid();

        booking.AddRegularPassenger(
            passengerId,
            "Office");


        // Act
        Action action = () =>
            booking.AddRegularPassenger(
                passengerId,
                "Office");


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Passenger already exists.");
    }

    [Fact]
    public void AddRegularPassenger_ShouldRejectAssignedDriver()
    {
        // Arrange
        var booking = CreateBooking();

        var driverId = Guid.NewGuid();

        booking.AssignDriver(driverId);


        // Act
        Action action = () =>
            booking.AddRegularPassenger(
                driverId,
                "Office");


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Assigned driver cannot also be a passenger.");
    }

    [Fact]
    public void Confirm_ShouldChangeStatusToConfirmed()
    {
        // Arrange
        var booking = CreateBooking();

        booking.AssignDriver(Guid.NewGuid());

        booking.AddRegularPassenger(
            Guid.NewGuid(),
            "Office");


        // Act
        booking.Confirm();


        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Confirmed);
    }

    [Fact]
    public void Confirm_ShouldRejectWithoutDriver()
    {
        // Arrange
        var booking = CreateBooking();

        booking.AddRegularPassenger(
            Guid.NewGuid(),
            "Office");


        // Act
        Action action = () => booking.Confirm();


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Booking requires a driver before confirmation.");
    }

    [Fact]
    public void Confirm_ShouldRejectWithoutPassenger()
    {
        // Arrange
        var booking = CreateBooking();

        booking.AssignDriver(Guid.NewGuid());


        // Act
        Action action = () => booking.Confirm();

        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Booking requires passengers before confirmation.");
    }
    [Fact]
    public void Complete_ShouldChangeStatusToCompleted()
    {
        // Arrange
        var booking = CreateBooking();

        booking.AssignDriver(Guid.NewGuid());

        booking.AddRegularPassenger(
            Guid.NewGuid(),
            "Office");

        booking.Confirm();


        // Act
        booking.Complete();


        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Completed);
    }
    [Fact]
    public void Complete_ShouldRejectNonConfirmedBooking()
    {
        // Arrange
        var booking = CreateBooking();


        // Act
        Action action = () =>
            booking.Complete();


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Only confirmed bookings can be completed.");
    }
    [Fact]
    public void Cancel_ShouldChangeStatusToCancelled()
    {
        // Arrange
        var booking = CreateBooking();


        // Act
        booking.Cancel();


        // Assert
        booking.Status.Should()
            .Be(BookingStatusEnum.Cancelled);
    }
    [Fact]
    public void Cancel_ShouldRejectConfirmedBooking()
    {
        // Arrange
        var booking = CreateBooking();

        booking.AssignDriver(Guid.NewGuid());

        booking.AddRegularPassenger(
            Guid.NewGuid(),
            "Office");

        booking.Confirm();


        // Act
        Action action = () =>
            booking.Cancel();


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Booking is Already Confirmed.");
    }
}