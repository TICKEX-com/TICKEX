using event_service.Entities;
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
                    OrganizationName = "",
                    profileImage = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521"
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
                    OrganizationName = "ENSA",
                    profileImage = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521"
                }
                );

                // events for organizer 1

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 1,
                        Title = "REAL MADRID VS BAYERN MUNICH",
                        Description = "Champions league semi finals",
                        EventDate = DateTime.Now,
                        CreationDate = DateTime.Now,
                        Duration = 2,
                        City = "Madrid",
                        Address = "Santiago Bernabéu",
                        Time = "20h00",
                        DesignId = 0,
                        OrganizerId = "1",
                        EventType = "Sports",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 2,
                        Title = "Fashion Show",
                        Description = "Exclusive fashion show showcasing latest trends",
                        EventDate = DateTime.Now.AddDays(120),
                        CreationDate = DateTime.Now,
                        Duration = 3,
                        City = "Rabat",
                        Address = "Royal Theater Rabat",
                        Time = "19h00",
                        DesignId = 0,
                        OrganizerId = "1",
                        EventType = "Fashion",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 3,
                        Title = "Tech Conference",
                        Description = "Cutting-edge technology conference",
                        EventDate = DateTime.Now.AddDays(30),
                        CreationDate = DateTime.Now,
                        Duration = 4,
                        City = "Casablanca",
                        Address = "Casablanca International Convention Center",
                        Time = "10h00",
                        DesignId = 0,
                        OrganizerId = "1",
                        EventType = "Technology",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 4,
                        Title = "Music Festival",
                        Description = "Annual music festival featuring top artists",
                        EventDate = DateTime.Now.AddDays(60),
                        CreationDate = DateTime.Now,
                        Duration = 2,
                        City = "Marrakech",
                        Address = "Palmeraie Marrakech",
                        Time = "18h00",
                        DesignId = 0,
                        OrganizerId = "1",
                        EventType = "Music",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 5,
                        Title = "Art Exhibition",
                        Description = "Contemporary art exhibition",
                        EventDate = DateTime.Now.AddDays(90),
                        CreationDate = DateTime.Now,
                        Duration = 2.5F,
                        City = "Tangier",
                        Address = "Tangier Art Gallery",
                        Time = "15h00",
                        DesignId = 0,
                        OrganizerId = "1",
                        EventType = "Art",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                // events for organizer 2

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 6,
                        Title = "ENSA Career Fair",
                        Description = "Annual career fair organized by ENSA",
                        EventDate = DateTime.Now.AddDays(30),
                        CreationDate = DateTime.Now,
                        Duration = 4,
                        City = "Agadir",
                        Address = "ENSA Agadir",
                        Time = "09h00",
                        DesignId = 0,
                        OrganizerId = "2",
                        EventType = "Career",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 7,
                        Title = "Tech Conference 2024",
                        Description = "Cutting-edge technology conference",
                        EventDate = DateTime.Now.AddDays(60),
                        CreationDate = DateTime.Now,
                        Duration = 5,
                        City = "Casablanca",
                        Address = "Casablanca International Convention Center",
                        Time = "10h00",
                        DesignId = 0,
                        OrganizerId = "2",
                        EventType = "Technology",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );


                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 8,
                        Title = "Music Festival",
                        Description = "Annual music festival featuring top artists",
                        EventDate = DateTime.Now.AddDays(90),
                        CreationDate = DateTime.Now,
                        Duration = 8,
                        City = "Marrakech",
                        Address = "Palmeraie Marrakech",
                        Time = "18h00",
                        DesignId = 0,
                        OrganizerId = "2",
                        EventType = "Music",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );


                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 9,
                        Title = "Startup Summit",
                        Description = "Annual startup summit bringing together entrepreneurs and investors",
                        EventDate = DateTime.Now.AddDays(120),
                        CreationDate = DateTime.Now,
                        Duration = 7,
                        City = "Marrakech",
                        Address = "Marrakech Conference Center",
                        Time = "09h00",
                        DesignId = 0,
                        OrganizerId = "2",
                        EventType = "Startup",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 10,
                        Title = "Food Festival",
                        Description = "Celebration of culinary delights with food stalls and cooking demonstrations",
                        EventDate = DateTime.Now.AddDays(150),
                        CreationDate = DateTime.Now,
                        Duration = 6,
                        City = "Casablanca",
                        Address = "Casablanca Food Park",
                        Time = "12h00",
                        DesignId = 0,
                        OrganizerId = "2",
                        EventType = "Food",
                        Poster = "https://firebasestorage.googleapis.com/v0/b/tickex-20fa7.appspot.com/o/images%2FScreenshot%20from%202024-04-15%2017-01-42.png-7abac09f-7c7d-4101-ac2b-703ba7a55fb6?alt=media&token=70fad095-672c-4226-b0af-8670422be521",
                    }
                );

                for (int eventId = 1; eventId <= 10; eventId++)
                {
                    // Seed categories for each event
                    for (int categoryId = 1; categoryId <= 3; categoryId++)
                    {
                        modelBuilder.Entity<Category>().HasData(
                            new Category
                            {
                                Id = $"{eventId}-{categoryId}", // Generate a string ID
                                Name = categoryId == 1 ? "Gold" : (categoryId == 2 ? "Silver" : "Bronze"),
                                Seats = categoryId == 1 ? 100 : (categoryId == 2 ? 200 : 300), // Adjust seats accordingly
                                Price = categoryId == 1 ? 1000 : (categoryId == 2 ? 500 : 300), // Adjust prices accordingly
                                Color = categoryId == 1 ? "#FFD700" : (categoryId == 2 ? "#C0C0C0" : "#CD7F32"), // Gold, Silver, Bronze colors
                                EventId = eventId,
                            }
                        );
                    }
                }


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
                Console.WriteLine(ex.Message);
            }

        }

    }
}
