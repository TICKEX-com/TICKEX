﻿using System;
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
                    profileImage = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<string>(type: "text", nullable: false),
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
                columns: new[] { "Id", "Email", "OrganizationName", "PhoneNumber", "firstname", "lastname", "profileImage" },
                values: new object[,]
                {
                    { "1", "anas@gmail.com", "", "1234567890", "anas", "chatt", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521" },
                    { "2", "aimane@gmail.com", "ENSA", "1234567890", "aimane", "chanaa", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "City", "CreationDate", "Description", "DesignId", "Duration", "EventDate", "EventType", "Is_finished", "On_sell", "OrganizerId", "Poster", "Time", "Title" },
                values: new object[,]
                {
                    { 1, "Santiago Bernabéu", "Madrid", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6305), "Champions league semi finals", 0, 2f, new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6304), "Sports", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "20h00", "REAL MADRID VS BAYERN MUNICH" },
                    { 2, "Royal Theater Rabat", "Rabat", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6323), "Exclusive fashion show showcasing latest trends", 0, 3f, new DateTime(2024, 9, 11, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6319), "Fashion", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "19h00", "Fashion Show" },
                    { 3, "Casablanca International Convention Center", "Casablanca", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6338), "Cutting-edge technology conference", 0, 4f, new DateTime(2024, 6, 13, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6337), "Technology", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "10h00", "Tech Conference" },
                    { 4, "Palmeraie Marrakech", "Marrakech", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6351), "Annual music festival featuring top artists", 0, 2f, new DateTime(2024, 7, 13, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6350), "Music", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "18h00", "Music Festival" },
                    { 5, "Tangier Art Gallery", "Tangier", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6364), "Contemporary art exhibition", 0, 2.5f, new DateTime(2024, 8, 12, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6363), "Art", false, true, "1", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "15h00", "Art Exhibition" },
                    { 6, "ENSA Agadir", "Agadir", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6380), "Annual career fair organized by ENSA", 0, 4f, new DateTime(2024, 6, 13, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6379), "Career", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "09h00", "ENSA Career Fair" },
                    { 7, "Casablanca International Convention Center", "Casablanca", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6393), "Cutting-edge technology conference", 0, 5f, new DateTime(2024, 7, 13, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6392), "Technology", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "10h00", "Tech Conference 2024" },
                    { 8, "Palmeraie Marrakech", "Marrakech", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6406), "Annual music festival featuring top artists", 0, 8f, new DateTime(2024, 8, 12, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6405), "Music", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "18h00", "Music Festival" },
                    { 9, "Marrakech Conference Center", "Marrakech", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6419), "Annual startup summit bringing together entrepreneurs and investors", 0, 7f, new DateTime(2024, 9, 11, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6418), "Startup", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "09h00", "Startup Summit" },
                    { 10, "Casablanca Food Park", "Casablanca", new DateTime(2024, 5, 14, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6433), "Celebration of culinary delights with food stalls and cooking demonstrations", 0, 6f, new DateTime(2024, 10, 11, 16, 59, 0, 739, DateTimeKind.Local).AddTicks(6432), "Food", false, true, "2", "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521", "12h00", "Food Festival" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "EventId", "Name", "Price", "Seats" },
                values: new object[,]
                {
                    { "1-1", "#FFD700", 1, "Gold", 1000f, 100 },
                    { "1-2", "#C0C0C0", 1, "Silver", 500f, 200 },
                    { "1-3", "#CD7F32", 1, "Bronze", 300f, 300 },
                    { "10-1", "#FFD700", 10, "Government", 8000f, 100 },
                    { "10-2", "#C0C0C0", 10, "Marine", 4000f, 200 },
                    { "10-3", "#CD7F32", 10, "Pirate", 1000f, 300 },
                    { "2-1", "#FFD700", 2, "Gold", 1000f, 100 },
                    { "2-2", "#C0C0C0", 2, "Silver", 500f, 200 },
                    { "2-3", "#CD7F32", 2, "Bronze", 300f, 300 },
                    { "3-1", "#FFD700", 3, "Gold", 1000f, 100 },
                    { "3-2", "#C0C0C0", 3, "Silver", 500f, 200 },
                    { "3-3", "#CD7F32", 3, "Bronze", 300f, 300 },
                    { "4-1", "#FFD700", 4, "VIP", 2500f, 100 },
                    { "4-2", "#C0C0C0", 4, "Premium", 1000f, 200 },
                    { "4-3", "#CD7F32", 4, "Classic", 100f, 300 },
                    { "5-1", "#FFD700", 5, "VIP", 2500f, 100 },
                    { "5-2", "#C0C0C0", 5, "Premium", 1000f, 200 },
                    { "5-3", "#CD7F32", 5, "Classic", 100f, 300 },
                    { "6-1", "#FFD700", 6, "VIP", 2500f, 100 },
                    { "6-2", "#C0C0C0", 6, "Premium", 1000f, 200 },
                    { "6-3", "#CD7F32", 6, "Classic", 100f, 300 },
                    { "7-1", "#FFD700", 7, "Government", 8000f, 100 },
                    { "7-2", "#C0C0C0", 7, "Marine", 4000f, 200 },
                    { "7-3", "#CD7F32", 7, "Pirate", 1000f, 300 },
                    { "8-1", "#FFD700", 8, "Government", 8000f, 100 },
                    { "8-2", "#C0C0C0", 8, "Marine", 4000f, 200 },
                    { "8-3", "#CD7F32", 8, "Pirate", 1000f, 300 },
                    { "9-1", "#FFD700", 9, "Government", 8000f, 100 },
                    { "9-2", "#C0C0C0", 9, "Marine", 4000f, 200 },
                    { "9-3", "#CD7F32", 9, "Pirate", 1000f, 300 }
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
