using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryService.Data.EF.SQL.Migrations
{
    public partial class AddViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string script = @"CREATE VIEW View_BookFundView AS SELECT 
                                    [LibraryService].[dbo].[BookFunds].[Id], [LibraryService].[dbo].[Books].[Title],[LibraryService].[dbo].[Books].[AmountPage],
                                    [LibraryService].[dbo].[Books].[Year], [LibraryService].[dbo].[Authors].[Surname], [LibraryService].[dbo].[Authors].[Name],
                                    [LibraryService].[dbo].[Authors].[Patronymic],[LibraryService].[dbo].[Genres].[Name] as Genre, 
                                    [LibraryService].[dbo].[Publishers].[Name] as Publisher,[LibraryService].[dbo].[Libraries].[Name] as Library,
                                    [LibraryService].[dbo].[Libraries].[Address],[LibraryService].[dbo].[Libraries].[Telephone],[LibraryService].[dbo].[BookFunds].[Amount]
                                    FROM [LibraryService].[dbo].[BookFunds] INNER JOIN [LibraryService].[dbo].[Books] 
                                    ON [LibraryService].[dbo].[BookFunds].[BookId] = [LibraryService].[dbo].[Books].[Id] 
                                    INNER JOIN [LibraryService].[dbo].[Libraries] ON [LibraryService].[dbo].[BookFunds].[LibraryId] = [LibraryService].[dbo].[Libraries].[Id]
                                    INNER JOIN [LibraryService].[dbo].[Authors] ON [LibraryService].[dbo].[Books].[AuthorId] = [LibraryService].[dbo].[Authors].[Id]
                                    INNER JOIN [LibraryService].[dbo].[Publishers] ON [LibraryService].[dbo].[Books].[PublisherId] = [LibraryService].[dbo].[Publishers].[Id]
                                    INNER JOIN [LibraryService].[dbo].[Genres] ON [LibraryService].[dbo].[Books].[GenreId] = [LibraryService].[dbo].[Genres].[Id]";

            migrationBuilder.Sql($"EXECUTE('{script}')");

            const string secondScript = @"CREATE VIEW View_BookView AS SELECT 
                                    [LibraryService].[dbo].[Books].[Id], [LibraryService].[dbo].[Books].[Title],[LibraryService].[dbo].[Books].[AmountPage],
                                    [LibraryService].[dbo].[Books].[Year], [LibraryService].[dbo].[Authors].[Surname], [LibraryService].[dbo].[Authors].[Name],
                                    [LibraryService].[dbo].[Authors].[Patronymic],[LibraryService].[dbo].[Genres].[Name] as Genre, 
                                    [LibraryService].[dbo].[Publishers].[Name] as Publisher
                                    FROM [LibraryService].[dbo].[Books]  
                                    INNER JOIN [LibraryService].[dbo].[Authors] ON [LibraryService].[dbo].[Books].[AuthorId] = [LibraryService].[dbo].[Authors].[Id]
                                    INNER JOIN [LibraryService].[dbo].[Publishers] ON [LibraryService].[dbo].[Books].[PublisherId] = [LibraryService].[dbo].[Publishers].[Id]
                                    INNER JOIN [LibraryService].[dbo].[Genres] ON [LibraryService].[dbo].[Books].[GenreId] = [LibraryService].[dbo].[Genres].[Id]";

            migrationBuilder.Sql($"EXECUTE('{secondScript}')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
