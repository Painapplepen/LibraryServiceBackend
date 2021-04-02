using LibraryService.Data.Domain.Models;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class BookFundConfiguration : IEntityTypeConfiguration<BookFund>
    {
        public void Configure(EntityTypeBuilder<BookFund> builder)
        {
            builder.Property("BookFund");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Amount).IsRequired();
            builder.HasOne(c => c.Book).WithMany();
            builder.HasOne(c => c.Library).WithMany();
        }
    }
}