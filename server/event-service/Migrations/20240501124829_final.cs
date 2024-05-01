using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace event_service.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    DesignId = table.Column<int>(type: "int", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    On_sell = table.Column<bool>(type: "bit", nullable: false),
                    Is_finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Organizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.url);
                    table.ForeignKey(
                        name: "FK_Images_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => new { x.EventId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_Ticket_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Organizers",
                columns: new[] { "Id", "Email", "OrganizationName", "PhoneNumber", "firstname", "lastname" },
                values: new object[,]
                {
                    { "1", "anas@gmail.com", "ENSA", "1234567890", "anas", "chatt" },
                    { "2", "aimane@gmail.com", "ENSA", "1234567890", "aimane", "chanaa" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "City", "CreationDate", "Description", "DesignId", "Duration", "EventDate", "EventType", "Is_finished", "On_sell", "OrganizerId", "Poster", "Time", "Title" },
                values: new object[,]
                {
                    { 1, "address", "Tangier", new DateTime(2024, 5, 1, 13, 48, 29, 384, DateTimeKind.Local).AddTicks(9778), "i am a football match", 1, 0f, new DateTime(2024, 5, 1, 13, 48, 29, 384, DateTimeKind.Local).AddTicks(9769), "Sports", false, true, "1", "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp5", "13h00", "Match" },
                    { 2, "address", "Tangier", new DateTime(2024, 5, 1, 13, 48, 29, 384, DateTimeKind.Local).AddTicks(9919), "i am a movie", 1, 0f, new DateTime(2024, 5, 1, 13, 48, 29, 384, DateTimeKind.Local).AddTicks(9918), "Cinema", false, true, "2", "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp995", "00h00", "Match" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "EventId", "Name", "Price", "Seats" },
                values: new object[,]
                {
                    { 1, "#ff0000", 1, "VIP", 500f, 100 },
                    { 2, "#ff0000", 1, "Normal", 60f, 500 },
                    { 3, "#ff0000", 2, "Normal", 50f, 400 },
                    { 4, "#ff0000", 2, "VIP", 700f, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EventId",
                table: "Categories",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_EventId",
                table: "Images",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ClientId",
                table: "Ticket",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Organizers");
        }
    }
}
