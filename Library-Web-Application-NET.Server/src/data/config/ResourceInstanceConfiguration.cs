using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.OpenApi.Extensions;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class ResourceInstanceConfiguration : IEntityTypeConfiguration<ResourceInstance>
    {
        public void Configure(EntityTypeBuilder<ResourceInstance> builder)
        {
            builder.ToTable("resource_instances");

            builder.HasKey(i => i.InstanceId);

            builder.Property(i => i.InstanceId)
                .HasColumnName("instance_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.HasIndex(i => i.Reserved)
                .IsUnique(true);

            builder.Property(i => i.Reserved)
                .HasColumnName("reserved")
                .HasColumnType("bit")
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(i => i.Status)
                .HasColumnName("status")
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .HasConversion<string>()
                .IsRequired(true)
                .HasDefaultValue(InstanceStatus.Active);

            builder.Property(i => i.ResourceId)
                .HasColumnName("FK_resource")
                .HasColumnType("int")
                .IsRequired(true);
        }
    }
}
