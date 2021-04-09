﻿using LibraryService.Data.Domain.Models;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).IsRequired().HasMaxLength(24);
            builder.Property(c => c.AmountPage).IsRequired();
            builder.Property(c => c.Year).IsRequired();
        }
    }
}