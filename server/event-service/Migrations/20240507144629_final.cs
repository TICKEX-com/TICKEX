using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    OrganizationName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Time = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    DesignId = table.Column<int>(type: "integer", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: false),
                    OrganizerId = table.Column<string>(type: "text", nullable: false),
                    Poster = table.Column<string>(type: "text", nullable: false),
                    On_sell = table.Column<bool>(type: "boolean", nullable: false),
                    Is_finished = table.Column<bool>(type: "boolean", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Seats = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
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
                    url = table.Column<string>(type: "text", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: true)
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
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false)
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
                    { "1", "anas@gmail.com", "", "1234567890", "anas", "chatt" },
                    { "2", "aimane@gmail.com", "ENSA", "1234567890", "aimane", "chanaa" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "City", "CreationDate", "Description", "DesignId", "Duration", "EventDate", "EventType", "Is_finished", "On_sell", "OrganizerId", "Poster", "Time", "Title" },
                values: new object[,]
                {
                    { 1, "Santiago Bernabéu", "Madrid", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4443), "Champions league semi finals", 0, 2f, new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4442), "Sports", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "20h00", "REAL MADRID VS BAYERN MUNICH" },
                    { 2, "Royal Theater Rabat", "Rabat", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4467), "Exclusive fashion show showcasing latest trends", 0, 3f, new DateTime(2024, 9, 4, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4463), "Fashion", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "19h00", "Fashion Show" },
                    { 3, "Casablanca International Convention Center", "Casablanca", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4482), "Cutting-edge technology conference", 0, 4f, new DateTime(2024, 6, 6, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4481), "Technology", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "10h00", "Tech Conference" },
                    { 4, "Palmeraie Marrakech", "Marrakech", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4497), "Annual music festival featuring top artists", 0, 2f, new DateTime(2024, 7, 6, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4496), "Music", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "18h00", "Music Festival" },
                    { 5, "Tangier Art Gallery", "Tangier", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4511), "Contemporary art exhibition", 0, 2.5f, new DateTime(2024, 8, 5, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4510), "Art", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "15h00", "Art Exhibition" },
                    { 6, "ENSA Agadir", "Agadir", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4561), "Annual career fair organized by ENSA", 0, 4f, new DateTime(2024, 6, 6, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4560), "Career", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "09h00", "ENSA Career Fair" },
                    { 7, "Casablanca International Convention Center", "Casablanca", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4577), "Cutting-edge technology conference", 0, 5f, new DateTime(2024, 7, 6, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4576), "Technology", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "10h00", "Tech Conference 2024" },
                    { 8, "Palmeraie Marrakech", "Marrakech", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4591), "Annual music festival featuring top artists", 0, 8f, new DateTime(2024, 8, 5, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4590), "Music", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "18h00", "Music Festival" },
                    { 9, "Marrakech Conference Center", "Marrakech", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4605), "Annual startup summit bringing together entrepreneurs and investors", 0, 7f, new DateTime(2024, 9, 4, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4604), "Startup", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "09h00", "Startup Summit" },
                    { 10, "Casablanca Food Park", "Casablanca", new DateTime(2024, 5, 7, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4621), "Celebration of culinary delights with food stalls and cooking demonstrations", 0, 6f, new DateTime(2024, 10, 4, 15, 46, 28, 717, DateTimeKind.Local).AddTicks(4620), "Food", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "12h00", "Food Festival" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "EventId", "Name", "Price", "Seats" },
                values: new object[,]
                {
                    { 1, "#FFD700", 1, "Gold", 1000f, 100 },
                    { 2, "#C0C0C0", 1, "Silver", 500f, 200 },
                    { 3, "#CD7F32", 1, "Bronze", 300f, 300 },
                    { 4, "#FFD700", 2, "Gold", 1000f, 100 },
                    { 5, "#C0C0C0", 2, "Silver", 500f, 200 },
                    { 6, "#CD7F32", 2, "Bronze", 300f, 300 },
                    { 7, "#FFD700", 3, "Gold", 1000f, 100 },
                    { 8, "#C0C0C0", 3, "Silver", 500f, 200 },
                    { 9, "#CD7F32", 3, "Bronze", 300f, 300 },
                    { 10, "#FFD700", 4, "Gold", 1000f, 100 },
                    { 11, "#C0C0C0", 4, "Silver", 500f, 200 },
                    { 12, "#CD7F32", 4, "Bronze", 300f, 300 },
                    { 13, "#FFD700", 5, "Gold", 1000f, 100 },
                    { 14, "#C0C0C0", 5, "Silver", 500f, 200 },
                    { 15, "#CD7F32", 5, "Bronze", 300f, 300 },
                    { 16, "#FFD700", 6, "Gold", 1000f, 100 },
                    { 17, "#C0C0C0", 6, "Silver", 500f, 200 },
                    { 18, "#CD7F32", 6, "Bronze", 300f, 300 },
                    { 19, "#FFD700", 7, "Gold", 1000f, 100 },
                    { 20, "#C0C0C0", 7, "Silver", 500f, 200 },
                    { 21, "#CD7F32", 7, "Bronze", 300f, 300 },
                    { 22, "#FFD700", 8, "Gold", 1000f, 100 },
                    { 23, "#C0C0C0", 8, "Silver", 500f, 200 },
                    { 24, "#CD7F32", 8, "Bronze", 300f, 300 },
                    { 25, "#FFD700", 9, "Gold", 1000f, 100 },
                    { 26, "#C0C0C0", 9, "Silver", 500f, 200 },
                    { 27, "#CD7F32", 9, "Bronze", 300f, 300 },
                    { 28, "#FFD700", 10, "Gold", 1000f, 100 },
                    { 29, "#C0C0C0", 10, "Silver", 500f, 200 },
                    { 30, "#CD7F32", 10, "Bronze", 300f, 300 }
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
