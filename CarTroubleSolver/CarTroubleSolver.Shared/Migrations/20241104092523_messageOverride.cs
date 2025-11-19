using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Shared.Migrations
{
    /// <inheritdoc />
    public partial class messageOverride : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CarId",
                table: "Messages",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Cars_CarId",
                table: "Messages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Cars_CarId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_CarId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "Messages");
        }
    }
}
