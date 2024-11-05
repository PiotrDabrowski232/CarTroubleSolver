using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Shared.Migrations
{
    /// <inheritdoc />
    public partial class dateofvisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfVisit",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfVisit",
                table: "Messages");
        }
    }
}
