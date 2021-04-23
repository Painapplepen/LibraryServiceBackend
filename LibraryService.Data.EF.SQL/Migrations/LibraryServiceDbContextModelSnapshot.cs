﻿// <auto-generated />
using LibraryService.Data.EF.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryService.Data.EF.SQL.Migrations
{
    [DbContext(typeof(LibraryServiceDbContext))]
    partial class LibraryServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Admin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AmountPage")
                        .HasColumnType("bigint");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<long>("GenreId")
                        .HasColumnType("bigint");

                    b.Property<long>("PublisherId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.BookFund", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.Property<long>("LibraryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryId");

                    b.ToTable("BookFund");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.BookFundView", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorPatronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BookAmountPage")
                        .HasColumnType("bigint");

                    b.Property<string>("BookTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("BookYear")
                        .HasColumnType("bigint");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LibraryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LibraryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LibraryTelephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToView("View_BookFundView");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.BookView", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AmountPage")
                        .HasColumnType("bigint");

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorPatronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuthorSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Year")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToView("View_BookView");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Library", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Publisher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Book", b =>
                {
                    b.HasOne("LibraryService.Data.Domain.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryService.Data.Domain.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryService.Data.Domain.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Genre");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.BookFund", b =>
                {
                    b.HasOne("LibraryService.Data.Domain.Models.Book", "Book")
                        .WithMany("BookFunds")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryService.Data.Domain.Models.Library", "Library")
                        .WithMany("BookFunds")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Book", b =>
                {
                    b.Navigation("BookFunds");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Library", b =>
                {
                    b.Navigation("BookFunds");
                });

            modelBuilder.Entity("LibraryService.Data.Domain.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
