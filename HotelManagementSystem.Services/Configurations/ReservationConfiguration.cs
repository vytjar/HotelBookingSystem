using HotelManagementSystem.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations", t =>
            {
                t.HasCheckConstraint("CK_Registration_CheckOutDate", "\"CheckOutDate\" > \"CheckInDate\"");
            });

            builder.HasKey(reg => reg.Id);

            builder.Property(reg => reg.GuestCount)
                .IsRequired();

            builder.Property(reg => reg.CheckInDate)
                .IsRequired();

            builder.Property(reg => reg.CheckOutDate)
                .IsRequired();

            builder.HasOne(reg => reg.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(reg => reg.RoomId);
        }
    }
}
