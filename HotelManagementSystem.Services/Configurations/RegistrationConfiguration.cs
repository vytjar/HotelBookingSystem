using HotelManagementSystem.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Services.Configurations
{
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.ToTable("Registrations", t =>
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
                .WithMany(r => r.Registrations)
                .HasForeignKey(reg => reg.RoomId);
        }
    }
}
