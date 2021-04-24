using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryService.Data.EF.SQL.Migrations
{
    public partial class addISBNToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Books",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

        }
    }
}
