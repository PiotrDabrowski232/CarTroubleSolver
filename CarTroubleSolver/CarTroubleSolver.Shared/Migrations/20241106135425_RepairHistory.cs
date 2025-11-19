using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarTroubleSolver.Shared.Migrations
{
    /// <inheritdoc />
    public partial class RepairHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepairHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Service = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SpentHours = table.Column<float>(type: "real", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkshopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairHistory_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairHistory_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RepairHistory_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    RepairHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryItems_RepairHistory_RepairHistoryId",
                        column: x => x.RepairHistoryId,
                        principalTable: "RepairHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryItems_RepairHistoryId",
                table: "HistoryItems",
                column: "RepairHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairHistory_AccidentId",
                table: "RepairHistory",
                column: "AccidentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairHistory_CarId",
                table: "RepairHistory",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairHistory_WorkshopId",
                table: "RepairHistory",
                column: "WorkshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryItems");

            migrationBuilder.DropTable(
                name: "RepairHistory");
        }
    }
}
