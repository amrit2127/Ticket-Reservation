﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalPark_WebApp_116.Migrations
{
    public partial class addbooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NationalParks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Established = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalParks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingTickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildrenCount = table.Column<int>(type: "int", nullable: false),
                    ChildrenTicketPrice = table.Column<int>(type: "int", nullable: false),
                    AdultCount = table.Column<int>(type: "int", nullable: false),
                    AdultTicketPrice = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    NationalParkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingTickets_NationalParks_NationalParkId",
                        column: x => x.NationalParkId,
                        principalTable: "NationalParks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elevation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    NationalParkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trails_NationalParks_NationalParkId",
                        column: x => x.NationalParkId,
                        principalTable: "NationalParks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTickets_NationalParkId",
                table: "BookingTickets",
                column: "NationalParkId");

            migrationBuilder.CreateIndex(
                name: "IX_Trails_NationalParkId",
                table: "Trails",
                column: "NationalParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTickets");

            migrationBuilder.DropTable(
                name: "Trails");

            migrationBuilder.DropTable(
                name: "NationalParks");
        }
    }
}
