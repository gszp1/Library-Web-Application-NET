using Library_Web_Application_NET.Server.src.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("resources");

            builder.HasKey(r => r.ResourceId);

            builder.Property(r => r.ResourceId)
                .HasColumnName("resource_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();


            builder.HasIndex(r => r.Title)
                .IsUnique(true);

            builder.Property(r => r.Title)
                .HasColumnName("title")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.HasIndex(r => r.Identifier)
                .IsUnique(true);

            builder.Property(r => r.Identifier)
                .HasColumnName("identifier")
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired(true);

            builder.Property(r => r.Descripiton)
                .HasColumnName("description")
                .HasColumnType("ntext")
                .IsRequired(false);

            builder.Property(r => r.ImageUrl)
                .HasColumnName("image_url")
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsRequired(false);
        }
    }
}
