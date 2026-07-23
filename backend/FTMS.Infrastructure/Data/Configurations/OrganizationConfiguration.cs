using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTMS.Infrastructure.Data.Configurations;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        // Table
        builder.ToTable("Organizations");

        // Primary Key
        builder.HasKey(o => o.OrganizationId);

        //// Properties
        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(o => o.Description)
            .HasMaxLength(500);

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.Property(o => o.UpdatedAt)
            .IsRequired();

        // Indexes
        builder.HasIndex(o => o.Name).IsUnique();

        // Relationships
        builder.HasMany(o => o.Persons)
            .WithOne(p => p.Organization)
            .HasForeignKey(p => p.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.Vehicles)
            .WithOne(v => v.Organization)
            .HasForeignKey(v => v.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.Bookings)
            .WithOne(u => u.Organization)
            .HasForeignKey(b => b.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.Users)
            .WithOne(u => u.Organization)
            .HasForeignKey(b => b.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking relationship will be added later

        builder.HasData(
            new
            {
                OrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "FTMS Development",
                Description = "Development Organization",
                Status = OrganizationStatusEnum.Active,
                CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero)
            });

    }


}