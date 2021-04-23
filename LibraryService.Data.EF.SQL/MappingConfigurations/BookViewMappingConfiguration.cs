using LibraryService.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class BookViewMappingConfiguration : IEntityTypeConfiguration<BookView>
    {
        public void Configure(EntityTypeBuilder<BookView> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToView("View_BookView");
        }
    }
}