using LibraryService.Data.Domain.Models;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class AuthorMappingConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(24);
            builder.Property(c => c.Surname).IsRequired().HasMaxLength(24);
            builder.Property(c => c.Patronymic).IsRequired().HasMaxLength(24);
        }
    }
}
