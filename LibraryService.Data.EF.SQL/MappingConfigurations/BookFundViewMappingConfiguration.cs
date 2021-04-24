using LibraryService.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Data.EF.SQL.MappingConfigurations
{
    public class BookFundViewMappingConfiguration : IEntityTypeConfiguration<BookFundView>
    {
        public void Configure(EntityTypeBuilder<BookFundView> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToView("View_BookFundView");
            builder.Property(v => v.ISBN).HasColumnName("ISBN");
            builder.Property(v => v.BookTitle).HasColumnName("Title");
            builder.Property(v => v.BookAmountPage).HasColumnName("AmountPage");
            builder.Property(v => v.BookYear).HasColumnName("Year");
            builder.Property(v => v.AuthorName).HasColumnName("Name");
            builder.Property(v => v.AuthorPatronymic).HasColumnName("Patronymic");
            builder.Property(v => v.AuthorSurname).HasColumnName("Surname");
            builder.Property(v => v.Publisher).HasColumnName("Publisher");
            builder.Property(v => v.Genre).HasColumnName("Genre");
            builder.Property(v => v.LibraryName).HasColumnName("Library");
            builder.Property(v => v.LibraryTelephone).HasColumnName("Telephone");
            builder.Property(v => v.LibraryAddress).HasColumnName("Address");
            builder.Property(v => v.Amount).HasColumnName("Amount");
        }
    }
}