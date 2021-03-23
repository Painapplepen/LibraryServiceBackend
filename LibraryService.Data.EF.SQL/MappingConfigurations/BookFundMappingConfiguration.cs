using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class BookFundConfiguration : IEntityTypeConfiguration<BookFund>
    {
        public void Configure(EntityTypeBuilder<BookFund> builder)
        {
            builder.Property("Authors");
            builder.HasKey(c => c.Id);

           
        }
    }
}