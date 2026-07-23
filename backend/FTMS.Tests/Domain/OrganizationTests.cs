using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class OrganizationTests
{
    private Organization CreateOrganization()
    {
        return Organization.Create(
            "FTMS Company",
            "Transportation provider");
    }


    [Fact]
    public void Create_ShouldSetDefaultStatusActive()
    {
        // Arrange
        var organization = CreateOrganization();


        // Assert
        organization.Status.Should()
            .Be(OrganizationStatusEnum.Active);

        organization.Name.Should()
            .Be("FTMS Company");
    }


    [Fact]
    public void Create_ShouldTrimName()
    {
        // Arrange
        var organization = Organization.Create(
            "  FTMS Company  ",
            null);


        // Assert
        organization.Name.Should()
            .Be("FTMS Company");
    }


    [Fact]
    public void Rename_ShouldUpdateName()
    {
        // Arrange
        var organization = CreateOrganization();


        // Act
        organization.Rename("New Name");


        // Assert
        organization.Name.Should()
            .Be("New Name");
    }


    [Fact]
    public void Deactivate_ShouldChangeStatusInactive()
    {
        // Arrange
        var organization = CreateOrganization();


        // Act
        organization.Deactivate();


        // Assert
        organization.Status.Should()
            .Be(OrganizationStatusEnum.Inactive);
    }


    [Fact]
    public void Activate_ShouldChangeStatusActive()
    {
        // Arrange
        var organization = CreateOrganization();

        organization.Deactivate();


        // Act
        organization.Activate();


        // Assert
        organization.Status.Should()
            .Be(OrganizationStatusEnum.Active);
    }
}