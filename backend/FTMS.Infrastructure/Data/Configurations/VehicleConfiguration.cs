using FTMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTMS.Infrastructure.Data.Configurations;

public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        // Table
        builder.ToTable("Vehicles");

        // Primary Key
        builder.HasKey(v => v.VehicleId);

        // Properties
        builder.Property(v => v.VehicleCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(v => v.ChassisNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.EngineNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Color)
            .HasMaxLength(50);

        builder.Property(v => v.SeatCapacity)
            .IsRequired();

        builder.Property(v => v.CargoCapacity)
            .HasPrecision(10, 2);

        builder.Property(v => v.VehicleType)
            .HasConversion<string>();

        builder.Property(v => v.FuelType)
            .HasConversion<string>();

        builder.Property(v => v.Drivetrain)
            .HasConversion<string>();

        builder.Property(v => v.Transmission)
            .HasConversion<string>();

        builder.Property(v => v.Status)
            .HasConversion<string>();

        // Audit and Soft Delete
        builder.Property(v => v.CreatedAt)
            .IsRequired();

        builder.Property(v => v.UpdatedAt)
            .IsRequired();

        builder.Property(v => v.IsDeleted)
            .IsRequired();

        builder.Property(v => v.DeletedAt);

        // Indexes
        builder.HasIndex(v => v.LicensePlate)
            .IsUnique();

        builder.HasIndex(v => v.ChassisNumber)
            .IsUnique();

        builder.HasIndex(v => v.EngineNumber)
            .IsUnique();

        // Relationships
        builder.HasOne(v => v.Organization)
            .WithMany(o => o.Vehicles)
            .HasForeignKey(v => v.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}