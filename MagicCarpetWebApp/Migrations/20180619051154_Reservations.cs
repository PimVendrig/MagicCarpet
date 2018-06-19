using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MagicCarpetWebApp.Migrations
{
    public partial class Reservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Agrees = table.Column<bool>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    ConcertId = table.Column<Guid>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    PaymentDetails = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_ConcertInfoes_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "ConcertInfoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ConcertId",
                table: "Reservations",
                column: "ConcertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
