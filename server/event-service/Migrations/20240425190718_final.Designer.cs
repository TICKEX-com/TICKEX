﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using event_service.Data;

#nullable disable

namespace event_service.Migrations
{
    [DbContext(typeof(DataContext))]
<<<<<<<< HEAD:server/event-service/Migrations/20240420121835_final.Designer.cs
    [Migration("20240420121835_final")]
========
    [Migration("20240425190718_final")]
>>>>>>>> origin/fix-gateway:server/event-service/Migrations/20240425190718_final.Designer.cs
    partial class final
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ticket", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("EventId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("event_service.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Cinema"
                        });
                });

            modelBuilder.Entity("event_service.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("event_service.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DesignId")
                        .HasColumnType("int");

                    b.Property<bool>("Is_finished")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MinPrize")
                        .HasColumnType("real");

                    b.Property<bool>("On_sell")
                        .HasColumnType("bit");

                    b.Property<string>("OrganizerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
<<<<<<<< HEAD:server/event-service/Migrations/20240420121835_final.Designer.cs
                            Date = new DateTime(2024, 4, 20, 13, 18, 35, 485, DateTimeKind.Local).AddTicks(6542),
========
                            Date = new DateTime(2024, 4, 25, 20, 7, 17, 812, DateTimeKind.Local).AddTicks(859),
>>>>>>>> origin/fix-gateway:server/event-service/Migrations/20240425190718_final.Designer.cs
                            Description = "i am a football match",
                            DesignId = 1,
                            Is_finished = false,
                            Location = "maps",
                            MinPrize = 500f,
                            On_sell = false,
                            OrganizerId = "1",
                            Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp5",
                            Title = "Match"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
<<<<<<<< HEAD:server/event-service/Migrations/20240420121835_final.Designer.cs
                            Date = new DateTime(2024, 4, 20, 13, 18, 35, 485, DateTimeKind.Local).AddTicks(6598),
========
                            Date = new DateTime(2024, 4, 25, 20, 7, 17, 812, DateTimeKind.Local).AddTicks(918),
>>>>>>>> origin/fix-gateway:server/event-service/Migrations/20240425190718_final.Designer.cs
                            Description = "i am a football match",
                            DesignId = 1,
                            Is_finished = false,
                            Location = "maps",
                            MinPrize = 400f,
                            On_sell = false,
                            OrganizerId = "2",
                            Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp995",
                            Title = "Match"
                        });
                });

            modelBuilder.Entity("event_service.Entities.Image", b =>
                {
                    b.Property<string>("url")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.HasKey("url");

                    b.HasIndex("EventId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("event_service.Entities.Organizer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizers");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Email = "anas@gmail.com",
                            OrganizationName = "ENSA",
                            PhoneNumber = "1234567890",
                            firstname = "anas",
                            lastname = "chatt"
                        },
                        new
                        {
                            Id = "2",
                            Email = "aimane@gmail.com",
                            OrganizationName = "ENSA",
                            PhoneNumber = "1234567890",
                            firstname = "aimane",
                            lastname = "chanaa"
                        });
                });

            modelBuilder.Entity("Ticket", b =>
                {
                    b.HasOne("event_service.Entities.Client", null)
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("event_service.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("event_service.Entities.Event", b =>
                {
                    b.HasOne("event_service.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("event_service.Entities.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("event_service.Entities.Image", b =>
                {
                    b.HasOne("event_service.Entities.Event", "Event")
                        .WithMany("Images")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Event");
                });

            modelBuilder.Entity("event_service.Entities.Event", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
