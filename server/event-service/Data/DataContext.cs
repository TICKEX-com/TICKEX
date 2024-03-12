using Microsoft.EntityFrameworkCore;
using event_service.Entities;
using System.Linq.Expressions;

namespace event_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

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
                    OrganizerUsername = "hhhh",
                    CategoryId = 1
                }
                );
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sport",
                }
                );
                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 2,
                        Title = "Cinema",
                        Description = "i am a movie",
                        Date = DateTime.Now,
                        Location = "maps",
                        MinPrize = 500,
                        DesignId = 2,
                        OrganizerUsername = "ooooo",
                        CategoryId = 2
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
                                l => l.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)),
                                r => r.HasOne(typeof(Event)).WithMany().HasForeignKey("EventId").HasPrincipalKey(nameof(Event.Id)),
                                j => j.HasKey("EventId", "ClientId"));
            } catch (Exception ex)
            {
                Console.WriteLine( ex.ToString() );
                Console.ReadLine();
            }
            
        }

    }
}
