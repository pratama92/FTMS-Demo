using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class PersonTests
{
    private Person CreatePerson()
    {
        return Person.Create(
            "John Doe",
            "john@test.com",
            "08123456789",
            Guid.NewGuid());
    }


    [Fact]
    public void Create_ShouldSetDefaultPassengerRole()
    {
        // Arrange
        var person = CreatePerson();


        // Assert
        person.Roles.Should()
            .Be(PersonRoleEnum.Passenger);

        person.IsDeleted.Should()
            .BeFalse();
    }


    [Fact]
    public void Create_ShouldNormalizeEmail()
    {
        // Arrange
        var person = Person.Create(
            "John",
            " JOHN@TEST.COM ",
            "08123",
            Guid.NewGuid());


        // Assert
        person.Email.Should()
            .Be("john@test.com");
    }


    [Fact]
    public void AddDriverRole_ShouldAllowDriving()
    {
        // Arrange
        var person = CreatePerson();


        // Act
        person.AddDriverRole();


        // Assert
        person.Roles.Should()
            .HaveFlag(PersonRoleEnum.Driver);

        person.EnsureCanDrive();
    }


    [Fact]
    public void EnsureCanDrive_ShouldRejectPassengerOnly()
    {
        // Arrange
        var person = CreatePerson();


        // Act
        Action action = () =>
            person.EnsureCanDrive();


        // Assert
        action.Should()
            .Throw<BusinessException>()
            .WithMessage("Person does not have driver role.");
    }


    [Fact]
    public void Delete_ShouldMarkPersonDeleted()
    {
        // Arrange
        var person = CreatePerson();


        // Act
        person.Delete();


        // Assert
        person.IsDeleted.Should()
            .BeTrue();

        person.DeletedAt.Should()
            .NotBeNull();
    }
}