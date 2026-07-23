using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class BookingPassengerTests
{
    [Fact]
    public void CreateRegular_ShouldCreateRegularPassenger()
    {
        // Arrange
        var bookingId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        var passenger = BookingPassenger.CreateRegular(
            bookingId,
            personId,
            "Office");

        // Assert
        passenger.BookingId.Should().Be(bookingId);
        passenger.PersonId.Should().Be(personId);
        passenger.PassengerType.Should().Be(PassengerTypeEnum.Regular);
        passenger.PickupLocation.Should().Be("Office");
    }


    [Fact]
    public void CreateGuest_ShouldCreateGuestPassenger()
    {
        // Arrange
        var bookingId = Guid.NewGuid();

        // Act
        var passenger = BookingPassenger.CreateGuest(
            bookingId,
            "John",
            "08123456789",
            "Home");

        // Assert
        passenger.BookingId.Should().Be(bookingId);
        passenger.GuestName.Should().Be("John");
        passenger.GuestPhone.Should().Be("08123456789");
        passenger.PassengerType.Should().Be(PassengerTypeEnum.Guest);
        passenger.PickupLocation.Should().Be("Home");
    }


    [Fact]
    public void ChangePickupLocation_ShouldUpdateLocation()
    {
        // Arrange
        var passenger = BookingPassenger.CreateGuest(
            Guid.NewGuid(),
            "John",
            "08123456789",
            "Home");


        // Act
        passenger.ChangePickupLocation("Office");


        // Assert
        passenger.PickupLocation.Should()
            .Be("Office");
    }


    [Fact]
    public void CreateRegular_WithEmptyPerson_ShouldThrowException()
    {
        // Act
        Action act = () =>
            BookingPassenger.CreateRegular(
                Guid.NewGuid(),
                Guid.Empty,
                "Office");


        // Assert
        act.Should()
            .Throw<BusinessException>();
    }


    [Fact]
    public void CreateGuest_WithEmptyName_ShouldThrowException()
    {
        // Act
        Action act = () =>
            BookingPassenger.CreateGuest(
                Guid.NewGuid(),
                "",
                "08123456789",
                "Home");


        // Assert
        act.Should()
            .Throw<BusinessException>();
    }


    [Fact]
    public void ChangePickupLocation_WithEmptyLocation_ShouldThrowException()
    {
        // Arrange
        var passenger = BookingPassenger.CreateGuest(
            Guid.NewGuid(),
            "John",
            "08123456789",
            "Home");


        // Act
        Action act = () =>
            passenger.ChangePickupLocation("");


        // Assert
        act.Should()
            .Throw<BusinessException>();
    }
}