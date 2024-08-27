using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.OpenApi.Extensions;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId)
                .HasColumnName("user_id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.HasIndex(u => u.Email)
                .IsUnique(true);

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar")
                .HasMaxLength(40)
                .IsRequired(false);
            
            builder.Property(u => u.Surname)
                .HasColumnName("surname")
                .HasColumnType("nvarchar")
                .HasMaxLength(40)
                .IsRequired(false);

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("phone_number")
                .HasColumnType("varchar")
                .HasMaxLength(12)
                .IsRequired(false);

            builder.Property(u => u.ImageUrl)
                .HasColumnName("image_url")
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(u => u.JoinDate)
                .HasColumnName("join_date")
                .HasColumnType("date")
                .IsRequired(true);

            builder.Property(u => u.Status)
                .HasColumnName("status")
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .HasConversion<string>()
                .IsRequired(true)
                .HasDefaultValue(UserStatus.Active);

            builder.Property(u => u.Role)
                .HasColumnName("role")
                .HasColumnType("nvarchar")
                .HasMaxLength(30)
                .HasConversion<string>()
                .IsRequired(true)
                .HasDefaultValue(Role.User);
        }
    }
}
