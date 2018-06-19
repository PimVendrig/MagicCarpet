using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MagicCarpetWebApp.Migrations
{
    public partial class ConcertLocations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "ConcertInfoes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes",
                column: "LocationId",
                principalTable: "ConcertLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "ConcertInfoes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes",
                column: "LocationId",
                principalTable: "ConcertLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
