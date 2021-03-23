using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class AdminMappingConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property("Admins");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Login).IsRequired().HasMaxLength(24);
            builder.Property(c => c.Password).IsRequired().HasMaxLength(24);
        }
    }
}
