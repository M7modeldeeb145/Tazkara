using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tazkara.Data.Migrations
{
    /// <inheritdoc />
    public partial class editmatchmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamA",
                table: "Matchs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamB",
                table: "Matchs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamA",
                table: "Matchs");

            migrationBuilder.DropColumn(
                name: "TeamB",
                table: "Matchs");
        }
    }
}
