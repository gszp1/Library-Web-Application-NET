using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.ReservationId);

            builder.Property(r => r.ReservationId)
                .HasColumnName("reservation_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.ReservationStart)
                .HasColumnName("reservation_start")
                .HasColumnType("date")
                .IsRequired(true);

            builder.Property(r => r.ReservationEnd)
                .HasColumnName("reservation_end")
                .HasColumnType("date")
                .IsRequired(true);

            builder.Property(r => r.Status)
                .HasColumnName("status")
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .HasConversion<string>()
                .IsRequired(true)
                .HasDefaultValue(ReservationStatus.Active);

            builder.Property(r => r.ExtensionCount)
                .HasColumnName("extension_count")
                .HasColumnType("int")
                .IsRequired(true)
                .HasDefaultValue(0);
        }
    }
}
