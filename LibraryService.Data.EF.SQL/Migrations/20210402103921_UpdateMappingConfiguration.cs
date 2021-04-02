using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryService.Data.EF.SQL.Migrations
{
    public partial class UpdateMappingConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFunds_Publishers_PublisherId",
                table: "BookFunds");

            migrationBuilder.DropIndex(
                name: "IX_BookFunds_PublisherId",
                table: "BookFunds");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "BookFunds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PublisherId",
                table: "BookFunds",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookFunds_PublisherId",
                table: "BookFunds",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFunds_Publishers_PublisherId",
                table: "BookFunds",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
