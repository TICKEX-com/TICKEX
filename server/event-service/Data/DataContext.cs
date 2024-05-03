﻿using event_service.Entities;
using Microsoft.EntityFrameworkCore;

namespace event_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Client> Clients { get; set; }  

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Organizer>().HasData(
                new Organizer
                {
                    Id = "1",
                    Email = "anas@gmail.com",
                    firstname = "anas",
                    lastname = "chatt",
                    PhoneNumber = "1234567890",
                    OrganizationName = "ENSA"
                }
                );
                modelBuilder.Entity<Organizer>().HasData(
                new Organizer
                {
                    Id = "2",
                    Email = "aimane@gmail.com",
                    firstname = "aimane",
                    lastname = "chanaa",
                    PhoneNumber = "1234567890",
                    OrganizationName = "ENSA"
                }
                );

                modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Match",
                    Description = "i am a football match",
                    EventDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    City = "Tangier",
                    Address = "address",
                    Time = "13h00",
                    DesignId = 0,
                    OrganizerId = "1",
                    EventType = "Sports",
                    Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp5",
                }
                );

                modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 2,
                    Title = "Match",
                    Description = "i am a movie",
                    EventDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    City = "Tangier",
                    Address = "address",
                    DesignId = 0,
                    Time = "00h00",
                    OrganizerId = "2",
                    EventType = "Cinema",
                    Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp995",
                }
                );
                
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "VIP",
                    Seats = 100,
                    Price = 500,
                    Color = "#ff0000",
                    EventId = 1
                }
                );

                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 2,
                    Name = "Normal",
                    Seats = 500,
                    Price = 60,
                    Color = "#ff0000",
                    EventId = 1
                }
                );

                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 3,
                    Name = "Normal",
                    Seats = 400,
                    Price = 50,
                    Color = "#ff0000",
                    EventId = 2
                }
                );

                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 4,
                    Name = "VIP",
                    Seats = 100,
                    Price = 700,
                    Color = "#ff0000",
                    EventId = 2
                }
                );


                modelBuilder.Entity<Event>()
                            .HasMany(e => e.Clients)
                            .WithMany(e => e.Events)
                            .UsingEntity(
                                "Ticket",
                                l => l.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)).OnDelete(DeleteBehavior.Restrict),
                                r => r.HasOne(typeof(Event)).WithMany().HasForeignKey("EventId").HasPrincipalKey(nameof(Event.Id)).OnDelete(DeleteBehavior.Restrict),
                                j => j.HasKey("EventId", "ClientId"));


                modelBuilder.Entity<Event>()
                            .HasMany(e => e.Images)
                            .WithOne(e => e.Event)
                            .HasForeignKey("EventId")
                            .IsRequired(false)
                            .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Event>()
                            .HasMany(e => e.Categories)
                            .WithOne(e => e.Event)
                            .HasForeignKey("EventId")
                            .IsRequired(false)
                            .OnDelete(DeleteBehavior.Cascade);

            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }
            
        }

    }
}
