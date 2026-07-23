using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class UserTests
{
    private User CreateUser()
    {
        return User.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "AdminUser",
            "hashed-password",
            UserRoleEnum.Admin);
    }


    [Fact]
    public void Create_ShouldSetUserInformation()
    {
        // Arrange
        var user = CreateUser();

        // Assert
        user.Username.Should()
            .Be("adminuser");

        user.Role.Should()
            .Be(UserRoleEnum.Admin);

        user.IsDeleted.Should()
            .BeFalse();
    }


    [Fact]
    public void Create_WithEmptyUsername_ShouldThrowException()
    {
        // Act
        Action action = () =>
            User.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "",
                "hash",
                UserRoleEnum.Admin);


        // Assert
        action.Should()
            .Throw<BusinessException>();
    }


    [Fact]
    public void ChangePassword_ShouldUpdatePassword()
    {
        // Arrange
        var user = CreateUser();

        // Act
        user.ChangePassword("new-password");


        // Assert
        user.PasswordHash.Should()
            .Be("new-password");
    }


    [Fact]
    public void ChangePassword_WithEmptyPassword_ShouldThrowException()
    {
        // Arrange
        var user = CreateUser();


        // Act
        Action action = () =>
            user.ChangePassword("");


        // Assert
        action.Should()
            .Throw<BusinessException>();
    }


    [Fact]
    public void Delete_ShouldMarkUserDeleted()
    {
        // Arrange
        var user = CreateUser();

        // Act
        user.Delete();

        // Assert
        user.IsDeleted.Should()
            .BeTrue();

        user.DeletedAt.Should()
            .NotBeNull();
    }


    [Fact]
    public void Delete_Twice_ShouldThrowException()
    {
        // Arrange
        var user = CreateUser();

        user.Delete();


        // Act
        Action action = () =>
            user.Delete();


        // Assert
        action.Should()
            .Throw<BusinessException>();
    }
}