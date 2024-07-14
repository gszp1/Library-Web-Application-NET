using Library_Web_Application_NET.Server.src.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class PublisherConifguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("publishers");

            builder.HasKey(p => p.PublisherId);

            builder.Property(p => p.PublisherId)
                .HasColumnName("publisher_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.HasIndex(p => p.Name)
                .IsUnique(true);

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.HasIndex(p => p.Address)
                .IsUnique(true);

            builder.Property(p => p.Address)
                .HasColumnName("address")
                .HasColumnType("nvarchar")
                .HasMaxLength(150)
                .IsRequired(true);
        }
    }
}
