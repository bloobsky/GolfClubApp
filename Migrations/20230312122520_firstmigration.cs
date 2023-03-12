using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolfClubApp.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Golfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    Handicap = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Golfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeeBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookingTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BookedTeeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeeBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeeBookings_Tees_BookedTeeId",
                        column: x => x.BookedTeeId,
                        principalTable: "Tees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GolferTeeBooking",
                columns: table => new
                {
                    GolfersId = table.Column<int>(type: "INTEGER", nullable: false),
                    TeeBookingsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GolferTeeBooking", x => new { x.GolfersId, x.TeeBookingsId });
                    table.ForeignKey(
                        name: "FK_GolferTeeBooking_Golfers_GolfersId",
                        column: x => x.GolfersId,
                        principalTable: "Golfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GolferTeeBooking_TeeBookings_TeeBookingsId",
                        column: x => x.TeeBookingsId,
                        principalTable: "TeeBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GolferTeeBooking_TeeBookingsId",
                table: "GolferTeeBooking",
                column: "TeeBookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeeBookings_BookedTeeId",
                table: "TeeBookings",
                column: "BookedTeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GolferTeeBooking");

            migrationBuilder.DropTable(
                name: "Golfers");

            migrationBuilder.DropTable(
                name: "TeeBookings");

            migrationBuilder.DropTable(
                name: "Tees");
        }
    }
}
