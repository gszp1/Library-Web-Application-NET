using Library_Web_Application_NET.Server.src.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library_Web_Application_NET.Server.src.data.config
{
    public class AuthorResourceConfiguration : IEntityTypeConfiguration<AuthorResource>
    {
        public void Configure(EntityTypeBuilder<AuthorResource> builder)
        {
            builder.ToTable("authors_resources");

            builder.Property(ar => ar.AuthorId)
                .HasColumnName("FK_author")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(ar => ar.ResourceId)
                .HasColumnName("FK_resource")
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
