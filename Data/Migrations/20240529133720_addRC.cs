using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tazkara.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StadiumId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNum = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EastPremiumStandsId = table.Column<int>(type: "int", nullable: false),
                    NorthPremiumStandsId = table.Column<int>(type: "int", nullable: false),
                    EastStandsId = table.Column<int>(type: "int", nullable: false),
                    CourtSidesRow3Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_CourtSidesRow3_CourtSidesRow3Id",
                        column: x => x.CourtSidesRow3Id,
                        principalTable: "CourtSidesRow3",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_EastPremiumStands_EastPremiumStandsId",
                        column: x => x.EastPremiumStandsId,
                        principalTable: "EastPremiumStands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_EastStands_EastStandsId",
                        column: x => x.EastStandsId,
                        principalTable: "EastStands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_Matchs_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_NorthPremiumStands_NorthPremiumStandsId",
                        column: x => x.NorthPremiumStandsId,
                        principalTable: "NorthPremiumStands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_ApplicationUserId",
                table: "ReservationCarts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_CourtSidesRow3Id",
                table: "ReservationCarts",
                column: "CourtSidesRow3Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_EastPremiumStandsId",
                table: "ReservationCarts",
                column: "EastPremiumStandsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_EastStandsId",
                table: "ReservationCarts",
                column: "EastStandsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_MatchId",
                table: "ReservationCarts",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_NorthPremiumStandsId",
                table: "ReservationCarts",
                column: "NorthPremiumStandsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_StadiumId",
                table: "ReservationCarts",
                column: "StadiumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationCarts");
        }
    }
}
