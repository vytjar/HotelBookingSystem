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

            builder.HasKey(r => r.Id);

            builder
                .Property(r => r.GuestCount)
                .IsRequired();

            builder
                .Property(r => r.CheckInDate)
                .IsRequired();  

            builder.Property(r => r.CheckOutDate)
                .IsRequired();

            builder
                .HasOne(r => r.Room)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.RoomId);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);
        }
    }
}
