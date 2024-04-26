using Microsoft.EntityFrameworkCore;
using event_service.Entities;
using Google.Api.Gax.ResourceNames;

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
                    Date = DateTime.Now,
                    Location = "maps",
                    MinPrize = 500,
                    DesignId = 1,
                    OrganizerId = "1",
                    CategoryId = 1,
                    Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp5"
                }
                );

                modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 2,
                    Title = "Match",
                    Description = "i am a football match",
                    Date = DateTime.Now,
                    Location = "maps",
                    MinPrize = 400,
                    DesignId = 1,
                    OrganizerId = "2",
                    CategoryId = 1,
                    Poster = "1YwGlpSZ3wrNrUhF3sVxMaaC6iIz1hDp995"
                }
                );
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sport",
                }
                );
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 2,
                    Name = "Cinema",
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

            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }
            
        }

    }
}
