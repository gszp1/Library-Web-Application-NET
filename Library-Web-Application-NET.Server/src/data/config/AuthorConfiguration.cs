using Library_Web_Application_NET.Server.src.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder) 
        {
            builder.ToTable("authors");
            
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.AuthorId)
                .HasColumnName("author_id")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("nvarchar")
                .HasMaxLength(40)
                .IsRequired(true);

            builder.Property(a => a.LastName)
                .HasColumnName("last_name")
                .HasColumnType("nvarchar")
                .HasMaxLength(40)
                .IsRequired(true);

            builder.HasIndex(a => a.Email)
                .IsUnique(true);

            builder.Property(a => a.Email)
                .HasColumnName("email")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired(true);
        }
    }
}
