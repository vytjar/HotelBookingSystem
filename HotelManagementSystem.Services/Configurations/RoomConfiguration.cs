using HotelManagementSystem.Interfaces.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(r => r.Capacity)
                .IsRequired();

            builder.HasMany(r => r.Registrations)
                .WithOne(reg => reg.Room)
                .HasForeignKey(reg => reg.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
