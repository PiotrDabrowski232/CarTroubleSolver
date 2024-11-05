using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Shared.Migrations
{
    /// <inheritdoc />
    public partial class previousmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreviousMessageId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PreviousMessageId",
                table: "Messages",
                column: "PreviousMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_PreviousMessageId",
                table: "Messages",
                column: "PreviousMessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_PreviousMessageId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_PreviousMessageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PreviousMessageId",
                table: "Messages");
        }
    }
}
