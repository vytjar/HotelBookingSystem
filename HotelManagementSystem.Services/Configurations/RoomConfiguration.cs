using HotelManagementSystem.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
                .ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder
                .Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(r => r.Capacity)
                .IsRequired();

            builder
                .HasMany(r => r.Reservations)
                .WithOne(r => r.Room)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
