using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Shared.Migrations
{
    /// <inheritdoc />
    public partial class messageflags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Responsed",
                table: "Messages",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Responsed",
                table: "Messages");
        }
    }
}
