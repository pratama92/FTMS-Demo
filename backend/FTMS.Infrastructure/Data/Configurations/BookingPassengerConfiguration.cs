using FTMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class BookingPassengerConfiguration
    : IEntityTypeConfiguration<BookingPassenger>
{
    public void Configure(EntityTypeBuilder<BookingPassenger> builder)
    {
        builder.ToTable("BookingPassengers");

        builder.HasKey(bp => bp.BookingPassengerId);

        builder.Property(x => x.BookingPassengerId)
          .ValueGeneratedNever();

        builder.Property(bp => bp.PassengerType)
            .HasConversion<string>()
            .IsRequired();


        builder.Property(bp => bp.PersonId)
            .IsRequired(false);


        builder.Property(bp => bp.GuestName)
            .HasMaxLength(100)
            .IsRequired(false);


        builder.Property(bp => bp.GuestPhone)
            .HasMaxLength(30)
            .IsRequired(false);


        builder.Property(bp => bp.PickupLocation)
            .IsRequired()
            .HasMaxLength(200);


        builder.HasOne<Person>()
            .WithMany()
            .HasForeignKey(bp => bp.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PersonId);

        builder.HasIndex(bp => new
        {
            bp.BookingId,
            bp.PersonId
        })
        .HasFilter("[PersonId] IS NOT NULL")
        .IsUnique();


        builder.HasIndex(bp => new
        {
            bp.BookingId,
            bp.GuestName,
            bp.GuestPhone
        })
        .HasFilter("[PersonId] IS NULL")
        .IsUnique();
    }
}