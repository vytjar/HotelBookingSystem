using HotelManagementSystem.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagementSystem.Services.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");

            builder.HasKey(s => s.Id);

            builder
                .Property(s => s.ExpiresAt)
                .IsRequired();

            builder
                .Property(s => s.Id)
                .IsRequired();

            builder
                .Property(s => s.InitiatedAt)
                .IsRequired();

            builder
                .Property(s => s.RefreshToken)
                .IsRequired();

            builder
                .Property(s => s.Revoked)
                .HasDefaultValue(false);

            builder
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
