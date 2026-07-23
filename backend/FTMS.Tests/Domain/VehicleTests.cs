using FluentAssertions;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Tests.Domain;

public class VehicleTests
{
    private Vehicle CreateVehicle()
    {
        return Vehicle.Create(
            "BUS-001",
            "B1234ABC",
            "CHASSIS-001",
            "ENGINE-001",
            "Toyota",
            "Hiace",
            2024,
            "White",
            15,
            500,
            VehicleTypeEnum.MPV,
            TransmissionTypeEnum.Automatic,
            DrivetrainEnum.RWD,
            FuelTypeEnum.Diesel,
            Guid.NewGuid());
    }


    [Fact]
    public void Create_ShouldSetDefaultStatusAvailable()
    {
        // Arrange
        var vehicle = CreateVehicle();


        // Assert
        vehicle.Status.Should()
            .Be(VehicleStatusEnum.Available);

        vehicle.IsDeleted.Should()
            .BeFalse();
    }


    [Fact]
    public void Create_ShouldNormalizeCodeAndLicensePlate()
    {
        // Arrange
        var vehicle = Vehicle.Create(
            " bus-001 ",
            " b1234abc ",
            "CHASSIS-001",
            "ENGINE-001",
            "Toyota",
            "Hiace",
            2024,
            "White",
            15,
            500,
            VehicleTypeEnum.MPV,
            TransmissionTypeEnum.Automatic,
            DrivetrainEnum.RWD,
            FuelTypeEnum.Diesel,
            Guid.NewGuid());


        // Assert
        vehicle.VehicleCode.Should()
            .Be("BUS-001");

        vehicle.LicensePlate.Should()
            .Be("B1234ABC");
    }


    [Fact]
    public void SetMaintenance_ShouldChangeStatus()
    {
        // Arrange
        var vehicle = CreateVehicle();


        // Act
        vehicle.SetMaintenance();


        // Assert
        vehicle.Status.Should()
            .Be(VehicleStatusEnum.Maintenance);
    }


    [Fact]
    public void Delete_ShouldMarkVehicleDeleted()
    {
        // Arrange
        var vehicle = CreateVehicle();


        // Act
        vehicle.Delete();


        // Assert
        vehicle.IsDeleted.Should()
            .BeTrue();

        vehicle.DeletedAt.Should()
            .NotBeNull();
    }
}