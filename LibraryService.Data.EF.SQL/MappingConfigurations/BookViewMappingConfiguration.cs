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
            builder.Property(v => v.Title).HasColumnName("Title");
            builder.Property(v => v.AmountPage).HasColumnName("AmountPage");
            builder.Property(v => v.Year).HasColumnName("Year");
            builder.Property(v => v.AuthorName).HasColumnName("Name");
            builder.Property(v => v.AuthorPatronymic).HasColumnName("Patronymic");
            builder.Property(v => v.AuthorSurname).HasColumnName("Surname");
            builder.Property(v => v.Publisher).HasColumnName("Publisher");
            builder.Property(v => v.Genre).HasColumnName("Genre");
        }
    }
}