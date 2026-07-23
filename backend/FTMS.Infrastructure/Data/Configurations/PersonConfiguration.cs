using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTMS.Infrastructure.Data.Configurations;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        // Table
        builder.ToTable("Persons");


        // Primary Key
        builder.HasKey(p => p.PersonId);


        // Properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Phone)
            .HasMaxLength(50);

        builder.Property(p => p.OrganizationId)
            .IsRequired();


        // Enum
        builder.Property(p => p.Roles)
            .HasConversion<int>()
            .IsRequired();


        // Audit
        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();


        // Soft Delete
        builder.Property(p => p.IsDeleted)
            .IsRequired();

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);


        // Indexes
        builder.HasIndex(p => p.Email)
            .IsUnique();


        // Relationships
        builder.HasOne(p => p.Organization)
            .WithMany(o => o.Persons)
            .HasForeignKey(p => p.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);


        // Seed
        builder.HasData(
            new
            {
                PersonId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                OrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),

                Name = "System Administrator",
                Email = "admin@ftms.com",
                Phone = "0000000000",

                Roles = PersonRoleEnum.Passenger,

                CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),

                IsDeleted = false,
                DeletedAt = (DateTimeOffset?)null
            }
        );
    }
}