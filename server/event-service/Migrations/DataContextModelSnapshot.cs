﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using event_service.Data;

#nullable disable

namespace event_service.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<int>("DesignId")
                        .HasColumnType("int");

                    b.Property<bool>("Is_finished")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_public")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MinPrize")
                        .HasColumnType("real");

                    b.Property<bool>("On_sell")
                        .HasColumnType("bit");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Date = new DateTime(2024, 3, 7, 11, 33, 10, 908, DateTimeKind.Local).AddTicks(1204),
                            Description = "i am a football match",
                            DesignId = 1,
                            Is_finished = false,
                            Is_public = false,
                            Location = "maps",
                            MinPrize = 500f,
                            On_sell = false,
                            OrganizerId = 1,
                            Title = "Match"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Date = new DateTime(2024, 3, 7, 11, 33, 10, 908, DateTimeKind.Local).AddTicks(1341),
                            Description = "i am a movie",
                            DesignId = 2,
                            Is_finished = false,
                            Is_public = false,
                            Location = "maps",
                            MinPrize = 500f,
                            On_sell = false,
                            OrganizerId = 2,
                            Title = "Cinema"
                        });
                });

            modelBuilder.Entity("event_service.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("event_service.Entities.Event", b =>
                {
                    b.HasOne("event_service.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("event_service.Entities.Image", b =>
                {
                    b.HasOne("event_service.Entities.Event", "Event")
                        .WithMany("Images")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
