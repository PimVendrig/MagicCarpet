using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MagicCarpetWebApp.Migrations
{
    public partial class ConcertLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInfo",
                table: "ConcertInfo");

            migrationBuilder.RenameTable(
                name: "ConcertInfo",
                newName: "ConcertInfoes");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "ConcertInfoes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInfoes",
                table: "ConcertInfoes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConcertLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Station = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConcertInfoes_LocationId",
                table: "ConcertInfoes",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes",
                column: "LocationId",
                principalTable: "ConcertLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertInfoes_ConcertLocations_LocationId",
                table: "ConcertInfoes");

            migrationBuilder.DropTable(
                name: "ConcertLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConcertInfoes",
                table: "ConcertInfoes");

            migrationBuilder.DropIndex(
                name: "IX_ConcertInfoes_LocationId",
                table: "ConcertInfoes");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ConcertInfoes");

            migrationBuilder.RenameTable(
                name: "ConcertInfoes",
                newName: "ConcertInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConcertInfo",
                table: "ConcertInfo",
                column: "Id");
        }
    }
}
