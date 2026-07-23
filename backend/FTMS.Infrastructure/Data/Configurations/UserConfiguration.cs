using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FTMS.Infrastructure.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table
        builder.ToTable("Users");


        // Primary Key
        builder.HasKey(u => u.UserId);


        // Properties
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .HasConversion<int>()
            .IsRequired();


        // Audit
        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired();


        // Soft Delete
        builder.Property(u => u.IsDeleted)
            .IsRequired();

        builder.Property(u => u.DeletedAt)
            .IsRequired(false);


        // Indexes
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.HasIndex(u => u.PersonId)
            .IsUnique();


        // Relationships

        // User -> Person
        builder.HasOne(u => u.Person)
            .WithOne()
            .HasForeignKey<User>(u => u.PersonId)
            .OnDelete(DeleteBehavior.Restrict);


        // User -> Organization
        builder.HasOne(u => u.Organization)
            .WithMany(o => o.Users)
            .HasForeignKey(u => u.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);


        // Seed
        builder.HasData(
            new
            {
                UserId = Guid.Parse("33333333-3333-3333-3333-333333333333"),

                OrganizationId = Guid.Parse("11111111-1111-1111-1111-111111111111"),

                PersonId = Guid.Parse("22222222-2222-2222-2222-222222222222"),

                Username = "admin",

                PasswordHash = "$2a$12$I22tx/iIpBcw4WACtHrwAOq3Ljk/ZOI94VO5PoFW9tPIki1Q.KQvK",

                Role = UserRoleEnum.Dispatcher,

                CreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                UpdatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero),

                IsDeleted = false,

                DeletedAt = (DateTimeOffset?)null
            }
        );
    }
}