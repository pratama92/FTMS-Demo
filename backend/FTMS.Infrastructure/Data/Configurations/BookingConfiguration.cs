using FTMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTMS.Infrastructure.Data.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        // Table
        builder.ToTable("Bookings");

        // Primary Key
        builder.HasKey(b => b.BookingId);

        // Properties
        builder.Property(b => b.BookingNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.DestinationLocation)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.EstimatedDepartureTime)
            .IsRequired();

        builder.Property(b => b.EstimatedArrivalTime)
       .IsRequired();

        builder.Property(b => b.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        builder.Property(b => b.UpdatedAt)
            .IsRequired();


        // Indexes
        builder.HasIndex(b => b.BookingNumber)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.VehicleId,
            x.EstimatedDepartureTime,
            x.EstimatedArrivalTime,
            x.Status,
        });

        builder.HasIndex(x => new
        {
            x.DriverPersonId,
            x.EstimatedDepartureTime,
            x.EstimatedArrivalTime,
            x.Status,
        });

        // Organization Relationship
        builder.HasOne(b => b.Organization)
            .WithMany(o => o.Bookings)
            .HasForeignKey(b => b.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);


        // Booking Passenger Relationship
        builder.HasMany(b => b.Passengers)
            .WithOne()
            .HasForeignKey(bp => bp.BookingId)
            .OnDelete(DeleteBehavior.Cascade);


        // Use private field for aggregate collection
        builder.Navigation(b => b.Passengers)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}