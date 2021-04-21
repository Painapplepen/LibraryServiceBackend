﻿using LibraryService.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.Property("Library");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(24);
            builder.Property(c => c.Telephone).IsRequired().HasMaxLength(24);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(24);
            builder.HasMany(c => c.BookFunds).WithOne(c => c.Library).OnDelete(DeleteBehavior.Cascade);
        }
    }
}